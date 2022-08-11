using UnityEngine;

public class PickupLineSpawner : MonoBehaviour
{
    [SerializeField] private Pickup _pickupPrefab;
    [SerializeField] private Transform _start;
    [SerializeField] private Transform _end;
    [SerializeField] private float _spaceBetweenPickups = 3.0f;

    private bool ShouldSkipPosition(Vector3 currentSpawnPosition, Vector3[] skipPositions)
    {
        foreach (var skipPosition in skipPositions)
        {
            float skipStart = skipPosition.z - _spaceBetweenPickups * 0.5f;
            float skipEnd = skipPosition.z + _spaceBetweenPickups * 0.5f;

            if (currentSpawnPosition.z >= skipStart && currentSpawnPosition.z <= skipEnd)
            {
                return true;
            }
        }

        return false;
    }

    public void SpawnPickupLine(Vector3[] skipPositions)
    {
        Vector3 currentSpawnPosition = _start.position;
        while (currentSpawnPosition.z < _end.position.z)
        {
            if (!ShouldSkipPosition(currentSpawnPosition, skipPositions))
            {
                Pickup pickup = Instantiate(_pickupPrefab, currentSpawnPosition, Quaternion.identity, transform);
            }
            currentSpawnPosition.z += _spaceBetweenPickups;
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 currentSpawnPosition = _start.position;

        while (currentSpawnPosition.z < _end.position.z)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawCube(currentSpawnPosition, Vector3.one);
            currentSpawnPosition.z += _spaceBetweenPickups;
        }
    }
}
