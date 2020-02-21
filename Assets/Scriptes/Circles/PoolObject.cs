using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class PoolObject : MonoBehaviour
{
    protected Transform _selfTransform;
    protected Rigidbody2D _selfRigidbody;

    private void OnEnable()
    {
        _selfTransform = GetComponent<Transform>();
        _selfRigidbody = GetComponent<Rigidbody2D>();
    }

    public int Index { get; private set; }
    public bool IsInThePool { get; private set; }

    public void LeaveThePoll(Vector2 position)
    {
        IsInThePool = false;
        _selfTransform.position = position;
        _selfRigidbody.simulated = true;
    }

    public void ReturnToPool(Vector2 position)
    {
        IsInThePool = true;
        _selfRigidbody.simulated = false;
        _selfTransform.position = position;
    }

}
