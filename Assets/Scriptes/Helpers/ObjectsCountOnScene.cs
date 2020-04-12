using System;
using UnityEngine;

class ObjectsCountOnScene
{
    private int _countActionObjectsOnStage;
    private int _countPhysicsObjectOnStage;
    private int _countSpawnObjectsOnStage;
    private int _spawnUpgradeCount;
    private int _scorreUpgrade;
    private int _deleted;

    public ObjectsCountOnScene()
    {
        _countActionObjectsOnStage = 0;
        _countPhysicsObjectOnStage = 0;
        _countSpawnObjectsOnStage = 0;
        _spawnUpgradeCount = 0;
        _scorreUpgrade = 0;
        _deleted = -1;
    }

    public void AddCount(ActionObjectType objectType)
    {
        ChangeCount(objectType, 1);
    }

    public void SetCount(ActionObjectType objectType, int value)
    {
        ChangeCount(objectType, value);
    }

    public int GetCount(ActionObjectType objectType)
    {
        return GetCorrectCounter(objectType);
    }

    private void ChangeCount(ActionObjectType objectType, int value)
    {
        ref int _currentCount = ref GetCorrectCounter(objectType);
        if (_currentCount != _deleted)
            _currentCount += value;
    }

    private ref int GetCorrectCounter(ActionObjectType objectType)
    {
        switch (objectType)
        {
            case ActionObjectType.Action:
                return ref _countActionObjectsOnStage;
            case ActionObjectType.Phisics:
                return ref _countPhysicsObjectOnStage;
            case ActionObjectType.Spawn:
                return ref _countSpawnObjectsOnStage;
            case ActionObjectType.UpgradeScorre:
                return ref _scorreUpgrade;
            case ActionObjectType.UpgradeSpawn:
                return ref _spawnUpgradeCount;
        }

        return ref _deleted;
    }
}

