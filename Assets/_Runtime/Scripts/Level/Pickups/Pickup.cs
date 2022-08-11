using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private AudioClip _pickupSFX;
    [SerializeField] private float _rotateSpeed = 100.0f;

    private bool _wasCollected;

    private void Update()
    {
        transform.Rotate(Vector3.up, _rotateSpeed * Time.deltaTime);
    }

    private void PlayPickupSFX() => AudioManager.Instance.PlaySFXOneShot(_pickupSFX);

    public void OnPickedUp()
    {
        if (_wasCollected) return;

        _wasCollected = true;
        GameManager.Instance.IncrementCherryCount();
        PlayPickupSFX();
        Destroy(gameObject);
    }
}
