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
        PlayerPrefs.SetString("ExitGameTime", DateTime.Now.ToString());
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

    public void ReductionScorre(float scorre)
    {
        GameField[] fields = _choseGameField.GameFields.Where(a => a.IsOpenLevel).ToArray();
        float[] percentageOfAmounts = new float[fields.Length];
        int[] subtractions = new int[fields.Length];

        for(int i = 0; i < fields.Length; i++)
        {
            percentageOfAmounts[i] = fields[i].Price / TotalScorre;
            subtractions[i] = (int)(scorre * percentageOfAmounts[i]);
            subtractions[i] = subtractions[i] > fields[i].ScorrePanel.Scorre ? (int)fields[i].ScorrePanel.Scorre : subtractions[i];
        }

        if(subtractions.Sum() < scorre)
        {
            int difference = (int)scorre - subtractions.Sum();

            for (int i = 0; i < fields.Length && difference != 0; i++)
            {
                if (fields[i].ScorrePanel.Scorre - subtractions[i] > difference)
                    subtractions[i] += difference;
                else
                {
                    subtractions[i] += (int)(fields[i].ScorrePanel.Scorre - subtractions[i]);
                }
            }
        }

        for (int i = 0; i < fields.Length; i++)
        {
            fields[i].ScorrePanel.ReductionScorre(subtractions[i]);
        }

        TotalScorre -= scorre;
    }

    public float GetSleepTimeSecond()
    {
        if (PlayerPrefs.HasKey("ExitGameTime"))
        {
            DateTime dateTime = DateTime.Parse(PlayerPrefs.GetString("ExitGameTime"));
            var sleepTime = DateTime.Now - dateTime;
            return (float)sleepTime.TotalSeconds;
        }
        else
            return 0;
    }

    private void ShowScorre()
    {
        TotalScorre = 0;

        foreach (var field in _levelsScorre)
            TotalScorre += field.Scorre;

        _scorreDrawer.Draw((int)TotalScorre);
    }
}
