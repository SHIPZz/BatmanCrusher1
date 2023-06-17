using System;
using UnityEngine;

[RequireComponent(typeof(PhysicalObject))]
public class PlayerInput : MonoBehaviour
{
    [SerializeField] private GrapplingHook _hook;


    public bool IsClicked { get; private set; }

    private readonly int _leftMouseClick = 0;

    private Animator _animator;
    private PhysicalObject _physicalObject;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _physicalObject = GetComponent<PhysicalObject>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(_leftMouseClick))
        {
            _hook.CreateHook();
            _animator.enabled = false;
            _physicalObject.TurnOffKinematic();
        }
        else if (Input.GetMouseButtonUp(_leftMouseClick))
        {
            _hook.DisableHook();
        }
    }
}