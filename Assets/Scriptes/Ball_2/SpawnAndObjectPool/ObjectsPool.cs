using System.Linq;
using UnityEngine;

public abstract class ObjectsPool<T> : MonoBehaviour where T: PoolObject
{
    [SerializeField] protected T _prefab;
    [SerializeField] protected T[] _objectArray;

    protected Transform _selfTransform;

    public virtual bool IsObjectEmpty()
    {
        return _objectArray.Where(a => a.IsInThePool).Count() <= 0;
    }

    private void OnEnable()
    {
        _selfTransform = GetComponent<Transform>();
    }

    protected virtual void GenerateObjectPool(int count)
    {
        _objectArray = new T[count];
        for (int i = 0; i < count; i++)
        {
            T poolObject = Instantiate(_prefab, transform);
            poolObject.Index = i;
            poolObject.ReturnToPool(_selfTransform.position);
            _objectArray[i] = poolObject;
        }
    }

    public virtual T GetObject()
    {
        return _objectArray.Where(a => a.IsInThePool).First();
    }

    public virtual T GetObject(int index)
    {
        if (_objectArray[index].IsInThePool)
            return _objectArray[index];
        else
            return null;
    }
    public virtual T[] GetObjects(int count)
    {
        T[] balls = new T[count];
        for (int i = 0; i < count; i++)
        {
            balls[i] = _objectArray.Where(a => a.IsInThePool).First();
        }

        return balls;
    }

    public virtual void ReturnToPool(T obj)
    {
        obj.ReturnToPool(_selfTransform.position);
    }

}
