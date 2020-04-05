using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(GameFieldSeller), typeof(LevelsFieldScorre))]
public class GameField : MonoBehaviour
{
    [SerializeField] private int _levelIndex;
    [SerializeField] private Sprite _closePanel;
    [SerializeField] private Sprite _openPanel;


    private Button _selfButton;
    private GameFieldSeller _seller;
    private Image _selfPanel;
    private Animator _selfAnimator;
    private AudioSource _audioSours;

    public bool IsOpenLevel { get; private set; }
    public LevelsFieldScorre ScorrePanel { get; private set; }
    public float Price => _seller.Price;
    public string LevelName => _seller.LevelName;


    private void Awake()
    {
        _selfButton = GetComponent<Button>();
        _seller = GetComponent<GameFieldSeller>();
        ScorrePanel = GetComponent<LevelsFieldScorre>();
        _selfPanel = GetComponent<Image>();
        _selfAnimator = GetComponent<Animator>();
        _audioSours = GetComponent<AudioSource>();

        IsOpenLevel = PlayerPrefs.HasKey(LevelName + "_isOpen");
        if (IsOpenLevel)
            OpeningField();
        _selfButton.interactable = IsOpenLevel;
    }

    public bool OpenLevel(float scorre)
    {
        if (_seller.isCanBuy(scorre))
        {
            IsOpenLevel = true;
            _selfAnimator.Play("OpeningGamePanel");
        }
        return IsOpenLevel;
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(_levelIndex, LoadSceneMode.Single);
    }

    public void PlayOpeningSound() => _audioSours.Play();

    public void OpeningField()
    {
        _seller.CloseSeller();
        _selfButton.interactable = IsOpenLevel;
        ScorrePanel.Open(LevelName);
        _selfPanel.sprite = _openPanel;
        PlayerPrefs.SetString(LevelName + "_isOpen","isOpen");
    }
}
