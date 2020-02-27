using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Ball : ObjectPool, IHaveScorre
{
    [SerializeField] private Vector2 _maxVelocity;
    [SerializeField] private int _scoreMultiplier;

    private Rigidbody2D _selfRigidbody;
    private float _startTime;
    private float _finishTime;

    private void OnEnable()
    {
        _selfRigidbody = GetComponent<Rigidbody2D>();
        _selfTransform = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        ControlSpeed();
    }

    public int GetScorre()
    {
        _finishTime = Time.time;
        return Mathf.FloorToInt(_finishTime - _startTime) * _scoreMultiplier;
    }

    public override void LeaveThePoll(Vector2 position)
    {
        _startTime = Time.time;
        _selfRigidbody.simulated = true;
        base.LeaveThePoll(position);
    }

    public override void ReturnToPool(Vector2 position)
    {
        _selfRigidbody.simulated = false;
        _selfRigidbody.velocity = Vector2.zero;
        base.ReturnToPool(position);
    }

    private void ControlSpeed()
    {
        if (Mathf.Abs(_selfRigidbody.velocity.x) > _maxVelocity.x)
            _selfRigidbody.velocity = new Vector2(_maxVelocity.x * Mathf.Sign(_selfRigidbody.velocity.x), _selfRigidbody.velocity.y);

        if (Mathf.Abs(_selfRigidbody.velocity.y) > _maxVelocity.y)
            _selfRigidbody.velocity = new Vector2(_selfRigidbody.velocity.x, _maxVelocity.y * + Mathf.Sign(_selfRigidbody.velocity.y));
    }
}
