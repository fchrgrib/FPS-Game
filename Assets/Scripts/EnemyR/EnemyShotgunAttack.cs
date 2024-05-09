using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyShotgunAttack : EnemyAttack
{
    public int pelletsPerShot = 12;
    public float damagePerPellet = 7f;
    public float shootingDelay = 1f;
    public float range = 15f;
    public Light faceLight;
    
    private AudioSource shotgunAudio;
    private ParticleSystem shotgunParticles;
    private Light shotgunLight;
    private List<LineRenderer> pelletLineRenderers;
    private Ray ray;

    private GameObject playerOnly;
    
    private int shootableMask;
    private float shootingTimer;
    
    private static readonly Color TrailColor = new(255, 232, 163);
    
    protected override void Awake()
    {
        base.Awake();
        
        EnemyManager = GetComponentInParent<EnemyManager>();
        
        shotgunAudio = GetComponent<AudioSource>();
        shotgunParticles = GetComponent<ParticleSystem>();
        shotgunLight = GetComponent<Light>();
        
        pelletLineRenderers = new List<LineRenderer>(pelletsPerShot);
        for (var i = 0; i < pelletsPerShot; i++)
        {
            var pelletLineRenderer = new GameObject("Pellet Line Renderer").AddComponent<LineRenderer>();
            pelletLineRenderer.transform.SetParent(transform);
            pelletLineRenderer.startWidth = 0.01f;
            pelletLineRenderer.endWidth = 0.01f;
            pelletLineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            pelletLineRenderer.startColor = TrailColor;
            pelletLineRenderer.endColor = TrailColor;
            pelletLineRenderer.positionCount = 2;
            pelletLineRenderers.Add(pelletLineRenderer);
        }
        
        playerOnly = GameObject.Find("PlayerOnly");
        
        shootableMask = LayerMask.GetMask("Player");
    }

    protected override void Update()
    {
        if (IsPaused) return;
        
        shootingTimer += Time.deltaTime;
        
        if (shootingTimer >= shootingDelay && ShouldShoot())
        {
            Attack();
            shootingTimer = 0f;
        }
        
        if (shootingTimer >= shootingDelay * 0.05f)
        {
            DisableEffects();
        }
    }

    protected override void Attack()
    {
        if (EnemyManager.health < 0) return;
        print("attack");
    
        if (PlayerManager.PlayerHp > 0)
        {
            Shoot();
        }
    }

    private void DisableEffects()
    {
        shotgunLight.enabled = false;
        faceLight.enabled = false;
        pelletLineRenderers.ForEach(pelletLineRenderer => pelletLineRenderer.enabled = false);
    }
    
    private bool ShouldShoot()
    {
        var playerTransform = playerOnly.transform;
        var enemy = transform;
        
        var playerDirection = playerTransform.position - enemy.position;
        var angle = Vector3.Angle(playerDirection, enemy.forward);
        
        return angle < 45f && playerDirection.magnitude < range;
    }

    private void Shoot()
    {
        shotgunAudio.Play();
        
        shotgunLight.enabled = true;
        faceLight.enabled = true;
        
        shotgunParticles.Stop();
        shotgunParticles.Play();
        
        pelletLineRenderers.ForEach(pelletLineRenderer => pelletLineRenderer.enabled = true);
        
        for (var i = 0; i < pelletsPerShot; i++)
        {
            var direction = transform.forward;
            direction.x += Random.Range(-0.1f, 0.1f);
            direction.y += Random.Range(-0.1f, 0.1f);
            direction.z += Random.Range(-0.1f, 0.1f);
            
            ray.origin = transform.position;
            ray.direction = direction;
            
            if (Physics.Raycast(ray, out var hit, range, shootableMask))
            {
                if (hit.collider.gameObject == playerOnly)
                    PlayerManager.TakeDamage(damagePerPellet * attackDamageMultiplier);
                
                pelletLineRenderers[i].SetPosition(0, transform.position);
                pelletLineRenderers[i].SetPosition(1, hit.point);
            }
            else
            {
                pelletLineRenderers[i].SetPosition(0, transform.position);
                pelletLineRenderers[i].SetPosition(1, ray.origin + ray.direction * range);
            }
        }
    }
}