using UnityEngine;

public class ActionObjectAnchor : MonoBehaviour, IActionObjectAnchor
{
    [SerializeField] private Color _idleColor;
    [SerializeField] private Color _activeColor;
    [SerializeField] private ActionObjectScriptableObject.ActionObjectType _type;
    private Transform _selfTransform;
    private SpriteRenderer _selfSpriteRenderer;

    public bool IsFree { get; set; }
    public Transform Avatar { get; set; }
    ActionObjectScriptableObject.ActionObjectType IActionObjectAnchor.GetType => _type;

    private void Awake()
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

    public void ToggleColor()
    {
        _selfSpriteRenderer.color = _selfSpriteRenderer.color == _idleColor ? _activeColor : _idleColor;
    }
}
