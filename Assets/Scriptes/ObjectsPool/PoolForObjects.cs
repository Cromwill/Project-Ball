using System;
using System.Linq;
using UnityEngine;

public class PoolForObjects : MonoBehaviour, IPoolForObjects
{
    [SerializeField] protected MonoBehaviour _objectPrefab;

    protected IObjectPool _object => (IObjectPool)_objectPrefab;
    protected IObjectPool[] _poolObjects;

    private void OnValidate()
    {
        if (_objectPrefab is IObjectPool)
            return;

        Debug.LogError(_objectPrefab.name + " needs to implement " + nameof(IObjectPool));
        _objectPrefab = null;
    }

    public virtual void GeneratePool(int objectCount)
    {
        _poolObjects = new IObjectPool[objectCount];

        for (int i = 0; i < _poolObjects.Length; i++)
        {
            IObjectPool obj = (IObjectPool)Instantiate(_objectPrefab);
            obj.SelfObjectForPool = this;
            obj.ReturnToPool(transform.position);
            _poolObjects[i] = obj;
        }
    }

    public virtual IObjectPool GetObject()
    {
        return _poolObjects.First(a=> a.IsInThePool);
    }

    public virtual IObjectPool GetObject(int index)
    {
        try
        {
            return _poolObjects[index];
        }
        catch(IndexOutOfRangeException)
        {
            Debug.LogError("Index" + index + "out of range array PoolOpjects. Max index -"+ (_poolObjects.Length - 1));
        }
        return null;
    }

    public void ReturnObjectToPool(IObjectPool obj)
    {
        obj.ReturnToPool(transform.position);
    }
}
