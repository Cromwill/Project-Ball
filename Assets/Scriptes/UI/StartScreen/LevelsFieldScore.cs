using UnityEngine;
using UnityEngine.UI;

public class LevelsFieldScore : MonoBehaviour
{
    [SerializeField] private Text _scorreViewer;
    [SerializeField] private Text _scorrePerSecondViewer;
    [SerializeField] private Text _lableViewer;
    [SerializeField] private StartScreenScoreCounter _totalScorre;
    [SerializeField] private ScoreFormConverter _scoreFormConverter;
    [SerializeField] private GameObject _gamePanel;
    private string _levelName;

    public float Score { get; private set; }
    public float ScorePerSecond { get; private set; }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(_levelName + "_scorre", Score);
    }

    private void Update()
    {
        Show();
    }

    public void Open(string levelName)
    {
        _gamePanel.SetActive(true);
        _levelName = levelName;
        FillingScoreDatas();
        Show();
    }

    public void AddScore(float time)
    {
        Score += ScorePerSecond * time;
        Show();
    }

    public void ReductionScore(float score) => Score -= score;

    private void FillingScoreDatas()
    {
        Score = PlayerPrefs.GetFloat(_levelName + "_scorre");
        ScorePerSecond = PlayerPrefs.GetFloat(_levelName + "_scorrePerSecond");
        Score += _totalScorre.GetSleepTimeSecond() * ScorePerSecond;
    }

    private void Show()
    {
        _scorrePerSecondViewer.text = _scoreFormConverter.GetConvertedScorrePerSecond(ScorePerSecond);
        Score += ScorePerSecond * Time.deltaTime;
        _scorreViewer.text = _scoreFormConverter.GetConvertedScore(Score);
    }
}
