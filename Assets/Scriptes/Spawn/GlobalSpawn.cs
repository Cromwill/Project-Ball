using UnityEngine;

public class GlobalSpawn : PoolForObjects
{
    [SerializeField] private PoolForObjects _objectPoolForBall;
    [SerializeField] private ActionObjectAnchor _startAnchor;
    [SerializeField] private int _spawnCount;
    [SerializeField] private float _startSpawnTime;

    private int _currentObjectsIndex;

    public override void GeneratePool(int objectCount, bool isFirstGame, string levelName)
    {
        _currentObjectsIndex = -1;
        _poolObjects = new IObjectPool[objectCount];
        GenerateObject();
        (_poolObjects[_currentObjectsIndex] as ObjectPool).LeaveThePoolAndRun(_startAnchor.GetPosition());
        _startAnchor.SetChangeableObject(_poolObjects[_currentObjectsIndex] as IUpgradeable);
        _startAnchor.IsFree = false;
    }

    public override IObjectPool GetObject()
    {
        GenerateObject();
        return _poolObjects[_currentObjectsIndex];
    }

    public override void Save(string level)
    {
        for (int i = 0; i < _poolObjects.Length; i++)
        {
            if (_poolObjects[i] != null)
            {
                CustomPlayerPrefs.SetFloat(level + "_spawnIndex_" + i + "_positionX", _poolObjects[i].GetPosition().x);
                CustomPlayerPrefs.SetFloat(level + "_spawnIndex_" + i + "_positionY", _poolObjects[i].GetPosition().y);
                CustomPlayerPrefs.SetFloat(level + "_spawnIndex_" + i + "_spawnTime", (_poolObjects[i] as Spawn).SpawnTime);
            }
        }
    }

    private void GenerateObject()
    {
        if (IsPoolContainFreeObject())
        {
            var objectPool = Instantiate(_objectPrefab) as Spawn;
            objectPool.FillObject(this, _objectPoolForBall, _startSpawnTime);
            _poolObjects[_currentObjectsIndex] = objectPool;
        }
        else
        {
            Debug.LogError("Pool overflow");
        }
    }

    private bool IsPoolContainFreeObject()
    {
        _currentObjectsIndex++;
        return _currentObjectsIndex < _poolObjects.Length;
    }
}
