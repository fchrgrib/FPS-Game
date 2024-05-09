using System;
using Nightmare;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{

    public float damage = 10f;
    public float range = 100f;
    public float timeBetweenShoot = 0.15f;
    public Camera cam;

    float effectsDisplayTime = 0.2f;
    private float damageDefault;
    float timer;
    int shootableMask;
    Ray ray = new Ray();
    ParticleSystem particleSystem;
    AudioSource audioSource;
    Light light;
    
    private LineRenderer lineRenderer;
    private InputManager inputManager;
    private UnityAction<float> multiplyDamageListener;

    // Start is called before the first frame update

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        particleSystem = GetComponent<ParticleSystem>();
        light = GetComponent<Light>();
        damageDefault = 10f;
        lineRenderer = GetComponent<LineRenderer>();
        shootableMask = LayerMask.GetMask("Enemy");
        inputManager = GetComponent<InputManager>();
        EventManager.StartListening("OneHitDamage",MultiplyDamage);
    }

    private void OnDestroy()
    {
        EventManager.StopListening("OneHitDamage",MultiplyDamage);
    }

    private void MultiplyDamage(bool isActive)
    {
        if (isActive)
        {
            damage*=1000f;   
        }
        else
        {
            damage = damageDefault;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;


        if (timer >= timeBetweenShoot && Time.timeScale != 0)
        {
            if (inputManager.PlayerInput.OnGround.Attack.IsPressed())
            {
                Shoot();
            }
        }

        if (timer >= timeBetweenShoot * effectsDisplayTime)
        {
            DisableEffects();
        }

/*        if (Input.GetButton("Fire2"))
        {
            gun.SetActive(false);
        }*/
    }

    void DisableEffects()
    {
        light.enabled = false;
        lineRenderer.enabled = false;
    }

    void Shoot()
    {
        timer = 0f;

        particleSystem.Stop();
        particleSystem.Play();
        audioSource.Play();
        light.enabled = true;
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, transform.position);


        ray.origin = transform.position;
        ray.direction = transform.forward;
        

        if (Physics.Raycast(ray, out var hit, range, shootableMask))
        {
            /*Debug.Log(hit.transform.name);*/

            EnemyManager enemyHealth = hit.collider.GetComponent<EnemyManager>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage, hit.point);
            }

            lineRenderer.SetPosition(1, hit.point);

            // TODO: hit damage to object

        
        }
        else
        {
            // ... set the second position of the line renderer to the fullest extent of the gun's range.
            lineRenderer.SetPosition(1, ray.origin + ray.direction * range);
        }
    }
}
