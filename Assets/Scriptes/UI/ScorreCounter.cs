using UnityEngine;

public class ScorreCounter : MonoBehaviour, IUpgradeable
{
    [SerializeField] ScorreDrawer _scorreDrawer;
    [SerializeField] float _scorreFactor;
    [SerializeField] float _speedRange;

    private int _scorre;
    private ScorrePerTime _scorrePerTime;

    public int Scorre => _scorre;

    private void OnEnable()
    {
        _scorrePerTime = new ScorrePerTime(30, Time.time);
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
