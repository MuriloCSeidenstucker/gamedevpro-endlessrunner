using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private float _reloadGameDelay = 3.0f;

    private IEnumerator ReloadGameCor()
    {
        yield return new WaitForSeconds(_reloadGameDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnGameOver() => StartCoroutine(ReloadGameCor());
}
