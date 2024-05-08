using UnityEngine;
using UnityEngine.ProBuilder;

public class PlayerMovement : MonoBehaviour, CheatListener
{
    private enum PlayerState
    {
        OnGround,
        OnJump
    }

    
    public float Speed = 10f;
    public float gravity = -6.5f;
    public float jumpHeight = 3f;
    
    private CharacterController characterController;
    private Vector3 playerVelocity;
    private PlayerState playerState = PlayerState.OnGround;
    private float allowableSpeed = 0;
    
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    public void MovePlayer(Vector2 input, Animator animator)
    {
        if (input == Vector2.zero)
        {
            allowableSpeed = 0;
        }
        
        playerVelocity.y += gravity * Time.deltaTime;
        if (characterController.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = gravity * 0.1f;
        }
        
        var speedIncrement = Speed > allowableSpeed ? Mathf.Lerp(allowableSpeed, Speed, 0.05f) : 0;
        allowableSpeed += speedIncrement;
        Vector3 dir = new Vector3(input.x, 0, input.y), 
            movement = (allowableSpeed * Time.deltaTime) * transform.TransformDirection(dir) + playerVelocity;
        characterController.Move(movement);
        animator.SetBool("IsWalking", movement.x != 0 || movement.z != 0);
        
        if (characterController.isGrounded)
        {
            playerState = PlayerState.OnGround;
        }
    }

    public void JumpPlayer()
    {
        if (playerState != PlayerState.OnGround)
        {
            return;
        }
        
        playerState = PlayerState.OnJump;
        playerVelocity.y = Mathf.Sqrt(jumpHeight);
    }

    public void ChangeSpeed()
    {
        this.HandleSpeed(this);
    }
}