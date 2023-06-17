using System;
using UnityEngine;

public class PhysicalObject : MonoBehaviour
{
    [SerializeField] private Rigidbody[] _rigidBodies;

    private void Start()
    {
        TurnOnKinematic();
        //GetComponent<Rigidbody>().isKinematic = false;
    }

    public void TurnOnKinematic()
        => ControlKinematic(true);

    public void TurnOffKinematic()
        => ControlKinematic(false);

    public void EachBody(Action<Rigidbody> eachBodyDelegate)
    {
        foreach (Rigidbody body in _rigidBodies)
            eachBodyDelegate(body);
    }

    private void ControlKinematic(bool isTurn)
    {
        for (int i = 0; i < _rigidBodies.Length; i++)
        {
            _rigidBodies[i].isKinematic = isTurn;
        }
    }
}