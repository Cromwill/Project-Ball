using UnityEngine;

public class MoverAroundLocation : MonoBehaviour
{
    [SerializeField] protected float _speed;
    [SerializeField] private float _maxYPosition;
    [SerializeField] private float _minYPosition;

    protected Transform _selfTransform;
    protected Vector3 _nextObjectPosition;
    protected Vector3 _startInputPosition;

    private void Start()
    {
        _selfTransform = GetComponent<Transform>();
    }

    public void SetMovePositionData(Vector2 position)
    {
        _startInputPosition = position;
    }

    public virtual void Move(Vector3 position)
    {
        float direction = position.y - _startInputPosition.y;
        TranslateSelf(new Vector3(0, direction, 0));
    }

    private bool IsPositionInRange(float value, float maxRange, float minRange)
    {
        return value >= minRange && value <= maxRange;
    }

    private void TranslateSelf(Vector2 direction)
    {
        Vector2 translation = direction.normalized * _speed * Time.deltaTime;
        if (IsPositionInRange(_selfTransform.position.y + translation.y, _maxYPosition, _minYPosition))
            _selfTransform.Translate(direction.normalized * _speed * Time.deltaTime, Space.World);
    }
}
