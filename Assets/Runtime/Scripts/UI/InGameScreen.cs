using System.Collections;
using TMPro;
using UnityEngine;

public class InGameScreen : UIScreen
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _travelledDistText;
    [SerializeField] private TextMeshProUGUI _countdownText;

    private GameManager _gameManager;
    private float _countdown;
    private bool _wasCountdownPerformed;

    private void Awake()
    {
        _gameManager = GameManager.Instance;
        _countdown = _gameManager.Countdown;

        if (!_wasCountdownPerformed || _countdown > 0f)
            StartCoroutine(PerformCountdownCor());
    }

    private void LateUpdate()
    {
        _scoreText.text = $"Score: {_gameManager.Score:D6}";
        _travelledDistText.text = $"{_gameManager.TravelledDistance:D4}m";
    }

    private IEnumerator PerformCountdownCor()
    {
        float timeToStart = Time.time + _countdown;
        yield return null;

        _countdownText.gameObject.SetActive(true);
        while (Time.time <= timeToStart)
        {
            float remainingTime = timeToStart - Time.time;
            int remainingTimeInt = Mathf.FloorToInt(remainingTime);
            _countdownText.text = (remainingTimeInt + 1).ToString();
            float percent = remainingTime - remainingTimeInt;
            _countdownText.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, percent);
            yield return null;
        }

        _countdownText.gameObject.SetActive(false);
        _wasCountdownPerformed = true;
    }
}
