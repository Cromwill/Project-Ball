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

    private void Start()
    {
        _actionObjectSpawner = GetComponent<ActionObjectSpawner>();
        _actionObjectSpawner.Load(_levelName);
        bool isFirstLavel = !PlayerPrefs.HasKey(_levelName);
        Debug.Log(isFirstLavel);
        _objectPoolForBalls.GeneratePool(_levelData.BallCount, isFirstLavel, _levelName);
        _globalSpawn.GeneratePool(_levelData.SpawnObjectCount, isFirstLavel, _levelName);
    }

    public void ExitLevel()
    {
        _objectPoolForBalls.Save(_levelName);
        _globalSpawn.Save(_levelName);
        _actionObjectSpawner.Save(_levelName);
        CustomPlayerPrefs.SetString(_levelName, _levelName);
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
