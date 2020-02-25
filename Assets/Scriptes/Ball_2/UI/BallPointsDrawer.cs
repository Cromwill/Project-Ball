using UnityEngine;
using UnityEngine.UI;

public class BallPointsDrawer : MonoBehaviour
{
    [SerializeField] private float _liveTime;
    [SerializeField] private float _speed;

    private Transform _selfTransform;
    private Text _points;

    private void OnEnable()
    {
        _selfTransform = GetComponent<Transform>();
        _points = GetComponent<Text>();
    }

    private void Update()
    {
        if (_liveTime != 0)
            _selfTransform.Translate(Vector2.up * _speed * Time.deltaTime);
        else
            Destroy(gameObject);
    }

    public void StartDrawing(int point)
    {
        _points.text = point.ToString();
    }

    private void Draw(int points)
    {
        _points.text = points.ToString();
    }
}
