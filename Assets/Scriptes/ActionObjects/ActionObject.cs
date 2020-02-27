using UnityEngine;

public class ActionObject : MonoBehaviour, IBuyable
{
    protected Transform _selfTransform;

    public virtual void SetPosition(Vector2 position)
    {
        if (_selfTransform == null)
            _selfTransform = GetComponent<Transform>();
        _selfTransform.position = position;
    }
}
