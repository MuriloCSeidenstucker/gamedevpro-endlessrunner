using UnityEngine;

[ExecuteAlways]
public class CameraController : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private Vector3 _arm;

    private void LateUpdate()
    {
        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = _player.transform.position + _arm;
        currentPosition.z = targetPosition.z;
        transform.position = currentPosition;
    }
}
