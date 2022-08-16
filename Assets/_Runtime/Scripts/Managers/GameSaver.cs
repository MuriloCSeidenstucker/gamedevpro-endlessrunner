using UnityEngine;

public class GameData
{
    public int BestScore;
    public int LastScore;
    public int TotalAmountCherries;
}

public class AudioPreferences
{
    public float MasterVolume;
    public float MusicVolume;
    public float SFXVolume;
}

public class GameSaver : MonoBehaviour
{
    private const string c_bestScoreKey = "BestScore";
    private const string c_lastScoreKey = "LastScore";
    private const string c_totalAmountCherriesKey = "TotalAmountCherries";

    private const string c_masterVolumeKey = "MasterVolume";
    private const string c_musicVolumeKey = "MusicVolume";
    private const string c_sfxVolumeKey = "SFXVolume";

    public GameData CurrentGameData;
    public AudioPreferences CurrentAudioPreferences;

    public void SaveGameData(GameData gameData)
    {
        CurrentGameData = gameData;
        PlayerPrefs.SetInt(c_bestScoreKey, CurrentGameData.BestScore);
        PlayerPrefs.SetInt(c_lastScoreKey, CurrentGameData.LastScore);
        PlayerPrefs.SetInt(c_totalAmountCherriesKey, CurrentGameData.TotalAmountCherries);
        PlayerPrefs.Save();
    }

    public void SaveAudioPreferences(AudioPreferences audioPreferences)
    {
        CurrentAudioPreferences = audioPreferences;
        PlayerPrefs.SetFloat(c_masterVolumeKey, CurrentAudioPreferences.MasterVolume);
        PlayerPrefs.SetFloat(c_musicVolumeKey, CurrentAudioPreferences.MusicVolume);
        PlayerPrefs.SetFloat(c_sfxVolumeKey, CurrentAudioPreferences.SFXVolume);
        PlayerPrefs.Save();
    }

    public void LoadGame()
    {
        CurrentGameData = new GameData
        {
            BestScore = PlayerPrefs.GetInt(c_bestScoreKey, 0),
            LastScore = PlayerPrefs.GetInt(c_lastScoreKey, 0),
            TotalAmountCherries = PlayerPrefs.GetInt(c_totalAmountCherriesKey, 0)
        };

        CurrentAudioPreferences = new AudioPreferences()
        {
            MasterVolume = PlayerPrefs.GetFloat(c_masterVolumeKey, 1f),
            MusicVolume = PlayerPrefs.GetFloat(c_musicVolumeKey, 1),
            SFXVolume = PlayerPrefs.GetFloat(c_sfxVolumeKey, 1)
        };
    }

    public void DeleteAllData() => PlayerPrefs.DeleteAll();
}
