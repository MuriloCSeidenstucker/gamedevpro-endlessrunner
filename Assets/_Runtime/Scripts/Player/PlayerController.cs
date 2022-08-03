using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _horizontalSpeed = 10.0f;
    [SerializeField] private float _forwardSpeed = 10.0f;
    [SerializeField] private float _laneDistanceX = 2.0f;

    [Header("Jump Parameters")]
    [SerializeField] private float _jumpDistanceZ = 5.0f;
    [SerializeField] private float _jumpHeightY = 2.0f;

    private InputManager _inputManager;
    private Vector3 _initialPosition;
    private float _targetPositionX;
    private float _jumpStartZ;

    public bool IsJumping { get; private set; }
    public float JumpDuration => _jumpDistanceZ / _forwardSpeed;
    private float LeftLaneX => _initialPosition.x - _laneDistanceX;
    private float RightLaneX => _initialPosition.x + _laneDistanceX;

    private void Awake()
    {
        _inputManager = InputManager.Instance;
        _initialPosition = transform.position;
    }

    private void Update()
    {
        ProcessInput();

        Vector3 position = transform.position;

        position.x = ProcessLaneMovement();
        position.y = ProcessJump();
        position.z = ProcessForwardMovement();

        transform.position = position;
    }

    private void ProcessInput()
    {
        if (_inputManager.GetLeftMoveInput())
        {
            _targetPositionX -= _laneDistanceX;
        }
        if (_inputManager.GetRightMoveInput())
        {
            _targetPositionX += _laneDistanceX;
        }
        if (_inputManager.GetJumpInput() && !IsJumping)
        {
            IsJumping = true;
            _jumpStartZ = transform.position.z;
        }

        _targetPositionX = Mathf.Clamp(_targetPositionX, LeftLaneX, RightLaneX);
    }

    private float ProcessJump()
    {
        float deltaY = 0f;
        if (IsJumping)
        {
            float jumpCurrentProgress = transform.position.z - _jumpStartZ;
            float jumpPercent = jumpCurrentProgress / _jumpDistanceZ;
            if (jumpPercent >= 1)
            {
                IsJumping = false;
            }
            else
            {
                deltaY = Mathf.Sin(Mathf.PI * jumpPercent) * _jumpHeightY;
            }
        }
        return _initialPosition.y + deltaY;
    }

    private float ProcessLaneMovement() => Mathf.Lerp(transform.position.x, _targetPositionX, _horizontalSpeed * Time.deltaTime);

    private float ProcessForwardMovement() => transform.position.z + _forwardSpeed * Time.deltaTime;
}
