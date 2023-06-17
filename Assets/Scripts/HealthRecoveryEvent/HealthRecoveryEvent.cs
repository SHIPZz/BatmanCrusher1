using UnityEngine;
using UnityEngine.UI;

public class HealthRecoveryEvent : MonoBehaviour
{
    [SerializeField] private Health _player;

    private void OnEnable()
    {
        PlayingAdvertisingHandler.ShortAdClosed += SetHealth;
        gameObject.transform.SetParent(null);
    }

    private void OnDisable()
    {
        PlayingAdvertisingHandler.ShortAdClosed -= SetHealth;
    }

    private void SetHealth()
    {
        _player.RecoverHealth();
        _player.gameObject.SetActive(true);
    }
}