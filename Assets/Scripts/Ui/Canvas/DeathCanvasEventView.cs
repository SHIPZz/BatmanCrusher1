using System;
using UnityEngine;

public class DeathCanvasEventView : MonoBehaviour
{
    private Player _player;

    public event Action CanvasTurned;

    private void Start()
    {
        _player.Dead += OnDead;
    }

    private void OnDisable()
    {
        _player.Dead -= OnDead;
    }

    public void SetPlayer(Player player) =>
        _player = player;

    private void OnDead()
    {
        CanvasTurned?.Invoke();
    }
}