using UnityEngine;
using UnityEngine.UI;

public class LevelsFieldScore : MonoBehaviour
{
    [SerializeField] private Text _scoreViewer;
    [SerializeField] private Text _scorePerSecondViewer;
    [SerializeField] private ScoreFormConverter _scoreFormConverter;
    [SerializeField] private GameObject _gamePanel;

    private string _levelName;
    private SavedScore _savedScore;

    public float Score => _savedScore.Score;
    public float ScorePerSecond => _savedScore.ScorePerSecond;

    private void Update()
    {
        if (_gamePanel.activeSelf)
        {
            Show();
        }
    }

    public void Open(string levelName, float sleepTime = 0)
    {
        _gamePanel.SetActive(true);
        _levelName = levelName;
        FillingScoreDatas(sleepTime);
    }

    public void AddScore(float time)
    {
        if (_savedScore != null && _gamePanel.activeSelf)
            _savedScore.AddScore(time);
    }

    public void ReductionScore(float score) => _savedScore.ReductionScore(score);

    private void FillingScoreDatas(float sleepTime)
    {
        _savedScore = GameDataStorage.GetSavedScore(_levelName);
        if (_savedScore == null)
            _savedScore = new SavedScore(0, 0, 2);

        AddScore(sleepTime);
        Show();
    }

    private void Show()
    {
        AddScore(Time.deltaTime);
        //_scorePerSecondViewer.text = _scoreFormConverter.GetConvertedScorrePerSecond(_savedScore.ScorePerSecond);
        _scoreViewer.text = _scoreFormConverter.GetConvertedScore(_savedScore.Score);
        GameDataStorage.SaveScore(_levelName, _savedScore);
    }
}
