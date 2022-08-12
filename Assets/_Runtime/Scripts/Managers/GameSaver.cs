using UnityEngine;

public struct GameData
{
    public int BestScore;
    public int LastScore;
    public int TotalAmountCherries;
}

public class GameSaver : MonoBehaviour
{
    private const string c_bestScoreKey = "BestScore";
    private const string c_lastScoreKey = "LastScore";
    private const string c_totalAmountCherriesKey = "TotalAmountCherries";

    public void SaveGame(GameData gameData)
    {
        PlayerPrefs.SetInt(c_bestScoreKey, gameData.BestScore);
        PlayerPrefs.SetInt(c_lastScoreKey, gameData.LastScore);
        PlayerPrefs.SetInt(c_totalAmountCherriesKey, gameData.TotalAmountCherries);
        PlayerPrefs.Save();
    }

    public GameData LoadGame()
    {
        return new GameData
        {
            BestScore = PlayerPrefs.GetInt(c_bestScoreKey),
            LastScore = PlayerPrefs.GetInt(c_lastScoreKey),
            TotalAmountCherries = PlayerPrefs.GetInt(c_totalAmountCherriesKey)
        };
    }
}
