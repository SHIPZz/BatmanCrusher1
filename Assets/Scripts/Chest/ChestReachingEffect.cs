using UnityEngine;

[RequireComponent(typeof(Chest))]
public class ChestReachingEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _moneyEffect;

    public void OnAnimationEvent()
    {
        _moneyEffect.Play();
    }
}