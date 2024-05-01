using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Input : MonoBehaviour
{

    public PlayerInput PlayerInput;
    private PlayerInput.OnGroundActions onGroundActions;

    private PlayerMovement playerMovement;
    private PlayerDirection playerDirection;
    
    void Awake()
    {
        PlayerInput = new PlayerInput();
        onGroundActions = PlayerInput.OnGround;
        playerMovement = GetComponent<PlayerMovement>();
        playerDirection = GetComponent<PlayerDirection>();
        onGroundActions.Jump.performed += ctx => playerMovement.JumpPlayer();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerMovement.MovePlayer(onGroundActions.Move.ReadValue<Vector2>());
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