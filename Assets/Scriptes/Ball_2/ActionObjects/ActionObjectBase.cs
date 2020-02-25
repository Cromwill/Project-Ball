using UnityEngine;

public class ActionObjectBase : PoolObject
{
    public void Action(Vector2 target)
    {
    }

    public void InAction()
    {
    }

    public void SetPosition(Vector2 position)
    {
        _selfTransform.position = position;
    }

    protected virtual void Work()
    {

    }
}
