using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class DefaultPetMovement : MonoBehaviour
{

    protected CharacterController characterController;
    
    public float gravity = -9.8f;
    public float maxDistanceToPlayer = 6f;

    private Vector3 distanceVec;
    protected Animator animator;
    
    private void Start()
    {
        characterController = transform.GetComponentInParent<CharacterController>();
        animator = GetComponent<Animator>();
    }
   
    public virtual void MovePet(Vector3 movement, Vector3 playerPosition)
    {
        movement.y += gravity * Time.deltaTime;
        if (movement.y <= 0f)
        {
            movement.y = gravity * 0.1f;
        }
        var playerDistance = transform.position - playerPosition;
        playerDistance.y = 0;
        if (playerDistance.magnitude >= maxDistanceToPlayer)
        {
            movement += -playerDistance * Time.deltaTime;
        }
        
        if (Math.Abs(movement.x) > 0f)
        {
            var movementNormalized = new Vector3(movement.x, 0, movement.z);
            Quaternion targetRotation = Quaternion.LookRotation(movementNormalized, Vector3.up),
                smoothedRotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10);
            transform.rotation = smoothedRotation;
        }
        characterController.Move(movement);
        
        animator.SetBool("Walking", Math.Abs(movement.x) > 0);
    }
    
}
