using System;
using System.Collections;
using InsaneSystems.HealthbarsKit.UI;
using UnityEngine;

public class HealthRecoveryEvent : MonoBehaviour
{
   [SerializeField] private Player _player;
 [SerializeField]  private Healthbar _healthBar;
   
   private Collider _playerCollider;
   private PlayingAdvertisingHandler _playingAdvertisingHandler;
   private PlayerMovement _playerMovement;

   private void Awake()
   {
       _playerCollider = _player.GetComponent<Collider>();
       _playerMovement = _player.GetComponent<PlayerMovement>();
   }

   private void OnDisable()
    {
        _playingAdvertisingHandler.RewardedDeathClosed -= SetHealth;
    }

    public void SetPlayingAdvertisingHandler(PlayingAdvertisingHandler playingAdvertisingHandler)
    {
        _playingAdvertisingHandler = playingAdvertisingHandler;
        _playingAdvertisingHandler.RewardedDeathClosed += SetHealth;
        gameObject.transform.SetParent(null);
    }

    private void SetHealth()
    {
        _player.Health.RecoverHealth();
        _healthBar.SetHealthValue((float)_player.Health.InitialValue);

        _player.gameObject.transform.parent.parent.gameObject.SetActive(true);
        StartCoroutine(MakePlayerImmortalCoroutine());
    }

    private IEnumerator MakePlayerImmortalCoroutine()
    {
        _playerCollider.enabled = false;
        _playerMovement.SetVelocity(Vector3.zero);
        yield return new WaitForSeconds(2.5f);
        _playerCollider.enabled = true;
    }
}