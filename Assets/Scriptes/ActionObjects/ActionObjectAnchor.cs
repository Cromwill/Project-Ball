using UnityEngine;

public class ActionObjectAnchor : MonoBehaviour, IActionObjectAnchor
{
    [SerializeField] private Color _idleColor;
    [SerializeField] private Color _activeColor;
    [SerializeField] private TypeForAnchor _type;
    private Transform _selfTransform;
    private SpriteRenderer _selfSpriteRenderer;
    private IChangeable _instaledObject;

    public bool IsFree { get; set; }
    public IChangeable InstalledFacility => _instaledObject;
    public TypeForAnchor GetAnchorType => _type;

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

    public void SetChangeableObject(IChangeable changeableObject)
    {
        if (changeableObject as IChangeable != null)
            _instaledObject = changeableObject as IChangeable;
        IsFree = false;
    }
}
