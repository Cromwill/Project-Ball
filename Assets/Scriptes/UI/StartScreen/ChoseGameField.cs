using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(LevelsShop))]
public class ChoseGameField : MonoBehaviour
{
    [SerializeField] private GameField[] _gameFields;
    [SerializeField] private RewardedVideoAds _videoAds;
    [SerializeField] private StartScreenScoreCounter _totalScoreCounter;
    [SerializeField] private bool _isDebug;
    [SerializeField] private int _adDisplayDelay;

    private bool _canLoadLevel;
    private int _currentLevelIndex;

    private void Start()
    {
        _gameFields = GetComponentsInChildren<GameField>();
    }

    public float GetScoreSum()
    {
        float sum = 0;

        foreach(var field in _gameFields.Where(a => a.IsOpenLevel))
        {
            sum += field.ScorePanel.Score;
        }
        return sum;
    }

    public float GetScorePerSecondSum()
    {
        float sum = 0;

        foreach (var field in _gameFields.Where(a => a.IsOpenLevel))
        {
            sum += field.ScorePanel.ScorePerSecond;
        }
        return sum;
    }

    public float[] GetPercentageOfAmounts()
    {
        GameField[] fields = _gameFields.Where(a => a.IsOpenLevel).ToArray();
        float[] fieldsPercent = new float[fields.Length];

        for(int i = 0; i < fieldsPercent.Length; i++)
        {
            fieldsPercent[i] = fields[i].ScorePanel.Score / GetScoreSum();
        }
        return fieldsPercent;
    }

    public void AddScoreAll(float time)
    {
        foreach(var field in _gameFields.Where(a=> a.IsOpenLevel))
        {
            field.ScorePanel.AddScore(time);
        }
    }

    public void ReduceScoreAll(float[] points)
    {
        for(int i = 0; i < points.Length; i++)
        {
            _gameFields[i].ScorePanel.ReductionScore(points[i]);
        }
    }

    public float GetSleepTime() => _totalScoreCounter.GetSleepTime();

    public void LevelPlay(int levelIndex)
    {
        _currentLevelIndex = levelIndex;
        _canLoadLevel = true;
        int count = GameDataStorage.NumberOfLaunches(levelIndex);


        if (_isDebug || count <= _adDisplayDelay)
        {
            LevelLoad();
        }
        else
        {
            _videoAds.UnityAdsDidFinish += LevelLoad;
            _videoAds.ShowRewardedVideo(true);
        }
    }

    private void LevelLoad()
    {
        if (_canLoadLevel && _currentLevelIndex != 0)
        {
            _videoAds.UnityAdsDidFinish -= LevelLoad;
            _canLoadLevel = false;
            SceneManager.LoadScene(_currentLevelIndex, LoadSceneMode.Single);
        }
    }
}
