using System;
using System.Text;
using UnityEngine;

public class StartScreenScoreCounter : MonoBehaviour
{
    [SerializeField] private ChoseGameField _chooser;
    [SerializeField] private ScoreDrawer _scoreDrawer;
    [SerializeField] private ScoreFormConverter _scoreFormConverter;
    [SerializeField] private InformationPanel _informationPanel;

    [Tooltip("seconds")]
    [SerializeField] private float _maxSleepTime;
    [Tooltip("second")]
    [SerializeField] private float _minTimeAway;

    #region ExitGame
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString("ExitGameTime", DateTime.Now.ToString());
    }

    private void OnApplicationFocus(bool focus)
    {
        if(!focus)
            PlayerPrefs.SetString("ExitGameTime", DateTime.Now.ToString());
    }

    private void OnApplicationPause(bool pause)
    {
        if(pause)
            PlayerPrefs.SetString("ExitGameTime", DateTime.Now.ToString());
    }
    #endregion
    private void Start()
    {
        var timeSleep = GetSleepTime();
        if (timeSleep > _minTimeAway)
        {
            TimeSpan sleepInterval = TimeSpan.FromSeconds(timeSleep);
            string timeSleepText = (sleepInterval.Minutes + " min " + sleepInterval.Seconds + " sec").ToUpper();
            _informationPanel.Show(timeSleepText, _scoreFormConverter.GetConvertedScore(_chooser.GetScorePerSecondSum() * timeSleep));
        }
    }
    private void Update()
    {
        ShowScore();
    }

    public void ReductionScore(float score)
    {
        float[] percentageOfAmounts = _chooser.GetPercentageOfAmounts();
        float[] subtractions = new float[percentageOfAmounts.Length];

        for (int i = 0; i < subtractions.Length; i++)
        {
            subtractions[i] = score * percentageOfAmounts[i];
        }

        _chooser.ReduceScoreAll(subtractions);
    }

    public void AddScoreAllFields(float time) => _chooser.AddScoreAll(time);

    public float GetSleepTime()
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
        _scoreDrawer.Draw(_scoreFormConverter.GetConvertedScore(_chooser.GetScoreSum()));
    }
}
