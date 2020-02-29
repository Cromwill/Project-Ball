using UnityEngine;

public class Propeller : ActionObject
{
    [SerializeField] private PropellerDirection _direction;
    [SerializeField] private float _startSpeed;

    private float _actionSpeed;

    private void Update()
    {
        Work();
    }

    private void Work()
    {
        _selfTransform.Rotate(Vector3.forward * (int)_direction * _startSpeed * Time.deltaTime);
    }

    public enum PropellerDirection
    {
        Left = 1,
        Right = -1
    }
}
