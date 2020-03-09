using System;
using System.Linq;
using UnityEngine;

public class StartScreenScorreCounter : MonoBehaviour
{
    [SerializeField] private ChoseGameField _choseGameField;
    [SerializeField] private ScorreDrawer _scorreDrawer;

    private LevelsFieldScorre[] _levelsScorre;
    
    public float TotalScorre { get; private set; }

    private void OnApplicationPause(bool pause)
    {
        CustomPlayerPrefs.SetString("ExitGameTime", DateTime.Now.ToString());
    }

    private void Start()
    {
        GameField[] fields = _choseGameField.GameFields.Where(a => a.IsOpenLevel).ToArray();
        _levelsScorre = new LevelsFieldScorre[fields.Length];

        for(int i = 0; i < _levelsScorre.Length; i++)
        {
            _levelsScorre[i] = fields[i].ScorrePanel;
        }
    }

    private void Update()
    {
        ShowScorre();
    }

    public void ReductionScorre(int scorre)
    {
        TotalScorre -= scorre;
    }

    public float GetSleepTimeSecond()
    {
        DateTime dateTime = DateTime.Parse(PlayerPrefs.GetString("ExitGameTime"));
        var sleepTime = DateTime.Now - dateTime;
        return (float)sleepTime.TotalSeconds;
    }

    private void ShowScorre()
    {
        TotalScorre = 0;

        foreach (var field in _levelsScorre)
            TotalScorre += field.Scorre;

        _scorreDrawer.Draw((int)TotalScorre);
    }
}
