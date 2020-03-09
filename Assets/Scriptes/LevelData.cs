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

    private void OnDisable()
    {
        SaveDatas();
    }

    private void OnApplicationPause(bool pause)
    {
        SaveDatas();
    }

    private void Start()
    {
        GameDataStorage.CurrentLevel = _levelName;
        _actionObjectSpawner = GetComponent<ActionObjectSpawner>();
        bool isFirstLavel = !PlayerPrefs.HasKey(_levelName + "_isOpen");
        _objectPoolForBalls.GeneratePool(_levelData.BallCount, isFirstLavel, _levelName);
        _globalSpawn.GeneratePool(_levelData.SpawnObjectCount, isFirstLavel, _levelName);

        _actionObjectSpawner.Load(_levelName);
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
