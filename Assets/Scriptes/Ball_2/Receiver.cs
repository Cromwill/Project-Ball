using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receiver : MonoBehaviour
{
    [SerializeField] private ObjectsPoolForBalls _objectsPool;
    [SerializeField] private PointsCounter _pointsCounter;
    [SerializeField] private BallPointsDrawer _ballPointsDrawer;

    private Transform _selfTransform;

    private void Start()
    {
        _selfTransform = GetComponent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Ball ball = collision.GetComponent<Ball>();
        var v = Instantiate(_ballPointsDrawer);
        v.transform.position = _selfTransform.position;
        v.StartDrawing(ball.GetPoints());

        _pointsCounter.AddingPoints(ball.GetPoints());
        _objectsPool.ReturnToPool(ball);
    }
}
