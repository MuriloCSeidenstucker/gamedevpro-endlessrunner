using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameScreen : UIScreen
{
    [SerializeField] private ScreenAudioController _screenAudioController;
    [SerializeField] private Button _pauseButton;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _cherryText;
    [SerializeField] private TextMeshProUGUI _travelledDistText;
    [SerializeField] private TextMeshProUGUI _countdownText;

    private GameManager _gameManager;
    private int _countdown;
    private bool _wasCountdownPerformed;

    private void Awake()
    {
        _gameManager = GameManager.Instance;
        _countdown = _gameManager.StartGameCountdown;

        if (!_wasCountdownPerformed || _countdown > 0f)
            StartCoroutine(PerformCountdownCor());
    }

    private void LateUpdate()
    {
        _scoreText.text = $"Score: {_gameManager.Score:D6}";
        _cherryText.text = _gameManager.CherryCount.ToString();
        _travelledDistText.text = $"{_gameManager.TravelledDistance:D4}m";

        if (!_wasCountdownPerformed)
            _pauseButton.interactable = false;
        else
            _pauseButton.interactable = true;
    }

    private IEnumerator PerformCountdownCor()
    {
        float timeToStart = Time.time + _countdown;
        yield return null;

        _countdownText.gameObject.SetActive(true);
        int previousRamainingTimeInt = _countdown;
        while (Time.time <= timeToStart)
        {
            float remainingTime = timeToStart - Time.time;
            int remainingTimeInt = Mathf.FloorToInt(remainingTime);
            _countdownText.text = (remainingTimeInt + 1).ToString();

            if (previousRamainingTimeInt != remainingTimeInt)
            {
                _screenAudioController.PlayCountdownAudio();
            }

            previousRamainingTimeInt = remainingTimeInt;

            float percent = remainingTime - remainingTimeInt;
            _countdownText.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, percent);
            yield return null;
        }

        _screenAudioController.PlayCountdownFinishedAudio();

        _countdownText.gameObject.SetActive(false);
        _wasCountdownPerformed = true;
    }
}
