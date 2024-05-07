using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PetManager : MonoBehaviour
{
    
    [SerializeField] private GameObject player;

    private PlayerManager playerManager;
    
    private NavMeshAgent navMeshAgent;
    private DefaultPetMovement petMovement;
    private Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerManager = GetComponentInParent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount != 1)
        {
            return;
        }

        if (petMovement is null)
        {
            petMovement = transform.GetChild(0).GetComponent<DefaultPetMovement>();
            animator = petMovement.GetComponent<Animator>();
        }

        animator.SetBool("Walking", navMeshAgent.velocity.magnitude != 0);
        var destination = petMovement.DoActionAndGetDestination(playerManager, player);
        navMeshAgent.SetDestination(destination);
    }
}
