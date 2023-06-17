using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [field: SerializeField] public int MaxValue { get; set; }

    public static event Action<Health> Dead;
    public event Action<float> ValueChanged;
    public event Action ValueZeroReached;
    public event Action<float> Recovered;

    public int InitialValue { get; set; }

    private void Awake()
    {
        InitialValue = MaxValue;
    }

    public void TakeDamage(float damage)
    {
        MaxValue = Mathf.Clamp(MaxValue - (int)damage, 0, MaxValue);

        if (MaxValue == 0)
        {
            Dead?.Invoke(this);
            ValueZeroReached?.Invoke();
        }

        ValueChanged?.Invoke(MaxValue);
    }

    public void RecoverHealth()
    {
        MaxValue = InitialValue;

        Recovered?.Invoke(MaxValue);
    }
}