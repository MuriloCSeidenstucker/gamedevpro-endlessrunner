using UnityEngine;

public class DecorationSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _decorationOptions;

    public GameObject CurrentDecoration { get; private set; }

    public void SpawnDecorations()
    {
        if (_decorationOptions.Length > 0)
        {
            GameObject prefab = _decorationOptions[Random.Range(0, _decorationOptions.Length)];
            CurrentDecoration = Instantiate(prefab, transform);
            CurrentDecoration.transform.localPosition = Vector3.zero;
            CurrentDecoration.transform.rotation = Quaternion.identity;
        }
    }
}
