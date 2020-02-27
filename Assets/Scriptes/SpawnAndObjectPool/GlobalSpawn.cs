using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSpawn : PoolForObjects
{
    [SerializeField] private PoolForObjects _objectPoolForBall;
    [SerializeField] private ActionObjectAnchor _startAnchor;
    [SerializeField] private int _spawnCount;
    [SerializeField] private float _startSpawnTime;

    private int _currentObjectsIndex = 0;

    public void SetSpawnObject(Vector2 position)
    {
        if (_currentObjectsIndex < _poolObjects.Length)
            _currentObjectsIndex++;
    }

    public override void Run<Int32>(Int32 value)
    {
        GeneratePool(Convert.ToInt32(value));
        _poolObjects[_currentObjectsIndex].LeaveThePoll(_startAnchor.GetPosition());
        _poolObjects[_currentObjectsIndex].StartUsing(_objectPoolForBall, _startSpawnTime);
        _currentObjectsIndex++;
    }

    public override IObjectPool GetObject()
    {
        for (int i = 0; i < _poolObjects.Length; i++)
        {
            if (_poolObjects[i].IsInThePool)
            {
                _poolObjects[i].StartUsing(_objectPoolForBall, _startSpawnTime);
                return _poolObjects[i];
            }
        }
        return null;
    }
}
