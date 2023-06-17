using UnityEngine;

public class CharacterId : MonoBehaviour
{
   [field: SerializeField] public int Index { get; private set; }
   [field: SerializeField] public int Price { get; private set; }
   [field: SerializeField] public GameObject PricePrefab { get; private set; }
}
