using UnityEngine;

public class Slowdone : ActionObject
{
    [SerializeField] private Vector2 _maxSpeed;
    [SerializeField] private Direction _direction;

    private void OnTriggerStay2D(Collider2D collision)
    {
        Rigidbody2D rigidbody = collision.GetComponent<Rigidbody2D>();
        rigidbody.velocity = RetentionSpeed(rigidbody.velocity);
    }

    private Vector2 RetentionSpeed(Vector2 speed)
    {
        return _direction == Direction.Horizontal ? MoveHorizontal() : MoveVertical(speed);
    }

    private Vector2 MoveVertical(Vector2 speed)
    {
        if (Mathf.Abs(speed.x) > _maxSpeed.x)
            return new Vector2(_maxSpeed.x * Mathf.Sign(speed.x), speed.y);

        if (Mathf.Abs(speed.y) > _maxSpeed.y)
            return new Vector2(speed.x, _maxSpeed.y * Mathf.Sign(speed.y));
        return speed;
    }

    private Vector2 MoveHorizontal()
    {
        return _maxSpeed;
    }

    public enum Direction
    {
        Vertical,
        Horizontal
    }
}
