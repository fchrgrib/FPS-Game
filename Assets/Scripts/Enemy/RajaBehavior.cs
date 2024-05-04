using System;
using System.Collections.Generic;
using UnityEngine;

namespace Nightmare
{
    public class RajaBehavior : MonoBehaviour
    {
        public float distanceThreshold = 10f;
        public float damagePerSecond = 3f;
        private GameObject[] players;
        
        void Awake()
        {
            players = GameObject.FindGameObjectsWithTag("Player");
        }

        void Update()
        {
            if (players == null || players.Length == 0)
                return;
            
            foreach (var player in players)
            {
                if (Vector3.Distance(player.transform.position, transform.position) < distanceThreshold)
                {
                    // Do something
                }
            }
        }
    }
}