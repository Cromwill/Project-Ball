using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ColorButton : MonoBehaviour
{
    [SerializeField] private Color _color;
    [SerializeField] private string _colorName;

    private Text _nameViewer;
    private Image _colorViewer;
    private Button _selfButton;
    private UnityAction<Color> _buttonClick;

    private void Start()
    {
        _selfButton = GetComponent<Button>();
        _nameViewer = GetComponentInChildren<Text>();
        _colorViewer = GetComponent<Image>();

        _nameViewer.text = _colorName;
        _colorViewer.color = _color;

        _selfButton.onClick.AddListener(OnButtonClick);
        
    }

    public void ButtonClickAddListener(UnityAction<Color> action)
    {
        _buttonClick += action;
    }

    private void OnButtonClick()
    {
        _buttonClick?.Invoke(_color);
    }
}
