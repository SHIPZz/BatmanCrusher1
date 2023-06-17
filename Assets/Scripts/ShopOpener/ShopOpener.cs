using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopOpener : MonoBehaviour
{
    [SerializeField] private Button _shopButton;

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }

    private void Open()
    {
        SceneManager.LoadScene(1);
    }

    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }
}