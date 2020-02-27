using UnityEngine;

public class ActionObjectAnchor : MonoBehaviour, IActionObjectAnchor
{
    [SerializeField] private Color _idleColor;
    [SerializeField] private Color _activeColor;

    private Transform _selfTransform;
    private SpriteRenderer _selfSpriteRenderer;

    public bool IsFree { get; set; }

    private void Start()
    {
        _selfTransform = GetComponent<Transform>();
        _selfSpriteRenderer = GetComponent<SpriteRenderer>();
        _selfSpriteRenderer.color = _idleColor;
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
        _selfSpriteRenderer.color = _selfSpriteRenderer.color == _idleColor ? _activeColor : _idleColor;
    }
}
