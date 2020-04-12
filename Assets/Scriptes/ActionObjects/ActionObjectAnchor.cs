using UnityEngine;

public class ActionObjectAnchor : MonoBehaviour, IActionObjectAnchor
{
    [SerializeField] private Color _idleColor;
    [SerializeField] private Color _activeColor;
    [SerializeField] private TypeForAnchor _type;
    private Transform _selfTransform;
    private SpriteRenderer _selfSpriteRenderer;

    public bool IsFree { get; set; }
    public IUpgradeable InstalledFacility { get; private set; }
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

    public void SetChangeableObject(IUpgradeable changeableObject)
    {
        if (changeableObject as IUpgradeable != null)
            InstalledFacility = changeableObject as IUpgradeable;
        IsFree = false;
    }
}
