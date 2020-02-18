using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float _power;
    [SerializeField]
    private float _startMass, _actionMass;
    [SerializeField]
    private Vector2 _maxVelocity;

    private Transform _selfTransform;
    private Rigidbody2D _rigidbody2D;
    private bool _isAction;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _selfTransform = GetComponent<Transform>();
        _isAction = false;
    }

    private void FixedUpdate()
    {
        if (_rigidbody2D.velocity.x > _maxVelocity.x)
            _rigidbody2D.velocity = new Vector2(_maxVelocity.x, _rigidbody2D.velocity.y);
        if (_rigidbody2D.velocity.y > _maxVelocity.y)
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _maxVelocity.y);
        if(!_isAction)
        {
            _rigidbody2D.mass = _startMass;
        }
    }

    public void Action(Vector3 target)
    {
        _isAction = true;
        _rigidbody2D.mass = _actionMass;
        _rigidbody2D.AddForce((target - _selfTransform.position).normalized * _power, ForceMode2D.Force);
    }

    public void InAction()
    {
        _isAction = false;
        _rigidbody2D.mass = _startMass;
    }

    public void ReturnToPool()
    {

    }
}
