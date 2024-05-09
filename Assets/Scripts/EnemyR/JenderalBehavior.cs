using System.Collections;
using UnityEngine;

public class JenderalBehavior : MonoBehaviour
{
    public float distanceThreshold = 15f;
    public float damagePerSecond = 3f;
    protected GameObject Player;
    protected PlayerManager PlayerManager;

    protected bool CoroutineStarted;
    
    protected virtual void Awake()
    {
        Player = GameObject.Find("PlayerOnly");
        PlayerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
    }
    
    void OnDestroy()
    {
        StopCoroutine(nameof(ApplyToPlayer));
    }

    void Update()
    {
        if (CoroutineStarted) return;
        
        if (WithinDistance())
        {
            StartCoroutine(nameof(ApplyToPlayer));
            CoroutineStarted = true;
        }
    }

    private bool WithinDistance() => Vector3.Distance(Player.transform.position, transform.position) < distanceThreshold;

    protected IEnumerator ApplyToPlayer()
    {
        while (true) {
            yield return new WaitForSeconds(1f);
            ApplyEffectsToPlayer();

            if (!WithinDistance())
            {
                DispelEffects();
                CoroutineStarted = false;
                break;
            }
        }
    }

    protected virtual void ApplyEffectsToPlayer()
    {
        PlayerManager.TakeDamage(damagePerSecond);
    }

    protected virtual void DispelEffects()
    {
        
    }
}