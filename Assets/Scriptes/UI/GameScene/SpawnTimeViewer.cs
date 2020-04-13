using UnityEngine;
using UnityEngine.UI;


public class SpawnTimeViewer : MonoBehaviour
{
    public Vector3 SpawnObjectPosition { get; set; }

    private Text _text;
    private RectTransform _selfTransform;

    private void OnEnable()
    {
        _selfTransform = GetComponent<RectTransform>();
        _text = GetComponent<Text>();
    }

    public void SetPosition(Vector3 position)
    {
        if(_selfTransform == null)
            _selfTransform = GetComponent<RectTransform>();
        _selfTransform.position = Camera.main.WorldToScreenPoint(position);
    }

    public void ShowValue(string value)
    {
        _text.text = value;
    }
}

