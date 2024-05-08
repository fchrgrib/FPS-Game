using System;
using System.Collections;
using UnityEngine;

namespace Nightmare
{
    public class JenderalBehavior : MonoBehaviour
    {
        public float distanceThreshold = 5f;
        public float damagePerSecond = 3f;
        private Transform player;
        private PlayerManager playerManager;

        private bool coroutineStarted;
        
        void Awake()
        {
            player = GameObject.Find("PlayerOnly").transform;
            playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        }

        void Update()
        {
            if (coroutineStarted) return;
            
            if (WithinDistance())
            {
                StartCoroutine(nameof(DealDamageToPlayer));
                coroutineStarted = true;
            }
        }

        private bool WithinDistance() => Vector3.Distance(player.transform.position, transform.position) < distanceThreshold;

        private IEnumerator DealDamageToPlayer()
        {
            while (true) {
                yield return new WaitForSeconds(1f);
                playerManager.TakeDamage(damagePerSecond);

                if (!WithinDistance())
                {
                    print("stopped");
                    coroutineStarted = false;
                    break;
                }
            }
        }
    }
}