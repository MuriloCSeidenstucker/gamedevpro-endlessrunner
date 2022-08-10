using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerAudioController _playerAudioController;
    
    [Header("Movement Parameters")]
    [SerializeField] private float _horizontalSpeed = 10.0f;
    [SerializeField] private float _forwardSpeed = 10.0f;
    [SerializeField] private float _laneDistanceX = 2.0f;

    [Header("Jump Parameters")]
    [SerializeField] private float _jumpDistanceZ = 5.0f;
    [SerializeField] private float _jumpHeightY = 2.0f;
    [SerializeField] private float _jumpAbortSpeed = 10.0f;

    [Header("Roll Parameters")]
    [SerializeField] private Collider _regularCollider;
    [SerializeField] private Collider _rollCollider;
    [SerializeField] private float _rollDistanceZ = 5.0f;

    private InputManager _inputManager;
    private Vector3 _initialPosition;
    private float _targetPositionX;
    private float _jumpStartZ;
    private float _rollStartZ;

    private float LeftLaneX => _initialPosition.x - _laneDistanceX;
    private float RightLaneX => _initialPosition.x + _laneDistanceX;
    private bool CanJump => !IsJumping;
    private bool CanRoll => !IsRolling;

    public bool IsJumping { get; private set; }
    public bool IsRolling { get; private set; }
    public bool IsDead { get; private set; }
    public float JumpDuration => _jumpDistanceZ / _forwardSpeed;
    public float RollDuration => _rollDistanceZ / _forwardSpeed;
    public float ForwardSpeed => _forwardSpeed;
    public float TravelledDistance => transform.position.z - _initialPosition.z;

    private void Awake()
    {
        _inputManager = InputManager.Instance;
        _initialPosition = transform.position;
        IsDead = false;
        StopRoll();
    }

    private void Update()
    {
        ProcessInput();

        Vector3 position = transform.position;

        position.x = ProcessLaneMovement();
        position.y = ProcessJump();
        position.z = ProcessForwardMovement();

        ProcessRoll();

        transform.position = position;
    }

    private void ProcessInput()
    {
        if (IsDead) return;

        if (_inputManager.GetLeftMoveInput())
            _targetPositionX -= _laneDistanceX;
        if (_inputManager.GetRightMoveInput())
            _targetPositionX += _laneDistanceX;
        if (_inputManager.GetJumpInput() && CanJump)
            StartJump();
        if (_inputManager.GetRollInput() && CanRoll)
            StartRoll();

        _targetPositionX = Mathf.Clamp(_targetPositionX, LeftLaneX, RightLaneX);
    }

    private void StartJump()
    {
        IsJumping = true;
        _jumpStartZ = transform.position.z;
        StopRoll();
        _playerAudioController.PlayJumpSFX();
    }

    private float ProcessJump()
    {
        float deltaY = 0f;
        if (IsJumping)
        {
            float jumpPercent = (transform.position.z - _jumpStartZ) / _jumpDistanceZ;
            if (jumpPercent >= 1)
            {
                StopJump();
            }
            else
            {
                deltaY = Mathf.Sin(Mathf.PI * jumpPercent) * _jumpHeightY;
            }
        }
        float targetPositionY = _initialPosition.y + deltaY;
        return Mathf.MoveTowards(transform.position.y, targetPositionY, _jumpAbortSpeed * Time.deltaTime);
    }

    private void StopJump()
    {
        IsJumping = false;
    }

    private void StartRoll()
    {
        IsRolling = true;
        _rollStartZ = transform.position.z;
        _regularCollider.enabled = false;
        _rollCollider.enabled = true;
        StopJump();
        _playerAudioController.PlayRollSFX();
    }

    private void ProcessRoll()
    {
        if (IsRolling)
        {
            float rollPercent = (transform.position.z - _rollStartZ) / _rollDistanceZ;
            if (rollPercent >= 1)
            {
                StopRoll();
            }
        }
    }

    private void StopRoll()
    {
        IsRolling = false;
        _regularCollider.enabled = true;
        _rollCollider.enabled = false;
    }

    private float ProcessLaneMovement() => Mathf.Lerp(transform.position.x, _targetPositionX, _horizontalSpeed * Time.deltaTime);
    private float ProcessForwardMovement() => transform.position.z + _forwardSpeed * Time.deltaTime;

    public void OnDeath()
    {
        IsDead = true;
        _horizontalSpeed = 0;
        _forwardSpeed = 0;
        StopJump();
        StopRoll();
    }
}
