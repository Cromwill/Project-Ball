using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Spawn : ObjectPool, IBuyable
{
    [SerializeField] private float _price;
    [SerializeField] private string _name;

    private IPoolForObjects _runablePool;
    private IRunable<float> _loadLine;
    private float _stratSpawnTime;

    public bool IsUsing { get; set; }
    public float Price => _price;
    public string Name => _name;

    private void OnEnable()
    {
        _selfTransform = GetComponent<Transform>();
        _loadLine = GetComponentInChildren<IRunable<float>>();
    }

    public override void LeaveThePoolAndRun(Vector2 position)
    {
        IsInThePool = false;
        _selfTransform.position = position;
        IsUsing = true;
        StartCoroutine(SpawnObjects(_stratSpawnTime));
    }

    public void FillObject(IPoolForObjects selfPool, IPoolForObjects runablePool, float startTime)
    {
        _selfPoolForObjects = selfPool;
        _runablePool = runablePool;
        _stratSpawnTime = startTime;
    }

    private IEnumerator SpawnObjects(float time)
    {
        while (IsUsing)
        {
            _runablePool.GetObject().LeaveThePool(_selfTransform.position);
            if (_loadLine == null)
                _loadLine = GetComponentInChildren<IRunable<float>>();
                _loadLine.Run(_stratSpawnTime);
            yield return new WaitForSeconds(time);
        }
    }
}
