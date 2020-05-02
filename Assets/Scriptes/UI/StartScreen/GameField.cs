using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GameFieldSeller), typeof(LevelsFieldScore))]
public class GameField : MonoBehaviour
{
    [SerializeField] private int _levelIndex;
    [SerializeField] private Sprite _closePanel;
    [SerializeField] private Sprite _openPanel;
    [SerializeField] private BuffCardsSetter _buffCardsSetter;


    private Button _selfButton;
    private GameFieldSeller _seller;
    private Image _selfPanel;
    private Animator _selfAnimator;
    private AudioSource _audioSours;
    private ChoseGameField _choseGameField;

    public bool IsOpenLevel { get; private set; }
    public LevelsFieldScore ScorrePanel { get; private set; }
    public float Price => _seller.Price;
    public string LevelName => _seller.LevelName;


    private void Awake()
    {
        _selfButton = GetComponent<Button>();
        _seller = GetComponent<GameFieldSeller>();
        ScorrePanel = GetComponent<LevelsFieldScore>();
        _selfPanel = GetComponent<Image>();
        _selfAnimator = GetComponent<Animator>();
        _audioSours = GetComponent<AudioSource>();

        IsOpenLevel = PlayerPrefs.HasKey(LevelName + "_isOpen");
        if (IsOpenLevel)
        {
            OpeningField();
        }
        _selfButton.interactable = IsOpenLevel;
    }

    public void SetChoseGameField(ChoseGameField choser) => _choseGameField = choser;

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
        _choseGameField.LevelPlay(_levelIndex);
    }

    public void PlayOpeningSound() => _audioSours.Play();

    public void OpeningField()
    {
        _seller.CloseSeller();
        _selfButton.interactable = IsOpenLevel;
        _selfButton.onClick.AddListener(LoadLevel);
        ScorrePanel.Open(LevelName);
        _selfPanel.sprite = _openPanel;
        PlayerPrefs.SetString(LevelName + "_isOpen","isOpen");
    }

    public void OpenBuffCardSetter()
    {
        _buffCardsSetter.OpenPanel(LevelName);
    }
}
