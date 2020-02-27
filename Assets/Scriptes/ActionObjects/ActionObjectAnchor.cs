using UnityEngine;

public class ActionObjectAnchor : MonoBehaviour, IActionObjectAnchor
{
    private Transform _selfTransform;
    private SpriteRenderer _selfSpriteRenderer;
    private const float _idleColorAlpha = 0;
    private const float _activeColorAlpha = 0.1f;
    private ColorAlpha _colorAlpha = ColorAlpha.Idle;

    public bool IsFree { get; set; }

    private void Start()
    {
        _selfTransform = GetComponent<Transform>();
        _selfSpriteRenderer = GetComponent<SpriteRenderer>();
        _selfSpriteRenderer.color = new Color(1, 1, 1, _idleColorAlpha);
        IsFree = true;
    }

    public Vector2 GetPosition()
    {
        if (_selfTransform == null)
            _selfTransform = GetComponent<Transform>();

        return _selfTransform.position;
    }

    public void Toggle()
    {
        if (_selfSpriteRenderer.color.a == _idleColorAlpha)
            _selfSpriteRenderer.color = new Color(1, 1, 1, _activeColorAlpha);
        else
            _selfSpriteRenderer.color = new Color(1, 1, 1, _idleColorAlpha);
    }

    private enum ColorAlpha
    {
        Idle,
        Active
    }

}
