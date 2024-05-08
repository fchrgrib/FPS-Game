using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShotgunController : MonoBehaviour
{
    public float range = 100f;
    public float demage = 30f;
    public float timeBetweenShoot = 0.30f;
    public Camera cam;

    float effectsDisplayTime = 0.2f;
    float timer;
    Light light;
    List<LineRenderer> lineRenderer = new List<LineRenderer>();
    LineRenderer lineComponent;
    AudioSource audioSource;
    ParticleSystem particleSystem;

    float rand;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        light = GetComponent<Light>();
        lineComponent = GetComponent<LineRenderer>();
        particleSystem = GetComponent<ParticleSystem>();
        for(int i = 0; i < 10; i++)
        {
            LineRenderer line = new GameObject().AddComponent<LineRenderer>();
            line.material = line.material;
            line.startColor = lineComponent.startColor;
            line.endColor = lineComponent.endColor;

            line.startWidth = lineComponent.startWidth;
            line.endWidth = lineComponent.endWidth;

            

            lineRenderer.Add(line);
        }
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

        if (timer >= 0.2 * effectsDisplayTime)
        {
            DisableEffects();
        }

    }

    void DisableEffects()
    {
        light.enabled = false;
        for(int i=0; i<lineRenderer.Count; i++)
        {
            lineRenderer[i].enabled = false;
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        timer = 0f;

        particleSystem.Stop();
        particleSystem.Play();
        audioSource.Play();
        light.enabled = true;
        for (int i = 0; i < lineRenderer.Count; i++)
        {
            lineRenderer[i].enabled = true;
            lineRenderer[i].SetPosition(0, transform.position);
        }
        






        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            /*Debug.Log(hit.transform.name);*/
            for (int i = 0;i < lineRenderer.Count; i++)
            {
                float xVal = Random.Range(-5f, 5f);
                float yVal = Random.Range(-5f, 5f);
                float zVal = Random.Range(-5f, 5f);
                lineRenderer[i].SetPosition(1, hit.point + new Vector3(xVal, yVal, zVal));
            }

            // TODO: hit damage to object


        }
    }
}
