using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;

public class DefaultPetMovement : MonoBehaviour
{
    public virtual Vector3 DoActionAndGetDestination(PlayerManager playerManager, GameObject player,
        NavMeshAgent navMeshAgent, Animator animator)
    {
        var destination = player.transform.position;
        var movementNormalized = destination - transform.position;
        movementNormalized.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(movementNormalized, Vector3.up),
            smoothedRotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10);
        transform.rotation = smoothedRotation;

        return destination;
    }
}