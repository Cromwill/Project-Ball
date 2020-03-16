using UnityEngine;
using UnityEngine.UI;

public class LevelsFieldScorre : MonoBehaviour
{
    [SerializeField] private Text _scorreViewer;
    [SerializeField] private Text _scorrePerSecondViewer;
    [SerializeField] private Text _lableViewer;
    [SerializeField] private StartScreenScorreCounter _totalScorre;

    private float _scorrePerSecond;
    private string _levelName;

    public float Scorre { get; private set; }

    private void OnDisable()
    {
        PlayerPrefs.SetInt(_levelName + "_scorre", (int)Scorre);
    }

    private void Update()
    {
        Show();
    }

    public void Open(string levelName)
    {
        GameObjectSetActive(true, _scorreViewer.gameObject, _scorrePerSecondViewer.gameObject, _lableViewer.gameObject);
        _levelName = levelName;
        FillingScorreDatas();
        Show();
    }

    public void ReductionScorre(float scorre)
    {
        Scorre -= scorre;
    }

    public void AddScorre(int value)
    {
        Scorre += value;
    }

    private void FillingScorreDatas()
    {
        Scorre = PlayerPrefs.GetInt(_levelName + "_scorre");
        _scorrePerSecond = PlayerPrefs.GetFloat(_levelName + "_scorrePerSecond");
        Scorre += (_totalScorre.GetSleepTimeSecond() * _scorrePerSecond);
    }

    private void Show()
    {
        _scorrePerSecondViewer.text = _scorrePerSecond.ToString("0.##") + " point/sec";
        Scorre += _scorrePerSecond * Time.deltaTime;
        _scorreViewer.text = ((int)Scorre).ToString() + " points";
    }

    private void GameObjectSetActive(bool state, params GameObject[] objects)
    {
        foreach(var obj in objects)
        {
            obj.SetActive(state);
        }
    }
}
