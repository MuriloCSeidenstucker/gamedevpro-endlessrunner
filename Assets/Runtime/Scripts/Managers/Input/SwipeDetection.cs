using System;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    [SerializeField] private float _minDistance = 0.2f;
    [SerializeField, Range(0f, 1f)] private float _directionThreshold = 0.9f;

    private InputManager _inputManager;
    private Vector2 _startPosition;
    private Vector2 _currentPosition;
    private bool _isTouching;
    private bool _isSwipeDetected;

    private void Awake() => _inputManager = InputManager.Instance;

    private void OnEnable()
    {
        _inputManager.OnStartTouch += SwipeStart;
        _inputManager.OnEndTouch += SwipeEnd;
    }

    private void OnDisable()
    {
        if (_inputManager != null)
        {
            _inputManager.OnStartTouch -= SwipeStart;
            _inputManager.OnEndTouch -= SwipeEnd;
        }
    }

    private void Update()
    {
        if (_isTouching)
            UpdateCurrentPosition();
    }

    private void SwipeStart(Vector2 position)
    {
        _isTouching = true;
        _startPosition = position;
    }

    private void UpdateCurrentPosition()
    {
        _currentPosition = _inputManager.GetTouchPosition();
        DetectSwipe();
    }

    private void SwipeEnd(Vector2 position)
    {
        _isTouching = false;
        _isSwipeDetected = false;
    }

    private void DetectSwipe()
    {
        if (Vector3.Distance(_startPosition, _currentPosition) >= _minDistance
            && !_isSwipeDetected)
        {
            _isSwipeDetected = true;
            Vector3 direction = _currentPosition - _startPosition;
            Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
            SwipeDirection(direction2D);
        }
    }

    private void SwipeDirection(Vector2 direction)
    {
        if (Vector2.Dot(Vector2.up, direction) > _directionThreshold)
        {
            _inputManager.SetSwipeUp();
        }
        else if (Vector2.Dot(Vector2.down, direction) > _directionThreshold)
        {
            _inputManager.SetSwipeDown();
        }
        else if (Vector2.Dot(Vector2.left, direction) > _directionThreshold)
        {
            _inputManager.SetSwipeLeft();
        }
        else if (Vector2.Dot(Vector2.right, direction) > _directionThreshold)
        {
            _inputManager.SetSwipeRight();
        }
    }
}
