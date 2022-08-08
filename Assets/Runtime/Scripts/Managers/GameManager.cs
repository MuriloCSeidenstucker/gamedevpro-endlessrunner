using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private float _reloadGameDelay = 3.0f;
    [SerializeField] private float _baseScoreMultiplier = 1.0f;

    private float _score;

    public int Score => Mathf.RoundToInt(_score);
    public int TravelledDistance => Mathf.RoundToInt(_playerController.TravelledDistance);

    protected override void Awake()
    {
        base.Awake();

        Time.timeScale = 1f;
        ScreenController.Instance.ShowScreen<InGameScreen>();
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

    public void OnGameOver() => StartCoroutine(ReloadGameCor());

    public void PauseGame()
    {
        Time.timeScale = 0f;
        ScreenController.Instance.ShowScreen<PauseGameScreen>();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        ScreenController.Instance.ShowScreen<InGameScreen>();
    }
}
