using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private DecorationSpawner[] _decorationSpawners;

    public void SpawnDecorations()
    {
        foreach (var decorationSpawner in _decorationSpawners)
        {
            decorationSpawner.SpawnDecorations();
        }
    }
}
