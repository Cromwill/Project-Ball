using System;
using UnityEngine;

class ObjectsCountOnScene
{
    private int _countActionObjectsOnStage = 0;
    private int _countPhysicsObjectOnStage = 0;
    private int _countSpawnObjectsOnStage = 1;
    private int _spawnUpgradeCount = 0;
    private int _scorreUpgrade = 0;

    public ObjectsCountOnScene()
    {
        _countActionObjectsOnStage = 0;
        _countPhysicsObjectOnStage = 0;
        _countSpawnObjectsOnStage = 1;
        _spawnUpgradeCount = 0;
        _scorreUpgrade = 0;
    }

    public void AddCount(ActionObjectType objectType)
    {
        if (objectType != ActionObjectType.Deleted)
        {
            ref int _currentCount = ref GetCorrectCounter(objectType);
            _currentCount++;
        }
    }

    public void SetCount(ActionObjectType objectType, int value)
    {
        ref int _currentCount = ref GetCorrectCounter(objectType);
        _currentCount = value;
    }

    public int GetCount(ActionObjectType objectType)
    {
        return GetCorrectCounter(objectType);
    }

    public void Save()
    {
        GameDataStorage.SaveObjectsOnScene(this);
    }

    private ref int GetCorrectCounter(ActionObjectType objectType)
    {
        switch(objectType)
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

        throw new IndexOutOfRangeException("ObjectType not found");
    }
}

