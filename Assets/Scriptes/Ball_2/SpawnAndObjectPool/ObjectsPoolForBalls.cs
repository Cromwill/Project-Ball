using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public class ObjectsPoolForBalls : ObjectsPool<Ball>
{
    [SerializeField] private int _objectsCount;

    public Action BallsGenerated;

    private void Start()
    {
        Generate(_objectsCount);
    }


    public void Generate(int count)
    {
        GenerateObjectPool(count);
    }

    protected override void GenerateObjectPool(int count)
    {
        base.GenerateObjectPool(count);
        OnBallsGenerated();
    }

    private void OnBallsGenerated()
    {
        BallsGenerated?.Invoke();
    }
}
