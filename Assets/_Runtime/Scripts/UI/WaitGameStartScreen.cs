using TMPro;
using UnityEngine;

public class WaitGameStartScreen : UIScreen
{
    [SerializeField] private TextMeshProUGUI _bestScoreText;
    [SerializeField] private TextMeshProUGUI _lastScoreText;
    [SerializeField] private TextMeshProUGUI _totalCherriesCountText;

    private void Start()
    {
        _bestScoreText.text = $"Highest Score\n{GameManager.Instance.CurrentSave.BestScore}";
        _lastScoreText.text = $"Last Score\n{GameManager.Instance.CurrentSave.LastScore}";
        _totalCherriesCountText.text = GameManager.Instance.CurrentSave.TotalAmountCherries.ToString();
    }
}
