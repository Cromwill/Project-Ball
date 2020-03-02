using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receiver : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _objectsPool;
    [SerializeField] private ScorreCounter _pointsCounter;
    [SerializeField] private BallPointsDrawer _ballPointsDrawer;
    [SerializeField] private RectTransform _parentForPointsDrawer;

    private Transform _selfTransform;

    protected IPoolForObjects _pool => (IPoolForObjects)_objectsPool;

    private void Start()
    {
        _selfTransform = GetComponent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IObjectPool ball = collision.GetComponent<IObjectPool>();
        IHaveScorre scorre = collision.GetComponent<IHaveScorre>();
        var v = Instantiate(_ballPointsDrawer, _parentForPointsDrawer);
        v.StartDrawing(scorre.GetScorre(), ball.GetPosition());

        _pointsCounter.AddingScorre(scorre.GetScorre());
        _pool.ReturnObjectToPool(ball);
    }
}
