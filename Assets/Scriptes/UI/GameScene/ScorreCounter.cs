using UnityEngine;

[RequireComponent(typeof(LevelData))]
public class ScorreCounter : MonoBehaviour, IUpgradeable
{
    [SerializeField] private ScorreDrawer _scorreDrawer;
    [SerializeField] private float _scorreFactor;
    [SerializeField] private ScorreFormConverter _scorreFormConvert;
    private ScorrePerTime _scorrePerTime;
    private string _levelName;

    public float Scorre { get; private set; }
    public float ScorrePerSecond { get; private set; }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(_levelName + "_scorre", Scorre);
        PlayerPrefs.SetFloat(_levelName + "_scorrePerSecond", ScorrePerSecond);
    }

    private void Start()
    {
        _levelName = GetComponent<LevelData>().LevelName;
        Scorre = PlayerPrefs.GetFloat(_levelName + "_scorre");
        ScorrePerSecond = PlayerPrefs.GetFloat(_levelName + "_scorrePerSecond");

        _scorrePerTime = new ScorrePerTime(10, Time.timeSinceLevelLoad, ScorrePerSecond);
        ChangeScorre(0);
    }

    public void AddingScorre(int scorre)
    {
        ScorrePerSecond = _scorrePerTime.GetValue(scorre, Time.timeSinceLevelLoad);
        ChangeScorre(scorre);
    }

    public void ReductionScorre(int scorre)
    {
        ChangeScorre(scorre * -1);
    }

    public int GetScorre(float time)
    {
        return Mathf.FloorToInt(time * _scorreFactor);
    }

    public void Upgrade(float value)
    {
        _scorreFactor *= value;
    }

    private void ChangeScorre(float scorre)
    {
        Scorre += scorre;
        _scorreDrawer.Draw(_scorreFormConvert.GetConvertedScorre(Scorre));
        _scorreDrawer.DrawSpeed(_scorreFormConvert.GetConvertedScorrePerSecond(ScorrePerSecond));
    }
}
