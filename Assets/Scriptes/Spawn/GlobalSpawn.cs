using UnityEngine;

public class GlobalSpawn : PoolForObjects
{
    [SerializeField] private PoolForObjects _objectPoolForBall;
    [SerializeField] private ActionObjectAnchor _startAnchor;
    [SerializeField] private float _startSpawnTime;

    private int _currentObjectsIndex;

    public override void GeneratePool(int objectCount, bool isFirstGame, string levelName)
    {
        _currentObjectsIndex = -1;
        _poolObjects = new IObjectPool[objectCount];
        if (isFirstGame)
        {
            GenerateObject();
            (_poolObjects[_currentObjectsIndex] as Spawn).LeaveThePoolAndRun(_startAnchor.GetPosition());
            _startAnchor.SetChangeableObject(_poolObjects[_currentObjectsIndex] as IUpgradeable);
            _startAnchor.IsFree = false;
        }
    }

    public override IObjectPool GetObject()
    {
        GenerateObject();
        return _poolObjects[_currentObjectsIndex];
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
