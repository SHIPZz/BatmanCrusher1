using System;
using System.Collections;
using UnityEngine;

public class HealthRecoveryEvent : MonoBehaviour
{
   [SerializeField] private Player _player;
   
   private Collider _playerCollider;
   private PlayingAdvertisingHandler _playingAdvertisingHandler;

   private void Awake()
   {
       _playerCollider = _player.GetComponent<Collider>();
   }

   private void OnDisable()
    {
        _playingAdvertisingHandler.ShortAdClosed -= SetHealth;
    }

    public void SetPlayingAdvertisingHandler(PlayingAdvertisingHandler playingAdvertisingHandler)
    {
        _playingAdvertisingHandler = playingAdvertisingHandler;
        _playingAdvertisingHandler.ShortAdClosed += SetHealth;
        gameObject.transform.SetParent(null);
    }

    private void SetHealth()
    {
        _player.Health.RecoverHealth();
        _player.gameObject.transform.parent.parent.gameObject.SetActive(true);
        StartCoroutine(MakePlayerImmortalCoroutine());
    }

    private IEnumerator MakePlayerImmortalCoroutine()
    {
        _playerCollider.enabled = false;
        yield return new WaitForSeconds(1.5f);
        _playerCollider.enabled = true;
    }
}