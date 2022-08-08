using TMPro;
using UnityEngine;

public class InGameScreen : UIScreen
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _travelledDistText;

    private GameManager _gameManager;

    private void Awake() => _gameManager = GameManager.Instance;

    private void LateUpdate()
    {
        _scoreText.text = $"Score: {_gameManager.Score:D6}";
        _travelledDistText.text = $"{_gameManager.TravelledDistance:D4}m";
    }
}
