using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    public PlayerInput PlayerInput;
    public GameObject pet;
    private PlayerInput.OnGroundActions onGroundActions;

    private PlayerMovement playerMovement;
    private PlayerDirection playerDirection;

    private DefaultPetMovement petMovement;
    
    void Awake()
    {
        PlayerInput = new PlayerInput();
        onGroundActions = PlayerInput.OnGround;
        playerMovement = GetComponent<PlayerMovement>();
        playerDirection = GetComponent<PlayerDirection>();
        onGroundActions.Jump.performed += ctx => playerMovement.JumpPlayer();

        petMovement = pet.transform.GetChild(0)?.GetComponent<DefaultPetMovement>();
    }

    void FixedUpdate()
    {
        var prevPosition = transform.position;
        playerMovement.MovePlayer(onGroundActions.Move.ReadValue<Vector2>());
        var movement = transform.position - prevPosition;
        movement.y = 0;
        petMovement?.MovePet(movement, transform.position);
    }

    private void LateUpdate()
    {
        playerDirection.MoveDirection(onGroundActions.MoveDirection.ReadValue<Vector2>());
    }

    private void OnDisable()
    {
        onGroundActions.Disable();
    }

    private void OnEnable()
    {
        onGroundActions.Enable();
    }
}