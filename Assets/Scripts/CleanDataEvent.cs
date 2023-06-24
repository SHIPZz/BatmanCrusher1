using UnityEngine;

public class CleanDataEvent : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F7) && Input.GetKeyDown(KeyCode.F8))
           DataProvider.Instance.ClearData();
    }
}