using UnityEngine;

[RequireComponent(typeof(LevelData))]
public class ScoreCounter : MonoBehaviour, IUpgradeable
{
    [SerializeField] private ScoreDrawer _scoreDrawer;
    [SerializeField] private float _scoreFactor;
    [SerializeField] private ScoreFormConverter _scoreFormConvert;

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
        if (savedScorre.ScoreFactor >= _scoreFactor)
            _scoreFactor = savedScorre.ScoreFactor;

        _scorePerTime = new ScorrePerTime(10, Time.timeSinceLevelLoad, ScorePerSecond);
        ChangeScorre(0);
    }

    public void AddingScorre(float scorre)
    {
        ScorePerSecond = _scorePerTime.GetValue(scorre, Time.timeSinceLevelLoad);
        ChangeScorre(scorre);
        GameDataStorage.SaveScore(Score, ScorePerSecond, _scoreFactor);
    }

    public void ReductionScorre(int scorre) => ChangeScorre(scorre * -1);

    public float GetScorre(float time)
    {
        return time * _scoreFactor;
    }

    public void Upgrade(float value)
    {
        _scoreFactor *= value;
    }

    private void ChangeScorre(float score)
    {
        Score += score;
        _scoreDrawer.Draw(_scoreFormConvert.GetConvertedScore(Score));
        _scoreDrawer.DrawSpeed(_scoreFormConvert.GetConvertedScorrePerSecond(ScorePerSecond));
    }

    public bool IsCanUpgrade()
    {
        throw new System.NotImplementedException();
    }
}
