using UnityEngine;
using UnityEngine.UI;

public class LevelsFieldScorre : MonoBehaviour
{
    [SerializeField] private Text _scorreViewer;
    [SerializeField] private Text _scorrePerSecondViewer;
    [SerializeField] private Text _lableViewer;
    [SerializeField] private StartScreenScorreCounter _totalScorre;
    [SerializeField] private ScorreFormConverter _scorreFormConverter;
    [SerializeField] private GameObject _gamePanel;
    private string _levelName;

    public float Scorre { get; private set; }
    public float ScorrePerSecond { get; private set; }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(_levelName + "_scorre", Scorre);
    }

    private void Update()
    {
        Show();
    }

    public void Open(string levelName)
    {
        _gamePanel.SetActive(true);
        _levelName = levelName;
        FillingScorreDatas();
        Show();
    }

    public void ReductionScorre(float scorre) => Scorre -= scorre;

    public void AddScorre(int value) => Scorre += value;

    private void FillingScorreDatas()
    {
        Scorre = PlayerPrefs.GetFloat(_levelName + "_scorre");
        ScorrePerSecond = PlayerPrefs.GetFloat(_levelName + "_scorrePerSecond");
        Scorre += _totalScorre.GetSleepTimeSecond() * ScorrePerSecond;
    }

    private void Show()
    {
        _scorrePerSecondViewer.text = _scorreFormConverter.GetConvertedScorrePerSecond(ScorrePerSecond);
        Scorre += ScorrePerSecond * Time.deltaTime;
        _scorreViewer.text = _scorreFormConverter.GetConvertedScorre(Scorre);
    }
}
