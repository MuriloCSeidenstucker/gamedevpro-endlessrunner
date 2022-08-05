using System.Collections.Generic;
using UnityEngine;

public class EndlessTrackGenerator : MonoBehaviour
{
    [SerializeField] private PlayerController _player;

    [Header("Prefabs")]
    [SerializeField] private TrackSegment _firstTrackPrefab;
    [SerializeField] private TrackSegment[] _easyTrackPrefabs;
    [SerializeField] private TrackSegment[] _hardTrackPrefabs;
    [SerializeField] private TrackSegment[] _rewardTrackPrefabs;

    [Header("Endless Generation Parameters")]
    [SerializeField] private int _initialTrackCount = 10;
    [SerializeField] private int _minTracksInFrontOfPlayer = 3;
    [SerializeField] private float _minDistToConsiderInsideTrack = 3.0f;

    [Header("Level Difficulty Parameters")]
    [SerializeField, Range(0f, 1f)] private float _spawnPercentHardTrack = 0.3f;
    [SerializeField] private int _minTracksBeforeReward = 10;
    [SerializeField] private int _maxTracksBeforeReward = 20;
    [SerializeField] private int _minRewardTrackCount = 1;
    [SerializeField] private int _maxRewardTrackCount = 2;

    private List<TrackSegment> _currentSegments = new List<TrackSegment>();
    private int _rewardTracksLeftToSpawn;
    private int _trackSpawnedAfterLastReward;
    private bool _isSpawningRewardTracks;


    private void Start()
    {
        SpawnTrackSegment(_firstTrackPrefab, null);
        SpawnTracks(_initialTrackCount);
    }

    private void Update()
    {
        UpdateTracks();
    }

    private void UpdateTracks()
    {
        int playerTrackIndex = FindTrackIndexWithPlayer();
        if (playerTrackIndex < 0) return;
        SpawnNewTracksIfNeeded(playerTrackIndex);
        DestroyTracksBehindPlayer(playerTrackIndex);
    }

    private void SpawnTracks(int trackCount)
    {
        TrackSegment previousTrack = _currentSegments.Count > 0
            ? _currentSegments[_currentSegments.Count - 1]
            : null;

        for (int i = 0; i < trackCount; i++)
        {
            TrackSegment track = GetRandomTrack();
            previousTrack = SpawnTrackSegment(track, previousTrack);
        }
    }

    private TrackSegment GetRandomTrack()
    {
        TrackSegment[] trackList = null;
        if (_isSpawningRewardTracks)
        {
            trackList = _rewardTrackPrefabs;
        }
        else
        {
            trackList = Random.value <= _spawnPercentHardTrack ? _hardTrackPrefabs : _easyTrackPrefabs;
        }
        return trackList[Random.Range(0, trackList.Length)];
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

        UpdateRewardTracking();

        return trackInstance;
    }

    private void UpdateRewardTracking()
    {
        if (_isSpawningRewardTracks)
        {
            _rewardTracksLeftToSpawn--;
            if (_rewardTracksLeftToSpawn <= 0)
            {
                _isSpawningRewardTracks = false;
                _trackSpawnedAfterLastReward = 0;
            }
        }
        else
        {
            _trackSpawnedAfterLastReward++;
            int requiredTracksBeforeReward = Random.Range(_minTracksBeforeReward, _maxTracksBeforeReward + 1);
            if (_trackSpawnedAfterLastReward >= requiredTracksBeforeReward)
            {
                _isSpawningRewardTracks = true;
                _rewardTracksLeftToSpawn = Random.Range(_minRewardTrackCount, _maxRewardTrackCount + 1);
            }
        }
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

    private void SpawnNewTracksIfNeeded(int playerTrackIndex)
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
