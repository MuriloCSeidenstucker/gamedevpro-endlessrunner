using System;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : Singleton<InputManager>
{
    public event Action<Vector2, float> OnStartTouch;
    public event Action<Vector2, float> OnEndTouch;

    private PlayerInputActions _inputActions;
    private Camera _mainCamera;

    public bool SwipeUp { get; private set; }
    public bool SwipeLeft { get; private set; }
    public bool SwipeDown { get; private set; }
    public bool SwipeRight { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        _inputActions = new PlayerInputActions();
        _mainCamera = Camera.main;
    }

    private void Start()
    {
        _inputActions.Player.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        _inputActions.Player.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
    }

    private void LateUpdate() => ResetSwipe();

    private void OnEnable() => _inputActions.Player.Enable();

    private void OnDisable() => _inputActions.Player.Disable();

    private void StartTouchPrimary(InputAction.CallbackContext ctx)
    {
        OnStartTouch?.Invoke(Utils.ScreenToWorld(_mainCamera, _inputActions.Player.PrimaryPosition.ReadValue<Vector2>()), (float)ctx.startTime);
    }

    private void EndTouchPrimary(InputAction.CallbackContext ctx)
    {
        OnEndTouch?.Invoke(Utils.ScreenToWorld(_mainCamera, _inputActions.Player.PrimaryPosition.ReadValue<Vector2>()), (float)ctx.time);
    }

    private void ResetSwipe()
    {
        SwipeUp = false;
        SwipeLeft = false;
        SwipeDown = false;
        SwipeRight = false;
    }

    public void SetSwipeUp() => SwipeUp = true;
    public void SetSwipeLeft() => SwipeLeft = true;
    public void SetSwipeDown() => SwipeDown = true;
    public void SetSwipeRight() => SwipeRight = true;
    public bool GetLeftMoveInput() => _inputActions.Player.LeftMove.WasPressedThisFrame() || SwipeLeft;
    public bool GetRightMoveInput() => _inputActions.Player.RightMove.WasPressedThisFrame() || SwipeRight;
    public bool GetJumpInput() => _inputActions.Player.Jump.WasPressedThisFrame() || SwipeUp;
}
