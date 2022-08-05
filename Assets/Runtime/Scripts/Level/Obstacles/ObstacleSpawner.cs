using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private Obstacle[] _obstaclePrefabs;

    private Obstacle _currentObstacle;

    internal void SpawnObstacle()
    {
        if (_obstaclePrefabs.Length > 0)
        {
            Obstacle _currentObstacle = Instantiate(_obstaclePrefabs[Random.Range(0, _obstaclePrefabs.Length)], transform);
            _currentObstacle.transform.localPosition = Vector3.zero;
            _currentObstacle.transform.rotation = Quaternion.identity;
            _currentObstacle.SpawnDecorations();
        }
    }
}
