using UnityEngine;
using UnityEngine.UI;

public class BallPointsDrawer : MonoBehaviour
{
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _speed;

    private RectTransform _selfTransform;
    private Text _points;

    private void OnEnable()
    {
        _selfTransform = GetComponent<RectTransform>();
        _points = GetComponent<Text>();
    }

    private void Update()
    {
        if (_lifeTime > 0)
        {
            _selfTransform.Translate(Vector2.up * _speed * Time.deltaTime);
            _lifeTime -= Time.deltaTime;
        }

        else
            Destroy(gameObject);
    }

    public void StartDrawing(int point, Vector2 position)
    {
        _selfTransform.position = Camera.main.WorldToScreenPoint(position);
        _points.text = point.ToString();
    }
}
