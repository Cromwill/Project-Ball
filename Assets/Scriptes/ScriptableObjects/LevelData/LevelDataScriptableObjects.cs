using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="new Level Data", menuName = "LevelDataSO")]
public class LevelDataScriptableObjects : ScriptableObject
{
    [SerializeField] private int _spawnObjectCount;
    [SerializeField] private int _ballCount;

    private Color _midleGround;
    private Color _backGround;
    private Color _ball;

    public int SpawnObjectCount { get => _spawnObjectCount; set => _spawnObjectCount = value; }
    public int BallCount { get => _ballCount; set => _ballCount = value; }
}
