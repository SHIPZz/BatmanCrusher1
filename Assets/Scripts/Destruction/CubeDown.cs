using UnityEngine;

public class CubeDown : MonoBehaviour
{
    private readonly float _delay = 1f;
    private PhysicalObject _physicalObject;

    private void Awake()
    {
        _physicalObject = GetComponent<PhysicalObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _physicalObject.TurnOffKinematic();

        Destroy(gameObject, _delay);
    }
}
