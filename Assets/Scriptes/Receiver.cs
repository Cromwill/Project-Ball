using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Receiver : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _objectsPool;
    [SerializeField] private ScoreCounter _scorreCounter;
    [SerializeField] private Effects _ballPointsDrawer;
    [SerializeField] private RectTransform _parentForPointsDrawer;

    private Transform _selfTransform;
    private List<Effects> _effects = new List<Effects>();

    protected IPoolForObjects _pool => (IPoolForObjects)_objectsPool;

    private void Start()
    {
        _selfTransform = GetComponent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IObjectPool ball = collision.GetComponent<IObjectPool>();
        IHaveScorre scorre = collision.GetComponent<IHaveScorre>();
        float points = _scorreCounter.GetScorre(scorre.GetScorre());
        var effectBall = Instantiate(_ballPointsDrawer);
        effectBall.Play(ball.GetPosition());
        _effects.Add(effectBall);
        _scorreCounter.AddingScorre(points);
        _pool.ReturnObjectToPool(ball);

        Effects[] effects = _effects.Where(a => !a.IsEffectPlaying).ToArray();

        for (int i = 0; i < effects.Length; i++)
        {
            _effects.Remove(effects[i]);
            Destroy(effects[i].gameObject);
        }
    }
}
