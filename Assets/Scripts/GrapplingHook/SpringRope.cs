using UnityEngine;

public class SpringRope : MonoBehaviour
{
    private float _value;
    private float _velocity;
    private float _normalVelocity;
    private readonly float _strength;
    private readonly float _damper;
    private readonly float _target = 0;

    public SpringRope(float strength, float damper, float velocity)
    {
        _strength = strength;
        _damper = damper;
        _normalVelocity = velocity;
    }

    public float Value => _value;

    public void Change(float deltaTime)
    {
        var direction = _target - _value >= 0 ? 1f : -1f;
        var force = Mathf.Abs(_target - _value) * _strength;

        _velocity += (force * direction - _velocity * _damper) * deltaTime;
        _value += _velocity * deltaTime;
    }

    public void Reset()
    {
        _velocity = 0f;
        _value = 0f;
    }

    public void ChangeVelocity()
    {
        _velocity = _normalVelocity;
    }
}

