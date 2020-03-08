using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameField : MonoBehaviour, IBuyable
{
    [SerializeField] private GameFieldData _gameFieldData;
    [SerializeField] private float _price;
    [SerializeField] private string _levelName;

    private RectTransform _selfTransform;
    private Vector2 _startPosition;
    private Button _selfButton;

    public bool IsOpenLevel { get; private set; }
    public Vector2 Position => _selfTransform.position;

    public float Price => _price;
    public string Name => _levelName;

    private void Awake()
    {
        _selfTransform = GetComponent<RectTransform>();
        _selfButton = GetComponent<Button>();
        IsOpenLevel = PlayerPrefs.GetInt(_levelName + "_isOpen") == 1 ? true : false;
        if (IsOpenLevel)
            OpeningField();
    }

    public void PreparationToMove()
    {
        _startPosition = _selfTransform.position;
    }

    public void MoveSelf(Vector2 direction, Vector2 startPosition)
    {
        Vector2 selfTarget = new Vector2(_startPosition.x + direction.x, _selfTransform.position.y);
        _selfTransform.position = Vector2.MoveTowards(_selfTransform.position, selfTarget, _gameFieldData.Speed * Time.deltaTime);

        var distance = Mathf.Abs(startPosition.x - _selfTransform.position.x);
        var step = _gameFieldData.DeltaScale.x / _gameFieldData.ScaleDistance;
        if (distance < _gameFieldData.ScaleDistance)
            _selfTransform.localScale = _gameFieldData.ScaleOnFocuse - new Vector2(step * distance, step * distance);
    }

    public bool OpenField(float payment)
    {
        if (payment >= Price)
            OpenLevel();

        return IsOpenLevel;
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(_gameFieldData.SceneIndex, LoadSceneMode.Single);
    }

    private void OpenLevel()
    {
        IsOpenLevel = true;
    }

    private void OpeningField()
    {
        var children = GetComponentsInChildren<Transform>();
        if (children != null)
        {
            for (int i = 0; i < children.Length; i++)
            {
                if (children[i].parent == transform) Destroy(children[i].gameObject);
            }
        }
    }
}
