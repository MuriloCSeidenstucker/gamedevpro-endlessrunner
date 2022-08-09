using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private PlayerAnimationController _playerAnimationController;
    [SerializeField] private float _reloadGameDelay = 3.0f;
    [SerializeField] private float _baseScoreMultiplier = 1.0f;
    [SerializeField] private float _countdown = 3.0f;

    private ScreenController _screenController;
    private float _score;

    public int Score => Mathf.RoundToInt(_score);
    public int TravelledDistance => Mathf.RoundToInt(_playerController.TravelledDistance);
    public float Countdown => _countdown;

    protected override void Awake()
    {
        base.Awake();

        Application.targetFrameRate = -1;
        Time.timeScale = 1f;
        _screenController = ScreenController.Instance;
        _screenController.ShowScreen<WaitGameStartScreen>();
        _playerController.enabled = false;
    }

    private void Update()
    {
        if (_playerController.enabled)
            _score += _baseScoreMultiplier * _playerController.ForwardSpeed * Time.deltaTime;
    }

    private IEnumerator ReloadGameCor()
    {
        yield return new WaitForSeconds(_reloadGameDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator StartGameCor()
    {
        _screenController.ShowScreen<InGameScreen>();
        yield return new WaitForSeconds(Countdown);
        _playerAnimationController.OnStart();
    }

    public void PauseGame()
    {
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
