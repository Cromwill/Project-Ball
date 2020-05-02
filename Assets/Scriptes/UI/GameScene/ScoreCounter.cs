using UnityEngine;

[RequireComponent(typeof(LevelData))]
public class ScoreCounter : MonoBehaviour, IUpgradeable
{
    [SerializeField] private ScoreDrawer _scorreDrawer;
    [SerializeField] private float _scorreFactor;
    [SerializeField] private ScoreFormConverter _scorreFormConvert;
    private ScorrePerTime _scorrePerTime;
    private string _levelName;

    public float Scorre { get; private set; }
    public float ScorrePerSecond { get; private set; }

    private void OnDisable()
    {
        GameDataStorage.SaveScorreCounter(Scorre, ScorrePerSecond, _scorreFactor);
    }

    private void Start()
    {
        _levelName = GetComponent<LevelData>().LevelName;
        var savedScorre = GameDataStorage.GetScorreCounter();

        Scorre = savedScorre.Scorre;
        ScorrePerSecond = savedScorre.ScorrePerSecond;
        if (savedScorre.ScorreFactor >= _scorreFactor)
            _scorreFactor = savedScorre.ScorreFactor;

        _scorrePerTime = new ScorrePerTime(10, Time.timeSinceLevelLoad, ScorrePerSecond);
        ChangeScorre(0);
    }

    public void AddingScorre(float scorre)
    {
        ScorrePerSecond = _scorrePerTime.GetValue(scorre, Time.timeSinceLevelLoad);
        ChangeScorre(scorre);
    }

    public void ReductionScorre(int scorre) => ChangeScorre(scorre * -1);

    public float GetScorre(float time)
    {
        return time * _scorreFactor;
    }

    public void Upgrade(float value)
    {
        _scorreFactor *= value;
    }

    private void ChangeScorre(float score)
    {
        Scorre += score;
        _scorreDrawer.Draw(_scorreFormConvert.GetConvertedScore(Scorre));
        _scorreDrawer.DrawSpeed(_scorreFormConvert.GetConvertedScorrePerSecond(ScorrePerSecond));
    }

    public bool IsCanUpgrade()
    {
        throw new System.NotImplementedException();
    }
}
