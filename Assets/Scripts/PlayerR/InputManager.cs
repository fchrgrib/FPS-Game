using UnityEngine;

public class InputManager : MonoBehaviour
{

    public PlayerInput PlayerInput;
    private PlayerInput.OnGroundActions _onGroundActions;

    private PlayerMovement _playerMovement;
    private PlayerDirection _playerDirection;
    private Animator _animator;
    
    void Awake()
    {
        PlayerInput = new PlayerInput();
        _onGroundActions = PlayerInput.OnGround;
        _playerMovement = GetComponent<PlayerMovement>();
        _playerDirection = GetComponent<PlayerDirection>();
        _onGroundActions.Jump.performed += ctx => _playerMovement.JumpPlayer();

        _animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        _playerMovement.MovePlayer(_onGroundActions.Move.ReadValue<Vector2>(), _animator);
    }

    private void LateUpdate()
    {
        _playerDirection.MoveDirection(_onGroundActions.MoveDirection.ReadValue<Vector2>());
    }

    private void OnDisable()
    {
        _onGroundActions.Disable();
    }

    private void OnEnable()
    {
        _onGroundActions.Enable();
    }
}