using UnityEngine;

public class Propeller : ActionObject, IBuyable
{
    [SerializeField]
    private PropellerDirection _direction;
    [SerializeField]
    private float _startSpeed;

    private float _actionSpeed;

    private void OnEnable()
    {
        _selfTransform = GetComponent<Transform>();
    }

    public override void SetPosition(Vector2 position)
    {
        if (_selfTransform == null)
            _selfTransform = GetComponent<Transform>();

        _selfTransform.position = position;
    }

    private void Update()
    {
        Work();
    }

    protected override void Work()
    {
        _selfTransform.Rotate(Vector3.forward * (int)_direction * _startSpeed * Time.deltaTime);
    }

    public enum PropellerDirection
    {
        Left = -1,
        Right = 1
    }
}
