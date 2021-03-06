﻿using UnityEngine;

public class ObjectPool : MonoBehaviour, IObjectPool, IRunable
{
    protected IPoolForObjects _selfPoolForObjects;
    protected Transform _selfTransform;
    protected Vector2 _savePosition;

    public bool IsInThePool { get; protected set; }
    public IPoolForObjects SelfObjectForPool { get; set; }

    public virtual void FillObject<T, V>(T valueT, V valueV) { }

    public Vector2 GetPosition()
    {
        if (_selfTransform != null)
            return _selfTransform.position;
        else
            return _savePosition;
    }

    public virtual void LeaveThePool(Vector2 position)
    {
        IsInThePool = false;
        _selfTransform.position = position;
    }

    public virtual void LeaveThePoolAndRun(Vector2 position) { }

    public virtual void ReturnToPool(Vector2 position)
    {
        if (_selfTransform == null)
            _selfTransform = GetComponent<Transform>();

        IsInThePool = true;
        _selfTransform.position = position;
    }

    public virtual void Run() { }
}
