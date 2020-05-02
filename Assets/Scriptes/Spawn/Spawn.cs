using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class Spawn : ActionObject, IObjectPool
{
    [SerializeField] private SpawnTimeViewer _spawnTimeViewer;
    [SerializeField] private float _minSpawnTime;
    private IPoolForObjects _runablePool;
    private IRunable<float> _loadLine;

    public bool IsUsing { get; set; }
    public float SpawnTime { get; private set; }

    public bool IsInThePool { get; protected set; }
    public IPoolForObjects SelfObjectForPool { get; private set; }

    private void OnEnable()
    {
        _selfTransform = GetComponent<Transform>();
        _loadLine = GetComponentInChildren<IRunable<float>>();
        var spawnTime = Instantiate(_spawnTimeViewer, FindObjectOfType<Canvas>().transform);
        _spawnTimeViewer = spawnTime.GetComponent<SpawnTimeViewer>();
    }

    public void LeaveThePoolAndRun(Vector2 position)
    {
        IsInThePool = false;
        _selfTransform.position = position;
        position.y += 0.2f;
        _spawnTimeViewer.SetPosition(position);
        _spawnTimeViewer.ShowValue(SpawnTime.ToString("0.00"));
        IsUsing = true;
        StartCoroutine(SpawnObjects());
    }

    public void FillObject(IPoolForObjects selfPool, IPoolForObjects runablePool, float startTime)
    {
        SelfObjectForPool = selfPool;
        _runablePool = runablePool;
        SpawnTime = startTime;
    }

    public override void Upgrade(float value)
    {
        SpawnTime -= value;
        if (SpawnTime < _minSpawnTime)
            SpawnTime = _minSpawnTime;
        _spawnTimeViewer.ShowValue(SpawnTime.ToString("0.00"));
    }

    public void UpdateSpawnTimeViewerPosition()
    {
        Vector3 position = _selfTransform.position;
        position.y += 0.2f;
        _spawnTimeViewer.SetPosition(position);
    }

    public void LeaveThePool(Vector2 position) => throw new System.NotImplementedException("SpawnObject not leave the Pool without Run. Use method <<LeaveThePoolAndRun()>>");
    public void ReturnToPool(Vector2 position) => throw new System.NotImplementedException("SpawnObject can't return to the Pool");

    public Vector2 GetPosition() => _selfTransform.position;

    public override bool IsCanUpgrade()
    {
        return SpawnTime > _minSpawnTime;
    }

    private IEnumerator SpawnObjects()
    {
        while (IsUsing)
        {
            _runablePool.GetObject().LeaveThePool(_selfTransform.position);
            if (_loadLine == null)
                _loadLine = GetComponentInChildren<IRunable<float>>();
            _loadLine.Run(SpawnTime);
            yield return new WaitForSeconds(SpawnTime);
        }
    }
}
