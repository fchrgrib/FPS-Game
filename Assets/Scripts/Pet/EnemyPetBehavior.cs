using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPetBehavior : MonoBehaviour
{

    [SerializeField] private float playerDetectionDistance;
    [SerializeField] private LayerMask playerLayerMask;

    private NavMeshAgent _navMeshAgent;
    
    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, playerDetectionDistance, playerLayerMask);
        if (hitColliders.Length == 1)
        {
            var fleePosition = transform.position + (transform.position - hitColliders[0].transform.position);
            _navMeshAgent.SetDestination(fleePosition);
            return;
        }
        
        _navMeshAgent.SetDestination(transform.parent.position);
    }
}
