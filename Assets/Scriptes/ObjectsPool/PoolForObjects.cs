﻿using System;
using UnityEngine;

public class PoolForObjects : MonoBehaviour, IPoolForObjects, IRunable
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

        for(int i = 0; i < _poolObjects.Length; i++)
        {
            IObjectPool obj = (IObjectPool)Instantiate(_objectPrefab);
            obj.SelfObjectForPool = this;
            obj.ReturnToPool(transform.position);
            _poolObjects[i] = obj;
        }
    }

    public virtual IObjectPool GetObject()
    {
        for(int i = 0; i < _poolObjects.Length; i++)
        {
            if (_poolObjects[i].IsInThePool)
                return _poolObjects[i];
        }
        return null;
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

    public void Run()
    {
        throw new NotImplementedException();
    }

    public virtual void Run<T>(T value)
    {
        throw new NotImplementedException();
    }

    public void Run<T, V>(T valueT, V valueV)
    {
        throw new NotImplementedException();
    }
}
