using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShotgunController : MonoBehaviour
{
    public float range = 100f;
    public float damage = 7f;
    public float timeBetweenShoot = 0.30f;
    public Camera cam;

    float effectsDisplayTime = 0.2f;
    float timer;
    Light light;
    List<LineRenderer> lineRenderer = new List<LineRenderer>();
    LineRenderer lineComponent;
    AudioSource audioSource;
    ParticleSystem particleSystem;
    InputManager inputManager;
    private LayerMask enemyLayerMask;
    private Ray ray;
    private PlayerManager PlayerManager;

    float rand;


    private void Awake()
    {
        ray = new Ray();
        audioSource = GetComponent<AudioSource>();
        light = GetComponent<Light>();
        lineComponent = GetComponent<LineRenderer>();
        particleSystem = GetComponent<ParticleSystem>();
        inputManager = GetComponent<InputManager>();
        PlayerManager = GetComponentInParent<PlayerManager>();
        enemyLayerMask = LayerMask.GetMask("Enemy");
        for (int i = 0; i < 10; i++)
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
    void FixedUpdate()
    {
        timer += Time.deltaTime;

        /*Debug.Log(timeBetweenShoot);*/

        if (timer >= timeBetweenShoot && Time.timeScale != 0)
        {
            if (inputManager.PlayerInput.OnGround.Attack.IsPressed())
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
        
        // if (Physics.Raycast(cam.transform.position, cam.transform.forward, out var hit, range))
        // {
        //     /*Debug.Log(hit.transform.name);*/
        //     for (int i = 0;i < lineRenderer.Count; i++)
        //     {
        //         float xVal = Random.Range(-5f, 5f);
        //         float yVal = Random.Range(-5f, 5f);
        //         float zVal = Random.Range(-5f, 5f);
        //         Vector3 positionShoot = hit.point + new Vector3(xVal, yVal, zVal);
        //         // hit.collider.Ge
        //
        //         lineRenderer[i].SetPosition(1, positionShoot);
        //         // hit.point = positionShoot;
        //
        //         // hit.collider.GetComponent<EnemyManager>().TakeDamage(demage, positionShoot);
        //     }
        //
        //     // TODO: hit damage to object
        //
        //
        // }


        foreach (var line in lineRenderer)
        {
            ray.origin = cam.transform.position;
            float xVal = Random.Range(-0.5f, 0.5f);
            float yVal = Random.Range(-0.5f, 0.5f);
            float zVal = Random.Range(-0.5f, 0.5f);
            ray.direction = transform.forward + new Vector3(xVal, yVal, zVal);

            if (Physics.Raycast(ray, out var hit, range, enemyLayerMask))
            {
                EnemyManager enemyManager = hit.collider.GetComponent<EnemyManager>();

                if (enemyManager != null)
                {
                    enemyManager.TakeDamage(damage*PlayerManager.PlayerDamageMultiplier, hit.point);
                }
                
                line.SetPosition(1, hit.point);
            }
            else
            {
                line.SetPosition(1, ray.origin + ray.direction * range);
            }
        }
   
    }
}
