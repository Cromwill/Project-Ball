using UnityEngine;

public class ObjectPool : MonoBehaviour, IObjectPool
{
    protected IPoolForObjects _selfPoolForObjects;
    protected Transform _selfTransform;

    public bool IsInThePool { get; private set; }
    public IPoolForObjects SelfObjectForPool { get; set; }

    public Vector2 GetPosition()
    {
        return _selfTransform.position;
    }

    public virtual void LeaveThePoll(Vector2 position)
    {
        IsInThePool = false;
        _selfTransform.position = position;
    }

    public virtual void ReturnToPool(Vector2 position)
    {
        if (_selfTransform == null)
            _selfTransform = GetComponent<Transform>();

        IsInThePool = true;
        _selfTransform.position = position;
    }

    public virtual void StartUsing<T>(T value)
    {
    }

    public virtual void StartUsing<T, U>(T value, U valueU)
    {
    }

    public virtual void StartUsing<T, U, V>(T valueT, U valueU, V valueV)
    {
    }
}
