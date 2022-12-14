using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private PlayerAnimationController _playerAnimationController;
    [SerializeField] private AudioController _audioController;
    [SerializeField] private MusicPlayer _musicPlayer;

    [Header("Gameplay parameters")]
    [SerializeField] private float _startPlayerSpeed = 10.0f;
    [SerializeField] private float _maxPlayerSpeed = 15.0f;
    [SerializeField] private float _timeToMaxSpeedSeconds = 300.0f;
    [SerializeField] private float _reloadGameDelay = 3.0f;
    [SerializeField, Range(0, 5)] private int _startGameCountdown = 3;

    [Header("Score parameters")]
    [SerializeField] private float _baseScoreMultiplier = 1.0f;

    private ScreenController _screenController;
    private GameSaver _gameSaver;
    private float _score;
    private float _startGameTime;
    private bool _isGameRunning;

    public int Score => Mathf.RoundToInt(_score);
    public int TravelledDistance => Mathf.RoundToInt(_playerController.TravelledDistance);
    public int StartGameCountdown => _startGameCountdown;
    public GameData CurrentSave => _gameSaver.CurrentGameData;
    public int CherryCount { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        StartCoroutine(SetWaitForStartGameState());
    }

    private void Update()
    {
        UpdateScore();
    }

    private IEnumerator SetWaitForStartGameState()
    {
        _gameSaver = GetComponent<GameSaver>();
        _gameSaver.LoadGame();
        _screenController = ScreenController.Instance;
        _screenController.ShowScreen<WaitGameStartScreen>();
        _playerController.enabled = false;
        _isGameRunning = false;
        float startGameDelay = 0.1f;
        yield return new WaitForSeconds(startGameDelay);
        _musicPlayer.PlayStartMenuMusic();
    }

    private void UpdateScore()
    {
        if (!_isGameRunning) return;

        float timePercent = (Time.time - _startGameTime) / _timeToMaxSpeedSeconds;
        _playerController.ForwardSpeed = Mathf.Lerp(_startPlayerSpeed, _maxPlayerSpeed, timePercent);
        float extraScoreMultiplier = 1 + timePercent;
        _score += _baseScoreMultiplier * extraScoreMultiplier * _playerController.ForwardSpeed * Time.deltaTime;
    }

    private IEnumerator ReloadGameCor()
    {
        yield return new WaitForSeconds(seconds: 0.5f);
        _musicPlayer.PlayGameOverMusic();
        yield return new WaitForSeconds(_reloadGameDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator StartGameCor()
    {
        _musicPlayer.PlayMainTrackMusic();
        _screenController.ShowScreen<InGameScreen>();
        yield return new WaitForSeconds(StartGameCountdown);
        yield return StartCoroutine(_playerAnimationController.OnStartCor());

        _playerController.enabled = true;
        _playerController.ForwardSpeed = _startPlayerSpeed;
        _startGameTime = Time.time;
        _isGameRunning = true;
    }

    public void PauseGame()
    {
        if (!_isGameRunning) return;
        Time.timeScale = 0f;
        _screenController.ShowScreen<PauseGameScreen>();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        _screenController.ShowScreen<InGameScreen>();
    }

    public void OnGameOver()
    {
        _gameSaver.SaveGameData(new GameData()
        {
            BestScore = CurrentSave.BestScore < Score ? Score : CurrentSave.BestScore,
            LastScore = Score,
            TotalAmountCherries = CurrentSave.TotalAmountCherries + CherryCount
        });

        _isGameRunning = false;
        _playerController.ForwardSpeed = 0f;
        StartCoroutine(ReloadGameCor());
    }

    public void DeleteAllData()
    {
        _gameSaver.DeleteAllData();
        _gameSaver.LoadGame();
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void StartGame() => StartCoroutine(StartGameCor());
    public void IncrementCherryCount() => CherryCount++;
}
