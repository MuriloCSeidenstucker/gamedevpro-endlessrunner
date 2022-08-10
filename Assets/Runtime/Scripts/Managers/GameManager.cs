using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private PlayerAnimationController _playerAnimationController;
    [SerializeField] private MusicPlayer _musicPlayer;
    [SerializeField] private float _reloadGameDelay = 3.0f;
    [SerializeField] private float _baseScoreMultiplier = 1.0f;
    [SerializeField] private int _countdown = 3;

    private ScreenController _screenController;
    private float _score;
    private bool _isDead;

    public int Score => Mathf.RoundToInt(_score);
    public int TravelledDistance => Mathf.RoundToInt(_playerController.TravelledDistance);
    public int Countdown => _countdown;

    protected override void Awake()
    {
        base.Awake();

        SetWaitForStartGameState();
    }

    private void Update()
    {
        UpdateScore();
    }

    private void SetWaitForStartGameState()
    {
        _musicPlayer.PlayStartMenuMusic();
        _screenController = ScreenController.Instance;
        _screenController.ShowScreen<WaitGameStartScreen>();
        _playerController.enabled = false;
    }

    private void UpdateScore()
    {
        if (!_playerController.enabled) return;

        _score += _baseScoreMultiplier * _playerController.ForwardSpeed * Time.deltaTime;
    }

    private IEnumerator ReloadGameCor()
    {
        _isDead = true;
        yield return new WaitForSeconds(seconds: 0.5f);
        _musicPlayer.PlayGameOverMusic();
        yield return new WaitForSeconds(_reloadGameDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator StartGameCor()
    {
        _musicPlayer.PlayMainTrackMusic();
        _screenController.ShowScreen<InGameScreen>();
        yield return new WaitForSeconds(Countdown);
        _playerAnimationController.OnStart();
    }

    public void PauseGame()
    {
        if (_isDead) return;
        Time.timeScale = 0f;
        _screenController.ShowScreen<PauseGameScreen>();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        _screenController.ShowScreen<InGameScreen>();
    }

    public void StartGame() => StartCoroutine(StartGameCor());
    public void OnGameOver() => StartCoroutine(ReloadGameCor());

}
