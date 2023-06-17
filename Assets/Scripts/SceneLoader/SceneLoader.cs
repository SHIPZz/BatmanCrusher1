using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoaderExtension
{
    public static void Load(this MonoBehaviour monoBehaviour, int index, float delay) =>
        monoBehaviour.StartCoroutine(LoadCoroutine(index, delay));

    private static IEnumerator LoadCoroutine(int index, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(index);
    }
}