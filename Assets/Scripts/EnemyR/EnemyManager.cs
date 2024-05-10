using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float health = 100f;
    public AudioClip deathAudio;

    private Animator animator;
    private AudioSource audioSource;
    private new ParticleSystem particleSystem;
    private CapsuleCollider capsuleCollider;
    private EnemyMovement enemyMovement;

    private Renderer renderer;

    private bool dieEvent;
    private bool firstPetDeath;
    private bool secondPetDeath;
    [NonSerialized] public bool Dead;
    
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        enemyMovement = GetComponent<EnemyMovement>();

        renderer = (from r in GetComponentsInChildren<Renderer>() where r.GetType() == typeof(SkinnedMeshRenderer) select r).First();
    }

    void OnEnable()
    {
        SetKinematics(false);
    }
    
    public void TakeDamage(float amount, Vector3 hitPoint)
    {
        if (!IsDead())
        {
            audioSource.Play();
            health -= amount;
            
            if (IsDead())
            {
                Die();
            }
            else
            {
                enemyMovement.GoToPlayer();
            }
        }
        
        particleSystem.transform.position = hitPoint;
        particleSystem.Play();
    }
    
    private void SetKinematics(bool isKinematic)
    {
        capsuleCollider.isTrigger = isKinematic;
        capsuleCollider.attachedRigidbody.isKinematic = isKinematic;
    }

    private void Update()
    {
        switch (health)
        {
            case <= 80 when !firstPetDeath:
                firstPetDeath = true;
                EventManager.TriggerEvent("PetDeath");
                break;
            case <= 40 when !secondPetDeath:
                secondPetDeath = true;
                EventManager.TriggerEvent("PetDeath");
                break;
            case <= 40:
                var mat = renderer.material;
                
                var emission = Mathf.PingPong(Time.time, 1f);
                var baseColor = new Color(2, 0, 0);
                var finalColor = baseColor * Mathf.LinearToGammaSpace(emission);
                mat.SetColor("_Color", finalColor);
                
                break;
        }
        
        if (!IsDead()) return;
        
        if (!dieEvent) Die();

        if (Dead)
        {
            transform.Translate(-Vector3.up * Time.deltaTime);
            if (transform.position.y < -5f)
            {
                Destroy(transform.parent ? transform.parent.gameObject : gameObject);
            }
        }
    }
    
    private void Die()
    {
        string trigger = "";

        if (name == "Keroco" || name == "Keroco(Clone)")
        {
            trigger = "EnemyDeath";
        }

        if (name == "Kepala Keroco" || name == "Kepala Keroco(Clone)")
        { 
            trigger = "LeaderOfEnemyDeath";
            
        }

        if (name == "Jenderal")
        {
            trigger = "AdmiralOfEnemyDeath";
        }

        if (name == "Raja")
        {
            trigger = "KingOfEnemyDeath";
        }
        
        EventManager.TriggerEvent("Sound", transform.position);
        EventManager.TriggerEvent(trigger);
        animator.SetTrigger("Dead");
        SetKinematics(true);
        audioSource.clip = deathAudio;
        audioSource.Play();

        dieEvent = true;
    }

    public bool IsDead() => health <= 0;
}