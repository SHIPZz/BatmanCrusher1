using System;
using System.Collections;
using UnityEngine;

public class EnemyDestruction : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionEffect;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private ParticleSystem _emojiEffect;
    [SerializeField] private GameObject[] _oldBodyParts;

    public event Action<Transform> Destroyed;

    private readonly float _initalCameraZoom = 35;
    private readonly float _targetCameraZoom = 60;
    private readonly float _targetTimeScale = 0.1f;
    private readonly float _duration = 5f;
    private readonly float _delay = 1.5f;

    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _explosionEffect.gameObject.SetActive(false);
        _audioSource.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _health.ValueZeroReached += Demolish;
    }

    private void OnDisable()
    {
        _health.ValueZeroReached -= Demolish;
    }

    public void Demolish()
    {
        SetVisualEnableFalse();

        PlayVisualEffect();

        Destroy(gameObject, _delay);

        Destroyed?.Invoke(this.transform);
    }

    private void SetVisualEnableFalse()
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<SkinnedMeshRenderer>().enabled = false;
        GetComponent<Animator>().enabled = false;
    }

    private void PlayVisualEffect()
    {
        _explosionEffect.gameObject.SetActive(true);
        _audioSource.gameObject.SetActive(true);

        CameraScale.Instance.StartZoom(_initalCameraZoom, _targetCameraZoom);
        GlobalSlowMotionSystem.Instance.StartSlowMotion(_targetTimeScale, _duration, 0);

        _audioSource.Play();
        _explosionEffect.Play();
        _emojiEffect.Play();
    }
}