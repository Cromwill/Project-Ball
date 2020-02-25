using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ActiveBlock : MonoBehaviour, IActiveObject
{
    [SerializeField]
    private float _power;
    [SerializeField]
    private Vector2 _maxVelocity;

    private Rigidbody2D _selfRigidbody;
    private Transform _selfTransform;

    private void Start()
    {
        _selfRigidbody = GetComponent<Rigidbody2D>();
        _selfTransform = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        SpeedControl();
    }

    public void Action(Vector2 target)
    {
        _selfRigidbody.isKinematic = false;
        Vector2 direction = target - (Vector2)_selfTransform.position;
        _selfRigidbody.AddForce(direction * _power, ForceMode2D.Force);
    }

    public void InAction()
    {
        _selfRigidbody.isKinematic = true;
        _selfRigidbody.velocity = Vector2.zero;
    }

    private void SpeedControl()
    {
        if (_selfRigidbody.velocity.x > _maxVelocity.x)
            _selfRigidbody.velocity = new Vector2(_maxVelocity.x, _selfRigidbody.velocity.y);

        if (_selfRigidbody.velocity.y > _maxVelocity.y)
            _selfRigidbody.velocity = new Vector2(_selfRigidbody.velocity.x, _maxVelocity.y);
    }
}
