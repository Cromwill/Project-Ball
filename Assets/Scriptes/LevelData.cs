using UnityEngine;

public class LevelData : MonoBehaviour
{
    [SerializeField] private PoolForObjects _globalSpawn;
    [SerializeField] private PoolForObjects _objectPoolForBalls;

    [SerializeField] private LevelDataScriptableObjects _levelData;

    private void Start()
    {
        _objectPoolForBalls.GeneratePool(_levelData.BallCount);
        _globalSpawn.Run(_levelData.SpawnObjectCount);
    }
}
