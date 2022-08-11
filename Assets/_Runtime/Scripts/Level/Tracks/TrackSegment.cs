using UnityEngine;

public class TrackSegment : MonoBehaviour
{
    [SerializeField] private Transform _start;
    [SerializeField] private Transform _end;
    [SerializeField] private ObstacleSpawner[] _obstacleSpawners;
    [SerializeField] private DecorationSpawner _decorationSpawner;

    [Header("Pickup Parameters")]
    [SerializeField, Range(0, 1)] private float _pickupSpawnChance = 0.3f;
    [SerializeField] private PickupLineSpawner[] _pickupLineSpawners;

    public Vector3 StartPosition => _start.position;
    public Vector3 EndPosition => _end.position;
    public float Length => Vector3.Distance(EndPosition, StartPosition);
    public float SqrLength => (EndPosition - StartPosition).sqrMagnitude;

    public void SpawnObstacles()
    {
        foreach (var obstacleSpawner in _obstacleSpawners)
        {
            obstacleSpawner.SpawnObstacle();
        }
    }

    public void SpawnPickups()
    {
        if (_pickupLineSpawners.Length > 0 && Random.value <= _pickupSpawnChance)
        {
            Vector3[] skipPositions = new Vector3[_obstacleSpawners.Length];
            for (int i = 0; i < skipPositions.Length; i++)
            {
                skipPositions[i] = _obstacleSpawners[i].transform.position;
            }

            PickupLineSpawner pickupSpawner = _pickupLineSpawners[Random.Range(0, _pickupLineSpawners.Length)];
            pickupSpawner.SpawnPickupLine(skipPositions);
        }
    }

    public void SpawnDecorations() => _decorationSpawner.SpawnDecorations();
}
