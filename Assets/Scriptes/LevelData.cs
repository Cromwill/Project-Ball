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
    private SpawnObjectSpawner _spawnObjectSpawner;

    public string LevelName => _levelName;

    private void Awake()
    {
        _actionObjectSpawner = GetComponent<ActionObjectSpawner>();
        _spawnObjectSpawner = GetComponent<SpawnObjectSpawner>();
        GameDataStorage.CurentLevel = _levelName;
    }

    #region ExitGame
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Debug.Log("Pause - " + pause);
            QuitLevel();
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
            QuitLevel();
    }

    private void OnDisable()
    {
        QuitLevel();
    }
    #endregion
    private void Start()
    {
        bool isFirstLavel = !PlayerPrefs.HasKey(_levelName + "_isFirstRun");
        _objectPoolForBalls.GeneratePool(_levelData.BallCount, isFirstLavel, _levelName);
        _globalSpawn.GeneratePool(_levelData.SpawnObjectCount, isFirstLavel, _levelName);
        _actionObjectSpawner.Load(_levelName);
        _spawnObjectSpawner.Load(_levelName);
        if (isFirstLavel)
            PlayerPrefs.SetString(_levelName + "_isFirstRun", true.ToString());
    }

    public void ExitLevel()
    {
        QuitLevel();
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    private void QuitLevel()
    {
        PlayerPrefs.SetString("ExitGameTime", DateTime.Now.ToString());
        _spawnObjectSpawner.Save(_levelName);
        _actionObjectSpawner.Save(_levelName);
    }
}
