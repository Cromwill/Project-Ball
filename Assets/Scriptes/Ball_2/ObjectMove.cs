using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    [SerializeField] protected float _speed;

    protected Transform _selfTransform;
    protected Vector3 _startObjectPosition;
    protected Vector3 _nextObjectPosition;
    protected Vector3 _startInputPosition;

    private void Start()
    {
        _selfTransform = GetComponent<Transform>();
    }

    public void SetMovePositionData(Vector2 position)
    {
        _startInputPosition = position;
        _startObjectPosition = _selfTransform.position;
    }

    public virtual void Move(Vector3 direction)
    {
        _selfTransform.Translate(direction.normalized * _speed * Time.deltaTime, Space.World);
    }
}
