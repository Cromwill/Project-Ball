using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelData : MonoBehaviour
{
    [SerializeField] private PoolForObjects _globalSpawn;
    [SerializeField] private PoolForObjects _objectPoolForBalls;
    [SerializeField] private ExitPanel _exitPanel;
    [SerializeField] private string _levelName;
    [SerializeField] private LevelDataScriptableObjects _levelData;

    private ActionObjectSpawner _actionObjectSpawner;

    public string LevelName => _levelName;

    private void Awake()
    {
        _actionObjectSpawner = GetComponent<ActionObjectSpawner>();
        GameDataStorage.CurrentLevel = _levelName;
    }

    private void OnDisable()
    {
        PlayerPrefs.SetString("ExitGameTime", DateTime.Now.ToString());
        SaveDatas();
    }

    private void Start()
    {
        bool isFirstLavel = !PlayerPrefs.HasKey(_levelName + "_isFirstRun");
        _objectPoolForBalls.GeneratePool(_levelData.BallCount, isFirstLavel, _levelName);
        _globalSpawn.GeneratePool(_levelData.SpawnObjectCount, isFirstLavel, _levelName);
        _actionObjectSpawner.Load(_levelName);
        if (isFirstLavel)
            PlayerPrefs.SetString(_levelName + "_isFirstRun", true.ToString());
    }

    public void ExitLevel()
    {
        SaveDatas();
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    private void SaveDatas()
    {
        _objectPoolForBalls.Save(_levelName);
        _actionObjectSpawner.Save(_levelName);
    }
}
