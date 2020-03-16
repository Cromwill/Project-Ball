using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(GameFieldSeller))]
public class GameField : MonoBehaviour
{
    [SerializeField] private LevelsFieldScorre _scorrePanel;
    [SerializeField] private Button _openColorPanel;
    [SerializeField] private ColorPanel _colorPanel;
    [SerializeField] private LevelDrawer _levelDrawer;
    [SerializeField] private int _levelIndex;

    private Button _selfButton;
    private GameFieldSeller _seller;

    public bool IsOpenLevel { get; private set; }
    public float Price => _seller.Price;
    public string Name => _seller.Name;
    public LevelsFieldScorre ScorrePanel => _scorrePanel;

    private void Awake()
    {
        _selfButton = GetComponent<Button>();
        _seller = GetComponent<GameFieldSeller>();
        _openColorPanel.interactable = false;
        _levelDrawer.gameObject.SetActive(false);

        IsOpenLevel = PlayerPrefs.HasKey(_seller.Name + "_isOpen");
        if (IsOpenLevel)
            OpeningField();
        _selfButton.interactable = IsOpenLevel;
    }

    private void Start()
    {
        _openColorPanel.onClick.AddListener(OpenColorPanel);
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
        SceneManager.LoadScene(_levelIndex, LoadSceneMode.Single);
    }

    private void OpenColorPanel()
    {
        _colorPanel.OpenPanel(Name, _levelDrawer);
    }

    private void OpeningField()
    {
        _seller.CloseSeller();
        _selfButton.interactable = IsOpenLevel;
        _levelDrawer.gameObject.SetActive(IsOpenLevel);
        _scorrePanel.Open(_seller.Name);
        _openColorPanel.interactable = IsOpenLevel;
        PlayerPrefs.SetString(_seller.Name + "_isOpen","isOpen");
    }
}
