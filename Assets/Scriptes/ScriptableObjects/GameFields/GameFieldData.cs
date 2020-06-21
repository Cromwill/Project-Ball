using UnityEngine;

[CreateAssetMenu(fileName = "new GameFieldData", menuName = "SO/Create GameFieldData")]
public class GameFieldData : ScriptableObject
{
    [SerializeField] private int _levelIndex;
    [SerializeField] private Sprite _closePanel;
    [SerializeField] private Sprite _openPanel;

    public int LevelIndex => _levelIndex;
    public Sprite ClosePanelSprite => _closePanel;
    public Sprite OpenPanelSprite => _openPanel;
}
