using UnityEngine;

public class TargetView : MonoBehaviour
{
    private void OnEnable()
    {
        Chest.Achieved += OnAchieved;
    }

    private void OnDisable()
    {
        Chest.Achieved -= OnAchieved;
    }

    private void OnAchieved(Chest chest)
    {
        chest.GetComponent<Target>().enabled = false;
    }
}