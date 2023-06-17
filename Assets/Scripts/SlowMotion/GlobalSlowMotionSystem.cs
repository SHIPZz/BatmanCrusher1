using System.Collections;
using UnityEngine;

public class GlobalSlowMotionSystem : MonoBehaviour
{
    private readonly float _maxTimeScale = 1f;
    private Coroutine _playingSlowMotion;

    public static GlobalSlowMotionSystem Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void StartSlowMotion(float targetTimeScale, float duration, float delayBeforeStart)
    {
        if (_playingSlowMotion != null)
            StopCoroutine(_playingSlowMotion);

        _playingSlowMotion = StartCoroutine(PlayingSlowMotion(targetTimeScale, duration, delayBeforeStart));
    }

    private IEnumerator PlayingSlowMotion(float targetTimeScale, float duration, float delayBeforeStart)
    {
        yield return new WaitForSeconds(delayBeforeStart);
        Time.timeScale = targetTimeScale;
        yield return new WaitForSeconds(Time.timeScale += (_maxTimeScale / duration) * Time.unscaledDeltaTime);
        Time.timeScale = 1;
        yield return null;
    }
}