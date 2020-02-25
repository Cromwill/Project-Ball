using System;
using UnityEngine;

public class Ball : PoolObject
{
    [SerializeField] private Vector2 _maxVelocity;
    [SerializeField] private int _scoreMultiplier;

    private float _startTime;
    private float _finishTime;

    private void FixedUpdate()
    {
        SpeedControl();
    }

    public int GetPoints()
    {
        _finishTime = Time.time;
        return Mathf.FloorToInt(_finishTime - _startTime) * _scoreMultiplier;
    }

    public override void LeaveThePoll(Vector2 position)
    {
        _startTime = Time.time;
        base.LeaveThePoll(position);
    }

    private void SpeedControl()
    {
        if (Mathf.Abs(_selfRigidbody.velocity.x) > _maxVelocity.x)
            _selfRigidbody.velocity = new Vector2(_maxVelocity.x * Mathf.Sign(_selfRigidbody.velocity.x), _selfRigidbody.velocity.y);

        if (Mathf.Abs(_selfRigidbody.velocity.y) > _maxVelocity.y)
            _selfRigidbody.velocity = new Vector2(_selfRigidbody.velocity.x, _maxVelocity.y * + Mathf.Sign(_selfRigidbody.velocity.y));
    }
}
