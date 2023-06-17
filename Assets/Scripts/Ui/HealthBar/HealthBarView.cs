using System.Collections;
using UnityEngine;

public class HealthBarView : MonoBehaviour
{
    [SerializeField] private Health _person;

    private readonly WaitForSeconds _delay = new WaitForSeconds(4f);
    private Canvas _playerCanvas;

    private Coroutine _delayCoroutine;

    private void Awake()
    {
        _playerCanvas = GetComponent<Canvas>();
        _playerCanvas.enabled = false;
    }

    private void OnEnable()
    {
        _person.ValueChanged += OnDamaged;
    }

    private void OnDisable()
    {
        _person.ValueChanged -= OnDamaged;
    }

    private void OnDamaged(float damage)
    {
        if (_delayCoroutine != null)
            StopCoroutine(_delayCoroutine);

        _delayCoroutine = StartCoroutine(ShowWithDelay());
    }

    private IEnumerator ShowWithDelay()
    {
        _playerCanvas.enabled = true;
        yield return _delay;
        _playerCanvas.enabled = false;
    }
}
