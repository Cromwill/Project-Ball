using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(GameFieldSeller))]
public class GameField : MonoBehaviour
{
    [SerializeField] private GameFieldData _gameFieldData;
    [SerializeField] private LevelsFieldScorre _scorrePanel;

    private RectTransform _selfTransform;
    private Vector2 _startPosition;
    private Button _selfButton;
    private GameFieldSeller _seller;

    public bool IsOpenLevel { get; private set; }
    public Vector2 Position => _selfTransform.position;
    public float Price => _seller.Price;
    public string Name => _seller.Name;
    public LevelsFieldScorre ScorrePanel => _scorrePanel;

    private void Awake()
    {
        _selfTransform = GetComponent<RectTransform>();
        _selfButton = GetComponent<Button>();
        _seller = GetComponent<GameFieldSeller>();

        IsOpenLevel = PlayerPrefs.HasKey(_seller.Name + "_isOpen");
        if (IsOpenLevel)
            OpeningField();
        _selfButton.interactable = IsOpenLevel;
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

    public bool OpenLevel(int scorre)
    {
        if (_seller.isCanBuy(scorre))
        {
            IsOpenLevel = true;
            OpeningField();
        }
        return IsOpenLevel;
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(_gameFieldData.SceneIndex, LoadSceneMode.Single);
    }

    private void OpeningField()
    {
        _seller.CloseSeller();
        _selfButton.interactable = IsOpenLevel;
        _scorrePanel.Open(_seller.Name);
        PlayerPrefs.SetString(_seller.Name + "_isOpen","isOpen");
    }
}
