using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _horizontalSpeed = 10.0f;
    [SerializeField] private float _forwardSpeed = 10.0f;

    private void Update()
    {
        Vector3 position = transform.position;

        if (InputManager.Instance.GetLeftMovement())
        {
            position += Vector3.left * _horizontalSpeed * Time.deltaTime;
        }

        if (InputManager.Instance.GetRightMovement())
        {
            position += Vector3.right * _horizontalSpeed * Time.deltaTime;
        }

        position += Vector3.forward * _forwardSpeed * Time.deltaTime;

        transform.position = position;
    }
}
