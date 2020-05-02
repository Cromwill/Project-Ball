using System;
using System.Linq;
using UnityEngine;

public class StartScreenScoreCounter : MonoBehaviour
{
    [SerializeField] private ChoseGameField _choseGameField;
    [SerializeField] private ScoreDrawer _scoreDrawer;
    [SerializeField] private ScoreFormConverter _scoreFormConverter;
    [SerializeField] private InformationPanel _informationPanel;

    [Tooltip("seconds")]
    [SerializeField] private float _maxSleepTime;
    [Tooltip("second")]
    [SerializeField] private float _minTimeAway;

    private LevelsFieldScore[] _levelsScore;

    public float TotalScore { get; private set; }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString("ExitGameTime", DateTime.Now.ToString());
    }

    private void Start()
    {
        GameField[] fields = _choseGameField.GameFields.Where(a => a.IsOpenLevel).ToArray();
        _levelsScore = new LevelsFieldScore[fields.Length];

        for (int i = 0; i < _levelsScore.Length; i++)
        {
            _levelsScore[i] = fields[i].ScorrePanel;
        }

        var timeSleep = GetSleepTimeSecond();
        if (timeSleep > _minTimeAway)
        {
            _informationPanel.gameObject.SetActive(true);

            float cashEarned = 0;
            for(int i = 0; i < _levelsScore.Length; i++)
            {
                cashEarned += timeSleep * _levelsScore[i].ScorePerSecond;
            }
            _informationPanel.Show(((int)timeSleep).ToString() + " sec", _scoreFormConverter.GetConvertedScore(cashEarned));
        }
    }

    private void Update()
    {
        ShowScore();
    }

    public void ReductionScorre(float scorre)
    {
        GameField[] fields = _choseGameField.GameFields.Where(a => a.IsOpenLevel).ToArray();
        float[] percentageOfAmounts = new float[fields.Length];
        float[] subtractions = new float[fields.Length];

        for (int i = 0; i < fields.Length; i++)
        {
            percentageOfAmounts[i] = fields[i].Price / TotalScore;
            subtractions[i] = scorre * percentageOfAmounts[i];
            subtractions[i] = subtractions[i] > fields[i].ScorrePanel.Score ? (int)fields[i].ScorrePanel.Score : subtractions[i];
        }

        if (subtractions.Sum() < scorre)
        {
            float difference = scorre - subtractions.Sum();

            for (int i = 0; i < fields.Length && difference != 0; i++)
            {
                if (fields[i].ScorrePanel.Score - subtractions[i] > difference)
                    subtractions[i] += difference;
                else
                {
                    subtractions[i] += (int)(fields[i].ScorrePanel.Score - subtractions[i]);
                }
            }
        }

        for (int i = 0; i < fields.Length; i++)
        {
            fields[i].ScorrePanel.ReductionScore(subtractions[i]);
        }

        TotalScore -= scorre;
    }

    public void AddScoreAllFields(float time)
    {
        foreach(var field in _levelsScore)
        {
            field.AddScore(time);
        }
    }

    public float GetSleepTimeSecond()
    {
        if (PlayerPrefs.HasKey("ExitGameTime"))
        {
            DateTime dateTime = DateTime.Parse(PlayerPrefs.GetString("ExitGameTime"));

            var sleepTime = DateTime.Now - dateTime;
            if (sleepTime.TotalSeconds > _minTimeAway)
                return sleepTime.TotalSeconds > _maxSleepTime ? _maxSleepTime : (float)sleepTime.TotalSeconds;
            else
                return 0;
        }
        else
            return 0;
    }

    private void ShowScore()
    {
        TotalScore = 0;

        foreach (var field in _levelsScore)
            TotalScore += field.Score;

        _scoreDrawer.Draw(_scoreFormConverter.GetConvertedScore(TotalScore));
    }
}
