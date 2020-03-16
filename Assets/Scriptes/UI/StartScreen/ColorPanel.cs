using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class ColorPanel : MonoBehaviour
{
    private LevelDrawer _levelDrawer;
    private Animator _animator;
    private int _colorsCounter;
    private ColorButton[] _colorButtons;
    private ObjectForColor _objectForColor;
    private string _levelName;
    private bool _isOpen;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _colorButtons = GetComponentsInChildren<ColorButton>();

        foreach (var button in _colorButtons)
        {
            button.ButtonClickAddListener(ChoseColor);
        }

    }

    public void OpenPanel(string levelName, LevelDrawer levelDrawer)
    {
        _levelName = levelName;
        _levelDrawer = levelDrawer;
        TogglePanel();
    }

    public void TogglePanel()
    {
        string animations = _isOpen ? "CloseColorPanel" : "OpenColorPanel";
        _animator.Play(animations);
        _isOpen = !_isOpen;
    }

    public void ChangeObjectForColor(int objectNumber)
    {
        if (objectNumber >= 0 && objectNumber <= 2)
        {
            if ((int)_objectForColor != objectNumber)
            {
                _animator.Play("ChangeColorBattons");
                _objectForColor = (ObjectForColor)objectNumber;
            }
        }
    }

    private void ChoseColor(Color color)
    {
        if (_objectForColor == ObjectForColor.Background)
        {
            _levelDrawer.SetBackgroundColor(color);
            GameDataStorage.SaveColorForLevel(_levelName, color, ColorPlace.Background);
        }
        else
        {
            _levelDrawer.SetMidlegroundColor(color);
            GameDataStorage.SaveColorForLevel(_levelName, color, ColorPlace.MidleGround);
        }
    }
}

public enum ObjectForColor
{
    Midleground = 0,
    Background = 1
}

public class ColorWithName
{
    public Color Color { get; private set; }
    public string Name { get; private set; }

    public ColorWithName(Color color, string name)
    {
        Color = color;
        Name = name;
    }
}
