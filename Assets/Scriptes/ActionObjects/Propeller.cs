using UnityEngine;

public class Propeller : ActionObject
{
    [SerializeField] private PropellerDirection _direction;
    [SerializeField] private float _startSpeed;

    private float _actionSpeed;
    private Rigidbody2D _selfRigidbody;

    private void Start()
    {
        _selfRigidbody = GetComponent<Rigidbody2D>();
        Work();
    }

    private void Work()
    {
        _selfRigidbody.angularVelocity = (int)_direction * _startSpeed;
    }

    public enum PropellerDirection
    {
        Left = 1,
        Right = -1
    }
}
