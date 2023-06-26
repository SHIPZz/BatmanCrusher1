using System.Collections.Generic;
using UnityEngine;

public class CubePartsSpawningByDyingEnemy : MonoBehaviour
{
    [SerializeField] private EnemyQuantityInZone _enemyQuantity;
    [SerializeField] private List<Rigidbody> _parts = new List<Rigidbody>();

    private void Awake()
    {
        this.SetKinematic(_parts, true);

        SetActive(false);

        SetInitialPosition();
    }

    private void OnEnable() =>
        _enemyQuantity.AllRemoved += Spawn;

    private void OnDisable() =>
        _enemyQuantity.AllRemoved -= Spawn;
    
    private void SetInitialPosition()
    {
        foreach (var part in _parts)
        {
            part.transform.position = transform.position;
        }
    }
    
    private void Spawn()
    {
        GetComponent<MeshRenderer>().enabled = false;
        this.SetKinematic(_parts, false);
        SetActive(true);
        this.SetActive(gameObject,false,0.3f);
    }
    
    private void SetActive(bool isActive)
    {
        for (int i = 0; i < _parts.Count; i++)
        {
            this.SetActive(_parts[i].gameObject, isActive,0);
        }
    }
}