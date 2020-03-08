using UnityEngine;

[RequireComponent(typeof(LevelData))]
public class ScorreCounter : MonoBehaviour, IUpgradeable
{
    [SerializeField] ScorreDrawer _scorreDrawer;
    [SerializeField] float _scorreFactor;
    private ScorrePerTime _scorrePerTime;
    private string _levelName;

    public int Scorre { get; private set; }
    public float ScorrePerSecond { get; private set; }

    private void OnEnable()
    {
        _scorrePerTime = new ScorrePerTime(30, Time.time);
    }

    private void OnDisable()
    {
        CustomPlayerPrefs.SetInt(_levelName + "_scorre", Scorre);
        CustomPlayerPrefs.SetFloat(_levelName + "_scorrePerSecond", ScorrePerSecond);
    }

    private void Start()
    {
        _levelName = GetComponent<LevelData>().LevelName;
        Scorre = PlayerPrefs.GetInt(_levelName + "_scorre");
        ScorrePerSecond = PlayerPrefs.GetFloat(_levelName + "_scorrePerSecond");
        ChangeScorre(0);
        _scorreDrawer.DrawSpeed(ScorrePerSecond);
    }

    public void AddingScorre(int scorre)
    {
        ScorrePerSecond = _scorrePerTime.GetValue(scorre, Time.time);
        _scorreDrawer.DrawSpeed(ScorrePerSecond);
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

    private void ChangeScorre(int scorre)
    {
        Scorre += scorre;
        _scorreDrawer.Draw(Scorre);
    }


}
