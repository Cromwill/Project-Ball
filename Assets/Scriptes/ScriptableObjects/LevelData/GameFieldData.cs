using UnityEngine;

[CreateAssetMenu(fileName = "new GameFieldData", menuName = "GameFieldDataSO")]
public class GameFieldData : ScriptableObject
{
    [SerializeField] private float _speed;
    [SerializeField] private Vector2 _scaleOnFocuse;
    [SerializeField] private Vector2 _deltaScale;
    [SerializeField] private float _scaleDistance;
    [SerializeField] private int _sceneIndex;

    public float Speed => _speed;
    public Vector2 ScaleOnFocuse => _scaleOnFocuse;
    public Vector2 DeltaScale => _deltaScale;
    public float ScaleDistance => _scaleDistance;
    public int SceneIndex => _sceneIndex;
}
