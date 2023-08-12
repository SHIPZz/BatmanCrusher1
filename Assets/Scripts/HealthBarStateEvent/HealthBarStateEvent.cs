using InsaneSystems.HealthbarsKit.UI;
using UnityEngine;

public class HealthBarStateEvent : MonoBehaviour
{
    [SerializeField] private Healthbar _healthBar;
    [SerializeField] private Health _player;
    [SerializeField] private HealthbarsController _healthbarsController;


    private void OnEnable()
    {
        _player.Recovered += OnPlayerRecovered;
    }

    private void OnDisable()
    {
        _player.Recovered -= OnPlayerRecovered;
    }

    private void OnPlayerRecovered(float health)
    {
        _healthBar.gameObject.SetActive(true);
        _healthBar.SetImageFill(health);
        _healthbarsController.gameObject.SetActive(true);
    }
}