using System;
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
    
    void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        enemyMovement = GetComponent<EnemyMovement>();
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
        if (!IsDead()) return;
        
        transform.Translate(-Vector3.up * (2.5f * Time.deltaTime));
        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }
    
    private void Die()
    {
        EventManager.TriggerEvent("Sound", transform.position);
        animator.SetTrigger("Dead");
        SetKinematics(true);
        audioSource.clip = deathAudio;
        audioSource.Play();
    }

    public bool IsDead() => health <= 0;
}