using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public class ObjectPoolForBalls : ObjectPool<Ball>
{
    public Action BallsGenerated;

    public void Generate(int count)
    {
        GenerateObjectPool(count);
    }

    protected override void GenerateObjectPool(int count)
    {
        _objectArray = new Ball[count];
        for(int i = 0; i < count; i++)
        {
            Ball ball = Instantiate((Ball)_prefab, transform);
            ball.ReturnToPool(_selfTransform.position);
            _objectArray[i] = ball;
        }

        BallsGenerated?.Invoke();
    }

    public Ball GetObject()
    {
        return _objectArray.Where(a => a.IsInThePool).First();
    }

    protected override Ball GetObject(int index)
    {
        if (_objectArray[index].IsInThePool)
            return _objectArray[index];
        else
            return null;
    }

    protected override Ball[] GetObjects(int count)
    {
        Ball[] balls = new Ball[count];
        for(int i = 0; i < count; i++)
        {
            balls[i] = _objectArray.Where(a => a.IsInThePool).First();
        }

        return balls;
    }
}
