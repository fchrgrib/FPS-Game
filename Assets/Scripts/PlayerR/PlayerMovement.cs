using UnityEngine;

public class PlayerMovement : MonoBehaviour, CheatListener
{
    private enum PlayerState
    {
        OnGround,
        OnJump
    }

    private CharacterController characterController;
    private Vector3 playerVelocity;
    private PlayerState playerState = PlayerState.OnGround;

    public float Speed { get; set; } = 15f;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;


    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    public void MovePlayer(Vector2 input)
    {
        playerVelocity.y += gravity * Time.deltaTime;
        if (playerVelocity.y <= 0f)
        {
            playerVelocity.y = gravity * 0.1f;
        }

        var dir = Vector3.zero;
        dir.x = input.x;
        dir.z = input.y;
        var movement = Speed * Time.deltaTime * transform.TransformDirection(dir) + playerVelocity;
        characterController.Move(movement);
    }

    public void JumpPlayer()
    {
        if (playerState != PlayerState.OnGround)
        {
            return;
        }

        playerVelocity.y = Mathf.Sqrt(jumpHeight);
    }

    public void ChangeSpeed()
    {
        this.HandleSpeed(this);
    }
}