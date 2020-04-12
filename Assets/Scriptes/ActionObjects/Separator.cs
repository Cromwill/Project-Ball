using System.Linq;
using UnityEngine;

public class Separator : ActionObject
{
    [SerializeField] private Vector2 _force;
    [SerializeField] private float _stopTime;

    private Collider2D _selfCollider;
    private float _currentTime;
    private IPoolForObjects _pool;

    private void Start()
    {
        _selfCollider = GetComponent<Collider2D>();
        _currentTime = 0;
        if (_force != Vector2.zero)
            _pool = FindObjectsOfType<PoolForObjects>().Where(a=> a.GetComponent<Separator>() != null).First();
    }

    private void Update()
    {
        if (_currentTime > 0)
            TimerRuning();
    }

    private void TimerRuning()
    {
        _selfCollider.enabled = (_currentTime -= Time.deltaTime) <= 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Ball>() != null)
        {
            Ball ball = collision.gameObject.GetComponent<Ball>();
            Ball secondBall = _pool.GetObject() as Ball;

            secondBall.LeaveThePool(_selfTransform.position);
            secondBall.Run(_force);
            ball.Run(new Vector2(_force.x * -1, _force.y));

            _currentTime = _stopTime;
            _selfCollider.enabled = false;
        }
    }
}
