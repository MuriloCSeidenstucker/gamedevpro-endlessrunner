using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _horizontalSpeed = 10.0f;
    [SerializeField] private float _forwardSpeed = 10.0f;
    [SerializeField] private float _laneDistanceX = 2.0f;

    private Vector3 _initialPosition;
    private float _targetPositionX;

    private float LeftLaneX => _initialPosition.x - _laneDistanceX;
    private float RightLaneX => _initialPosition.x + _laneDistanceX;

    private void Awake() => _initialPosition = transform.position;

    private void Update()
    {
        ProcessInput();

        Vector3 position = transform.position;

        position.x = ProcessLaneMovement();
        position.z = ProcessForwardMovement();

        transform.position = position;
    }


    private void ProcessInput()
    {
        if (InputManager.Instance.GetLeftMovement())
        {
            _targetPositionX -= _laneDistanceX;
        }

        if (InputManager.Instance.GetRightMovement())
        {
            _targetPositionX += _laneDistanceX;
        }

        _targetPositionX = Mathf.Clamp(_targetPositionX, LeftLaneX, RightLaneX);
    }

    private float ProcessLaneMovement() => Mathf.Lerp(transform.position.x, _targetPositionX, _horizontalSpeed * Time.deltaTime);

    private float ProcessForwardMovement() => transform.position.z + _forwardSpeed * Time.deltaTime;
}
