using UnityEngine;

public class Ball : PoolObject
{
    [SerializeField]
    private float _power;
    [SerializeField]
    private Vector2 _maxVelocity;

    private bool _iFound;

    private void FixedUpdate()
    {
        SpeedControl();
    }

    public void Action(Vector3 target)
    {
        Vector2 direction = (target - _selfTransform.position).normalized;
        if (IsCanMove(target))
        {
            _selfRigidbody.AddForce(direction * _power, ForceMode2D.Force);
            _selfRigidbody.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
            //_selfTransform.Translate(direction * Time.deltaTime, Space.World);
        }
    }

    public void InAction()
    {
        _selfRigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        _selfRigidbody.velocity = Vector2.zero;
    }

    private void SpeedControl()
    {
        if (_selfRigidbody.velocity.x > _maxVelocity.x)
            _selfRigidbody.velocity = new Vector2(_maxVelocity.x, _selfRigidbody.velocity.y);

        if (_selfRigidbody.velocity.y > _maxVelocity.y)
            _selfRigidbody.velocity = new Vector2(_selfRigidbody.velocity.x, _maxVelocity.y);
    }

    private bool IsCanMove(Vector2 target)
    {
        float distance = Vector2.Distance(_selfTransform.position, target);

        return distance > 0.02f;
    }
}
