using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class PoolObject : MonoBehaviour
{
    protected Transform _selfTransform;
    protected Rigidbody2D _selfRigidbody;
    private int _index;

    private void OnEnable()
    {
        _selfTransform = GetComponent<Transform>();
        _selfRigidbody = GetComponent<Rigidbody2D>();
    }

    public int Index
    {
        get
        {
            return Index; 
        }
        set
        {
            if (_index == 0 && value != 0)
                _index = value;
        }
    }
    public bool IsInThePool { get; private set; }

    public virtual void LeaveThePoll(Vector2 position)
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
