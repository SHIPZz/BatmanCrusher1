using Agava.YandexGames;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Initialize : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(Init());
    }

    private IEnumerator Init()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        StartGame();
        yield break;
#endif
        yield return YandexGamesSdk.Initialize();
        YandexGamesSdk.CallbackLogging = true;
        StartGame();
    }
    
    private void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}