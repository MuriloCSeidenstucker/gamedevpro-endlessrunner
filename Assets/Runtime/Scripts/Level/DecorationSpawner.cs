using UnityEngine;

public class DecorationSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _decorationOptions;

    private GameObject _currentDecoration;

    public void SpawnDecorations()
    {
        GameObject prefab = _decorationOptions[Random.Range(0, _decorationOptions.Length)];
        _currentDecoration = Instantiate(prefab, transform);
        _currentDecoration.transform.localPosition = Vector3.zero;
        _currentDecoration.transform.rotation = Quaternion.identity;
    }
}
