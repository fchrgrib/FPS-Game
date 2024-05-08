using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyShotgunAttack : EnemyAttack
{
    public int pelletsPerShot = 12;
    public float damagePerPellet = 7f;
    public float shootingDelay = 1f;
    public float range = 100f;
    public Light faceLight;
    
    private AudioSource shotgunAudio;
    private ParticleSystem shotgunParticles;
    private Light shotgunLight;
    private LineRenderer lineRenderer;
    private Ray ray;
    
    private int shootableMask;

    private float shootingTimer;
    
    protected override void Awake()
    {
        base.Awake();
        
        shotgunAudio = GetComponent<AudioSource>();
        shotgunParticles = GetComponent<ParticleSystem>();
        shotgunLight = GetComponent<Light>();
        lineRenderer = GetComponent<LineRenderer>();
        
        shootableMask = LayerMask.GetMask("Player");
    }

    private void Update() 
    {
        shootingTimer += Time.deltaTime;
        
        if (shootingTimer >= shootingDelay)
        {
            Attack();
            shootingTimer = 0f;
        }
        
        if (shootingTimer >= shootingDelay * 0.01f)
        {
            DisableEffects();
        }
    }

    protected override void Attack()
    {
        // if (!(enemyManager.health < 0 || IsAttacking)) return;
        Timer = 0f;
        print("attack");
    
        if (playerManager.PlayerHp > 0)
        {
            // anim.SetTrigger("Attack");
            // IsAttacking = true;
            Shoot();
        }
    }

    private void DisableEffects()
    {
        shotgunLight.enabled = false;
        faceLight.enabled = false;
        lineRenderer.enabled = false;
    }

    public void Shoot()
    {
        print("shoot");
        shootingTimer = 0f;
        
        shotgunAudio.Play();
        
        shotgunLight.enabled = true;
        faceLight.enabled = true;
        
        shotgunParticles.Stop();
        shotgunParticles.Play();
        
        lineRenderer.enabled = true;
        
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
                var player = hit.collider.GetComponent<PlayerManager>();
                
                if (player != null)
                {
                    player.TakeDamage(damagePerPellet);
                }

                lineRenderer.SetPosition(i, hit.point);
            }
            else
            {
                lineRenderer.SetPosition(i, ray.origin + ray.direction * range);
            }
        }
    }
}