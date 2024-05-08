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
    private Animator _animator;
    
    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        _animator.SetBool("Walk", _navMeshAgent.velocity.magnitude != 0);
        
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, playerDetectionDistance, playerLayerMask);
        if (hitColliders.Length == 1)
        {
            var fleePosition = transform.position + (transform.position - hitColliders[0].transform.position);
            _navMeshAgent.SetDestination(fleePosition);
            return;
        }
        
        _navMeshAgent.SetDestination(transform.parent.position);
    }

    public void MeDead()
    {
        _animator.SetBool("Die", _navMeshAgent.velocity.magnitude != 0);

    }
    
}
