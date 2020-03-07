using UnityEngine;

public class Receiver : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _objectsPool;
    [SerializeField] private ScorreCounter _scorreCounter;
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
        int points = _scorreCounter.GetScorre(scorre.GetScorre());

        var scorreDrawer = Instantiate(_ballPointsDrawer, _parentForPointsDrawer);
        scorreDrawer.StartDrawing(points, ball.GetPosition());

        _scorreCounter.AddingScorre(points);
        _pool.ReturnObjectToPool(ball);
    }
}
