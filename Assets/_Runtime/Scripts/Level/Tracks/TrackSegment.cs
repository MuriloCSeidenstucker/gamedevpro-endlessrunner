using UnityEngine;

public class TrackSegment : MonoBehaviour
{
    [SerializeField] private Transform _start;
    [SerializeField] private Transform _end;

    private ObstacleSpawner[] _obstacleSpawners;

    public ObstacleSpawner[] ObstacleSpawners => _obstacleSpawners == null
        ? _obstacleSpawners = GetComponentsInChildren<ObstacleSpawner>()
        : _obstacleSpawners;

    public Vector3 StartPosition => _start.position;
    public Vector3 EndPosition => _end.position;
}
