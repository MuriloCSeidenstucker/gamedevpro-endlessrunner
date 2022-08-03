using UnityEngine;

[ExecuteAlways]
public class CameraController : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private float _armZ;

    private void LateUpdate()
    {
        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = _player.transform.position;
        currentPosition.z = targetPosition.z + _armZ;
        transform.position = currentPosition;
    }
}
