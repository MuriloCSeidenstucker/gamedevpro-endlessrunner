using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    [SerializeField] private float _swipeRange;
    
    private Vector2 _startTouchPosition;
    private Vector2 _currentTouchPosition;

    private Vector2 Swipe()
    {
        Vector2 distance = Vector2.zero;

        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                _startTouchPosition = Input.GetTouch(0).position;
            }

            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                _currentTouchPosition = Input.GetTouch(0).position;
                distance = _currentTouchPosition - _startTouchPosition;
            }
        }

        return distance;
    }

    private bool SwipeLeft()
    {
        Vector2 distance = Swipe();
        return distance.x < -_swipeRange;
    }

    private bool SwipeRight()
    {
        Vector2 distance = Swipe();
        return distance.x > _swipeRange;
    }

    public bool GetLeftMovement()
    {
        return Input.GetKeyDown(KeyCode.A) || SwipeLeft();
    }

    public bool GetRightMovement()
    {
        return Input.GetKeyDown(KeyCode.D) || SwipeRight();
    }
}
