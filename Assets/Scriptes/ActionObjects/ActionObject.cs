using UnityEngine;

public class ActionObject : MonoBehaviour, IRunable
{
    protected Transform _selfTransform;

    public virtual void Action(Vector2 target)
    {
    }

    public virtual void InAction()
    {
    }

    public void Run()
    {
        throw new System.NotImplementedException();
    }

    public void Run<T>(T value)
    {
        throw new System.NotImplementedException();
    }

    public void Run<T, V>(T valueT, V valueV)
    {
        throw new System.NotImplementedException();
    }

    public virtual void SetPosition(Vector2 position)
    {
        _selfTransform.position = position;
    }

    protected virtual void Work()
    {

    }
}
