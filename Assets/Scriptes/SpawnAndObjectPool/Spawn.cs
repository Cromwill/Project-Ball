using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Spawn : ObjectPool, IBuyable
{
    private IPoolForObjects _pool;
    private Vector2 _spawnPositionMin;
    private Vector2 _spawnPositionMax;
    private IRunable _loadLine;
    private float _stratSpawnTime;

    public bool IsUsing { get; set; }

    private void Start()
    {
        _selfTransform = GetComponent<Transform>();
        _loadLine = GetComponentInChildren<IRunable>();
    }

    private IEnumerator SpawnObjects(float time)
    {
        while (IsUsing)
        {
            _pool.GetObject().LeaveThePoll(_selfTransform.position);
            if (_loadLine == null)
                _loadLine = GetComponentInChildren<IRunable>();

                _loadLine.Run(_stratSpawnTime);
            yield return new WaitForSeconds(time);
        }
    }

    public override void StartUsing<IPoolForObjects, Single>(IPoolForObjects pool, Single startSpawnTime)
    {
        _pool = pool as PoolForObjects;
        _stratSpawnTime = Convert.ToSingle(startSpawnTime);
        IsUsing = true;
        StartCoroutine(SpawnObjects(_stratSpawnTime));
    }
}
