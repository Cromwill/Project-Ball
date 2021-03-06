﻿using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Ball : ObjectPool, IHaveScorre
{
    [SerializeField] private Vector2 _maxVelocity;
    [SerializeField] private int _scoreMultiplier;
    [SerializeField] private float _maxTime;

    private Rigidbody2D _selfRigidbody;
    private float _finishTime;
    private Vector2 _poolPosition;

    public float StartTime { get; private set; }

    private void OnEnable()
    {
        _selfRigidbody = GetComponent<Rigidbody2D>();
        _selfTransform = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        ControlSpeed();
        ControlTime();
    }

    public float GetScorre()
    {
        _finishTime = Time.time;
        return (_finishTime - StartTime);
    }

    public override void LeaveThePool(Vector2 position)
    {
        StartTime = Time.time;
        _selfRigidbody.simulated = true;
        base.LeaveThePool(position);
    }

    public void ReturnToPool() => ReturnToPool(_poolPosition);

    public override void ReturnToPool(Vector2 position)
    {
        if (_poolPosition == null || _poolPosition != position)
            _poolPosition = position;

        _selfRigidbody.simulated = false;
        _selfRigidbody.velocity = Vector2.zero;
        base.ReturnToPool(_poolPosition);
    }

    public void Run(Vector2 force)
    {
        _selfRigidbody.velocity = Vector2.zero;
        _selfRigidbody.AddForce(force, ForceMode2D.Impulse);
    }

    private void ControlSpeed()
    {
        if (Mathf.Abs(_selfRigidbody.velocity.x) > _maxVelocity.x)
            _selfRigidbody.velocity = new Vector2(_maxVelocity.x * Mathf.Sign(_selfRigidbody.velocity.x), _selfRigidbody.velocity.y);

        if (Mathf.Abs(_selfRigidbody.velocity.y) > _maxVelocity.y)
            _selfRigidbody.velocity = new Vector2(_selfRigidbody.velocity.x, _maxVelocity.y * + Mathf.Sign(_selfRigidbody.velocity.y));
        _savePosition = _selfTransform.position;
    }

    private void ControlTime()
    {
        if (GetScorre() > _maxTime)
            ReturnToPool();
    }
}
