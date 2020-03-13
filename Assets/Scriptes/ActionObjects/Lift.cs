using UnityEngine;

public class Lift : MonoBehaviour
{
    [SerializeField] private float _distanceY;
    [SerializeField] private float _speed;
    [SerializeField] private Vector2 _startDirection;
    [SerializeField] private Vector2 _startPosition;

    private Transform _selfTransform;
    private Rigidbody2D _selfRigidBody;
    private Vector2 _direction;

    private void Start()
    {
        _selfTransform = GetComponent<Transform>();
        _selfRigidBody = GetComponent<Rigidbody2D>();
        _direction = _startDirection;
    }

    private void Update()
    {
        if (isCanMove(_direction))
            Move();
    }

    private void Move()
    {
        _selfRigidBody.velocity = _direction * _speed;
    }

    private void ToggleDirection()
    {
        _direction = _direction == Vector2.up ? Vector2.down : Vector2.up;
    }

    private bool isCanMove(Vector2 direction)
    {
        if (_selfTransform.position.y >= _startPosition.y + _distanceY && _direction == Vector2.up)
        {
            ToggleDirection();
            return true;
        }
            
        if (_selfTransform.position.y <= _startPosition.y && _direction == Vector2.down)
        {
            ToggleDirection();
            return true;
        }
        return true;
    }
}
