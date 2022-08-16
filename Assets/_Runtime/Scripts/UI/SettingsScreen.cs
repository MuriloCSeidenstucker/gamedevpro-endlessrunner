using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScreen : UIScreen
{
    [SerializeField] private AudioController _audioController;

    [Header("Sliders")]
    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;

    [Header("Buttons")]
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _deleteDataButton;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI _deleteDataText;

    private void OnEnable()
    {
        UpdateSliders();
        UpdateDeleteDataButton();
    }

    private void OnDisable()
    {
        _audioController.SaveAudioPreferences();
    }

    private void UpdateSliders()
    {
        _masterSlider.value = _audioController.MasterVolume;
        _musicSlider.value = _audioController.MusicVolume;
        _sfxSlider.value = _audioController.SFXVolume;
    }

    private void UpdateDeleteDataButton()
    {
        _deleteDataButton.interactable = true;
        _deleteDataText.text = "DELETE DATA";
        Color32 buttonColor = new Color32(245, 246, 250, 255);
        _deleteDataButton.image.color = buttonColor;
    }

    public void OnDeleteDataButtonPress()
    {
        _deleteDataButton.interactable = false;
        _deleteDataText.text = "DELETED";
        Color32 buttonColor = new Color32(245, 246, 250, 128);
        _deleteDataButton.image.color = buttonColor;
        GameManager.Instance.DeleteAllData();
    }

    public void OnMasterVolumeChange(float value) => _audioController.MasterVolume = value;

    public void OnBackButtonPress() => ScreenController.Instance.ShowScreen<WaitGameStartScreen>();
}
