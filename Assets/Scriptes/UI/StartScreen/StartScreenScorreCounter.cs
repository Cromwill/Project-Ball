using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenScorreCounter : MonoBehaviour
{
    [SerializeField] private ChoseGameField _choseGameField;
    [SerializeField] private ScorreDrawer _scorreDrawer;

    private string[] _levelNames;
    private float[] _scorrePerSecond;
    
    public float TotalScorre { get; private set; }

    private void OnApplicationPause(bool pause)
    {
        CustomPlayerPrefs.SetString("ExitGameTime", DateTime.Now.ToString());
    }

    private void OnDisable()
    {
        CustomPlayerPrefs.SetString("ExitGameTime", DateTime.Now.ToString());
    }

    private void Start()
    {
        _levelNames = _choseGameField.GetOpenLevelNames();
        _scorrePerSecond = new float[_levelNames.Length];

        for(int i = 0; i < _scorrePerSecond.Length; i++)
        {
            _scorrePerSecond[i] = PlayerPrefs.GetFloat(_levelNames[i] + "_scorrePerSecond");
        }

        TotalScorre = StartScorreCounting();
        _scorreDrawer.Draw((int)TotalScorre);
    }

    private void Update()
    {
        ShowScorre();
    }

    private float StartScorreCounting()
    {
        float scorre = 0;
        DateTime dateTime = DateTime.Parse(PlayerPrefs.GetString("ExitGameTime"));
        var sleepTime = DateTime.Now - dateTime;

        for(int i = 0; i < _levelNames.Length; i++)
        {
            scorre += PlayerPrefs.GetInt(_levelNames[i] + "_scorre");
            scorre += ((int)sleepTime.TotalSeconds * _scorrePerSecond[i]);
        }

        return scorre;
    }

    private void ShowScorre()
    {
        for (int i = 0; i < _levelNames.Length; i++)
        {
            TotalScorre += _scorrePerSecond[i] * Time.deltaTime;
        }
        _scorreDrawer.Draw((int)TotalScorre);
    }

}
