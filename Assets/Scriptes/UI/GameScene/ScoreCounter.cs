using UnityEngine;

[RequireComponent(typeof(LevelData))]
public class ScoreCounter : MonoBehaviour, IUpgradeable
{
    [SerializeField] private ScoreDrawer _scoreDrawer;
    [SerializeField] private float _scoreFactor;
    [SerializeField] private ScoreFormConverter _scoreFormConvert;
    [SerializeField] private float _increaseTime;

    private ScorrePerTime _scorePerTime;
    private string _levelName;

    public float Score { get; private set; }
    public float ScorePerSecond { get; private set; }

    private void Start()
    {
        _levelName = GetComponent<LevelData>().LevelName;
        var savedScorre = GameDataStorage.GetSavedScore();

        Score = savedScorre.Score;
        ScorePerSecond = savedScorre.ScorePerSecond;
        if (savedScorre.ScoreFactor >= _increaseTime)
            _increaseTime = savedScorre.ScoreFactor;

        _scorePerTime = new ScorrePerTime(10, Time.timeSinceLevelLoad, ScorePerSecond);
        ChangeScorre(0);
    }

    public void AddingScorre(float scorre)
    {
        ScorePerSecond = _scorePerTime.GetValue(scorre, Time.timeSinceLevelLoad);
        ChangeScorre(scorre);
        GameDataStorage.SaveScore(Score, ScorePerSecond, _increaseTime);
    }

    public void ReductionScorre(int scorre) => ChangeScorre(scorre * -1);

    public float GetScorre(float time)
    {
        float score = Mathf.CeilToInt(time / _increaseTime);

        if (score < 1) score = 1;

        return score;
    }

    public void Upgrade(float value)
    {
        _increaseTime *= value;
    }

    private void ChangeScorre(float score)
    {
        Score += score;
        _scoreDrawer.Draw(_scoreFormConvert.GetConvertedScore(Score));
    }

    public bool IsCanUpgrade()
    {
        throw new System.NotImplementedException();
    }
}
