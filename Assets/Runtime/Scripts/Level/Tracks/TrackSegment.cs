using UnityEngine;

public class TrackSegment : MonoBehaviour
{
    [SerializeField] private Transform _start;
    [SerializeField] private Transform _end;
    [SerializeField] private ObstacleSpawner[] _obstacleSpawners;
    [SerializeField] private DecorationSpawner _decorationSpawner;

    public ObstacleSpawner[] ObstacleSpawners => _obstacleSpawners;
    public DecorationSpawner DecorationSpawner => _decorationSpawner;
    public Vector3 StartPosition => _start.position;
    public Vector3 EndPosition => _end.position;
    public float Length => Vector3.Distance(EndPosition, StartPosition);
    public float SqrLength => (EndPosition - StartPosition).sqrMagnitude;
}
