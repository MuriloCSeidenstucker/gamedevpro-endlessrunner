using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private AudioClip _pickupSFX;

    private bool _wasCollected;

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
