using UnityEngine;

[RequireComponent(typeof(LevelData))]
public class ScorreCounter : MonoBehaviour, IUpgradeable
{
    [SerializeField] ScorreDrawer _scorreDrawer;
    [SerializeField] float _scorreFactor;

    private int _scorre;
    private ScorrePerTime _scorrePerTime;
    private string _levelName;

    public int Scorre => _scorre;

    private void OnEnable()
    {
        _scorrePerTime = new ScorrePerTime(30, Time.time);
    }

    private void OnDisable()
    {
        CustomPlayerPrefs.SetInt(_levelName + "_scorre", _scorre);
    }

    private void Start()
    {
        _levelName = GetComponent<LevelData>().LevelName;
        _scorre = PlayerPrefs.GetInt(_levelName + "_scorre");
        ChangeScorre(0);
    }

    public void AddingScorre(int scorre)
    {
        _scorreDrawer.DrawSpeed(_scorrePerTime.GetValue(scorre, Time.time));
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
        _scorre += scorre;
        _scorreDrawer.Draw(_scorre);
    }


}
