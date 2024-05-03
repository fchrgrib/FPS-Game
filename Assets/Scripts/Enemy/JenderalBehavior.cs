using System;
using UnityEngine;

namespace Nightmare
{
    public class JenderalBehavior : MonoBehaviour
    {
        public float distanceThreshold = 10f;
        public float damagePerSecond = 3f;
        private GameObject player;
        
        void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        void Update()
        {
            if (Vector3.Distance(player.transform.position, transform.position) < distanceThreshold)
            {
                // Do something
            }
        }
    }
}