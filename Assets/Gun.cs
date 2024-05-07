using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public float demage = 10f;
    public float range = 100f;
    public float timeBetweenShoot = 0.15f;
    public GameObject gun;

    float effectsDisplayTime = 0.2f;
    float timer;
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




        

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            /*Debug.Log(hit.transform.name);*/

            lineRenderer.SetPosition(1, hit.point);

            // TODO: hit damage to object

        
        }
    }
}
