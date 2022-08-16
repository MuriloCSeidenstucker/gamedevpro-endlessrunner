using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaitGameStartScreen : UIScreen
{
    [SerializeField] private Button _settingsButton;
    
    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI _bestScoreText;
    [SerializeField] private TextMeshProUGUI _lastScoreText;
    [SerializeField] private TextMeshProUGUI _totalCherriesCountText;

    private void OnEnable()
    {
        _bestScoreText.text = $"Highest Score\n{GameManager.Instance.CurrentSave.BestScore}";
        _lastScoreText.text = $"Last Score\n{GameManager.Instance.CurrentSave.LastScore}";
        _totalCherriesCountText.text = GameManager.Instance.CurrentSave.TotalAmountCherries.ToString();
    }

    public void OnSettingsButtonPress() => ScreenController.Instance.ShowScreen<SettingsScreen>();
    public void OnQuitButtonPress() => GameManager.Instance.QuitGame();
}
