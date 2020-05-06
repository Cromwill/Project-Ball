using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Ball : ObjectPool, IHaveScorre
{
    [SerializeField] private Vector2 _maxVelocity;
    [SerializeField] private int _scoreMultiplier;
    [SerializeField] private BallEffect _ballEffect;
    [SerializeField] private float _maxStayTime;
    [SerializeField] private float _stayForce;


    private Rigidbody2D _selfRigidbody;
    private float _finishTime;
    private float _currentStayTime;
    private Vector2 _previousePosition;
    public float StartTime { get; private set; }

    private void OnEnable()
    {
        _selfRigidbody = GetComponent<Rigidbody2D>();
        _selfTransform = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        ControlSpeed();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var effect = Instantiate(_ballEffect);
        effect.Play(collision.GetContact(0).point);
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

    public override void ReturnToPool(Vector2 position)
    {
        _selfRigidbody.simulated = false;
        _selfRigidbody.velocity = Vector2.zero;
        base.ReturnToPool(position);
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
}
