using UnityEngine;

public class GameFactory
{
    public GameObject CreateObject(string name)
    {
        GameObject prefab = Resources.Load<GameObject>(name);

        return Object.Instantiate(prefab);
    }

    public GameObject CreateObject(string name, Transform targetPosition)
    {
        GameObject prefab = Resources.Load<GameObject>(name);
        
        return Object.Instantiate(prefab, targetPosition.position, Quaternion.identity);
    }
}