using UnityEngine;
using UnityEngine.SceneManagement;

public class GameField : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Vector2 _scaleOnFocuse;
    [SerializeField] private Vector2 _deltaScale;
    [SerializeField] private float _scaleDistance;
    [SerializeField] private int _sceneIndex;

    private RectTransform _selfTransform;
    private Vector2 _startPosition;

    public Vector2 Position => _selfTransform.position;

    private void Awake()
    {
        _selfTransform = GetComponent<RectTransform>();
    }

    public void PreparationToMove()
    {
        _startPosition = _selfTransform.position;
    }

    public void MoveSelf(Vector2 direction, Vector2 startPosition)
    {
        Vector2 selfTarget = new Vector2(_startPosition.x + direction.x, _selfTransform.position.y);
        _selfTransform.position = Vector2.MoveTowards(_selfTransform.position, selfTarget, _speed * Time.deltaTime);

        var distance = Mathf.Abs(startPosition.x - _selfTransform.position.x);
        var step = _deltaScale.x / _scaleDistance;
        if (distance < _scaleDistance)
        {
            _selfTransform.localScale = _scaleOnFocuse - new Vector2(step * distance, step * distance);
        }
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(_sceneIndex, LoadSceneMode.Single);
    }
}
