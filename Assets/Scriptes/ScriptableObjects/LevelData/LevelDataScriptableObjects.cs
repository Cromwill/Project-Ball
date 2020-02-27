using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="new Level Data", menuName = "LevelDataSO")]
public class LevelDataScriptableObjects : ScriptableObject
{
    [SerializeField] private int _spawnObjectCount;
    [SerializeField] private int _ballCount;

    public int SpawnObjectCount { get => _spawnObjectCount; set => _spawnObjectCount = value; }
    public int BallCount { get => _ballCount; set => _ballCount = value; }
}
