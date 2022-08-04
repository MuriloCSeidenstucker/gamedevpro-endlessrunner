using System.Collections.Generic;
using UnityEngine;

public class EndlessTrackGenerator : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private TrackSegment[] _segmentPrefabs;
    [SerializeField] private TrackSegment _firstTrackPrefab;

    [Header("Endless Generation Parameters")]
    [SerializeField] private int _initialTrackCount = 10;
    [SerializeField] private int _minTracksInFrontOfPlayer = 3;
    [SerializeField] private float _minDistToConsiderInsideTrack = 3.0f;

    private List<TrackSegment> _currentSegments = new List<TrackSegment>();

    private void Start()
    {
        if (_segmentPrefabs.Length > 0)
        {
            SpawnTrackSegment(_firstTrackPrefab, null);
            SpawnTracks(_initialTrackCount);
        }
    }

    private void Update()
    {
        int playerTrackIndex = FindTrackIndexWithPlayer();
        if (playerTrackIndex < 0) return;
        SpawnTracksInFrontOfPlayer(playerTrackIndex);
        DestroyTracksBehindPlayer(playerTrackIndex);
    }

    private void SpawnTracks(int trackCount)
    {
        TrackSegment previousTrack = _currentSegments.Count > 0
            ? _currentSegments[_currentSegments.Count - 1]
            : null;

        for (int i = 0; i < trackCount; i++)
        {
            TrackSegment track = _segmentPrefabs[Random.Range(0, _segmentPrefabs.Length)];
            previousTrack = SpawnTrackSegment(track, previousTrack);
        }
    }

    private TrackSegment SpawnTrackSegment(TrackSegment track, TrackSegment previousTrack)
    {
        TrackSegment trackInstance = Instantiate(track, transform);

        if (previousTrack != null)
        {
            trackInstance.transform.position = previousTrack.EndPosition + (trackInstance.transform.position - trackInstance.StartPosition);
        }
        else
        {
            trackInstance.transform.localPosition = Vector3.zero;
        }

        foreach (var obstacleSpawner in trackInstance.ObstacleSpawners)
        {
            obstacleSpawner.SpawnObstacle();
        }

        _currentSegments.Add(trackInstance);
        return trackInstance;
    }

    private int FindTrackIndexWithPlayer()
    {
        int playerTrackIndex = -1;
        for (int i = 0; i < _currentSegments.Count; i++)
        {
            TrackSegment track = _currentSegments[i];
            if (_player.transform.position.z >= (track.StartPosition.z + _minDistToConsiderInsideTrack)
                && _player.transform.position.z <= track.EndPosition.z)
            {
                playerTrackIndex = i;
                break;
            }
        }

        return playerTrackIndex;
    }

    private void SpawnTracksInFrontOfPlayer(int playerTrackIndex)
    {
        int tracksInFrontOfPlayer = _currentSegments.Count - (playerTrackIndex + 1);
        if (tracksInFrontOfPlayer < _minTracksInFrontOfPlayer)
        {
            SpawnTracks(_minTracksInFrontOfPlayer - tracksInFrontOfPlayer);
        }
    }

    private void DestroyTracksBehindPlayer(int playerTrackIndex)
    {
        for (int i = 0; i < playerTrackIndex; i++)
        {
            TrackSegment track = _currentSegments[i];
            Destroy(track.gameObject);
        }
        _currentSegments.RemoveRange(0, playerTrackIndex);
    }
}
