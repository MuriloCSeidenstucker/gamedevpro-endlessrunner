using System;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    [SerializeField] private float _minDistance = 0.2f;
    [SerializeField] private float _maxTime = 1.0f;
    [SerializeField, Range(0f, 1f)] private float _directionThreshold = 0.9f;

    private InputManager _inputManager;
    private Vector2 _startPosition;
    private Vector2 _endPosition;
    private float _startTime;
    private float _endTime;

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

    private void SwipeStart(Vector2 position, float time)
    {
        _startPosition = position;
        _startTime = time;
    }

    private void SwipeEnd(Vector2 position, float time)
    {
        _endPosition = position;
        _endTime = time;
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        if (Vector3.Distance(_startPosition, _endPosition) >= _minDistance
            && (_endTime - _startTime) <= _maxTime)
        {
            Vector3 direction = _endPosition - _startPosition;
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
