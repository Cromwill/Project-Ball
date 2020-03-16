using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

[RequireComponent(typeof(LevelData))]
public class LevelColorSetter : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _background;
    [SerializeField] private Tilemap _midleground;
    [SerializeField] private Image[] _imagesUI;

    private LevelData _levelData;

    private void Start()
    {
        _levelData = GetComponent<LevelData>();
        _background.color = GameDataStorage.GetColorForLevel(_levelData.LevelName, ColorPlace.Background);

        Color color = GameDataStorage.GetColorForLevel(_levelData.LevelName, ColorPlace.MidleGround);
        _midleground.color = color;

        foreach(var colorUI in _imagesUI)
        {
            colorUI.color = color;
        }
    }
}
