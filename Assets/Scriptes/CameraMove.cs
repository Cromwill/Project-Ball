using UnityEngine;

public class CameraMove : ObjectMove
{
    [SerializeField] private float _maxYPosition;
    [SerializeField] private float _minYPosition;

    public override void Move(Vector3 position)
    {
        float direction = Mathf.Sign(position.y - _startInputPosition.y) == -1 ? -1 : 1;
        if (_selfTransform.position.y >= _maxYPosition)
        {
            if (direction == -1)
                base.Move(new Vector3(0, direction, 0));
        }
        else if(_selfTransform.position.y <= _minYPosition)
        {
            if (direction == 1)
                base.Move(new Vector3(0, direction, 0));
        }
        else
        {
            base.Move(new Vector3(0, direction, 0));
        }
    }
}
