using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GameFieldSeller), typeof(LevelsFieldScore))]
public class GameField : MonoBehaviour
{
    [SerializeField] private GameFieldData _gameFieldData;

    private GameFieldComponentsHelper _components;

    public bool IsOpenLevel { get; private set; }
    public LevelsFieldScore ScorePanel => _components.ScorePanel;
    public float Price => _components.Seller.Price;
    public string LevelName => _components.Seller.ObjectName;
    public int LevelIndex => _gameFieldData.LevelIndex;

    private void Awake()
    {
        ComponentInitialization();
        IsOpenLevel = PlayerPrefs.HasKey(LevelName + "_isOpen");

        if (IsOpenLevel)
            OpenField();

        _components.SelfButton.interactable = IsOpenLevel;
    }

    public bool OpenLevel() => _components.IsCanOpenLevel();

    public void LoadLevel() => _components.Chooser.LevelPlay(_gameFieldData.LevelIndex);

    public void PlayOpeningSound() => _components.AudioSours.Play();

    public void OpenField()
    {
        _components.OpenGameField(LoadLevel, _gameFieldData.OpenPanelSprite);
        ScorePanel.Open(LevelName, _components.Chooser.GetSleepTime());
        IsOpenLevel = true;
        PlayerPrefs.SetString(LevelName + "_isOpen", "isOpen");
    }

    public void GameFieldOpenning() => _components.SelfAnimator.Play("OpeningGamePanel");

    private void ComponentInitialization()
    {
        _components = new GameFieldComponentsHelper
        {
            SelfButton = GetComponent<Button>(),
            Seller = GetComponent<GameFieldSeller>(),
            SelfPanel = GetComponent<Image>(),
            SelfAnimator = GetComponent<Animator>(),
            AudioSours = GetComponent<AudioSource>(),
            ScorePanel = GetComponent<LevelsFieldScore>(),
            Chooser = GetComponentInParent<ChoseGameField>()
        };
    }
}
