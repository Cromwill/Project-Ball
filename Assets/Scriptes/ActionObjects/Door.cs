using UnityEngine;

public class Door : ActionObject
{
    [SerializeField] private float _cicleTime;
    [SerializeField] private Vector3 _angleOfRotetion;
    [SerializeField] private float _speed;
    [SerializeField] private Direction _startDirection;

    private Rigidbody2D _selfRigidbody;
    private Quaternion _startRotation;
    private Quaternion _finishRotation;
    private float _currentTime;
    private Direction _direction;

    private void Start()
    {
        _selfRigidbody = GetComponent<Rigidbody2D>();
        _startRotation = _selfTransform.rotation;
        _finishRotation.eulerAngles = new Vector3(0, 0, _angleOfRotetion.z * (int)_startDirection);
        _currentTime = _cicleTime;
        _direction = _startDirection;
    }

    private void Update()
    {
        if (IsCanRotate())
            Work();
    }

    private void Work()
    {
        _selfRigidbody.angularVelocity = _speed * (int)_direction;
        if (Mathf.Abs(_selfTransform.rotation.z) > Mathf.Abs(_finishRotation.z) && _direction == _startDirection
            || _selfTransform.rotation.z * (int)_startDirection < _startRotation.z && _direction != _startDirection)
        {
            ChangeDirection();
        }
    }

    private void ChangeDirection()
    {
        _currentTime = _cicleTime;
        _selfRigidbody.angularVelocity = 0;
        _selfTransform.rotation = _direction == _startDirection ? _finishRotation : _startRotation;
        _direction = _direction == Direction.Right ? Direction.Left : Direction.Right;
    }

    private bool IsCanRotate()
    {
        return (_currentTime -= Time.deltaTime) < 0;
    }

    private enum Direction
    {
        Left = 1,
        Right = -1
    }
}
