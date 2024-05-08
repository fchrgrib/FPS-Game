using Nightmare;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{

    public float demage = 10f;
    public float range = 100f;
    public float timeBetweenShoot = 0.15f;

    float effectsDisplayTime = 0.2f;
    float timer;
    int shootableMask;
    Ray ray = new Ray();
    ParticleSystem particleSystem;
    AudioSource audioSource;
    Light light;
    LineRenderer lineRenderer;

    public Camera cam;
    // Start is called before the first frame update

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        particleSystem = GetComponent<ParticleSystem>();
        light = GetComponent<Light>();
        lineRenderer = GetComponent<LineRenderer>();
        shootableMask = LayerMask.GetMask("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        /*Debug.Log(timeBetweenShoot);*/

        if (timer >= timeBetweenShoot && Time.timeScale != 0)
        {
            if (Input.GetButton("Fire1"))
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
        RaycastHit hit;
        timer = 0f;

        particleSystem.Stop();
        particleSystem.Play();
        audioSource.Play();
        light.enabled = true;
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, transform.position);


        ray.origin = transform.position;
        ray.direction = transform.forward;






        if (Physics.Raycast(ray, out hit, range, shootableMask))
        {
            /*Debug.Log(hit.transform.name);*/

            BoxHealth enemyHealth = hit.collider.GetComponent<BoxHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(10);
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
