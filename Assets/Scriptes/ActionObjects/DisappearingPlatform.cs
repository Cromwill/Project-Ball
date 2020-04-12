using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DisappearingPlatform : ActionObject
{
    [SerializeField] private float _cicleTime;
    [SerializeField] private Color _colorOn;
    [SerializeField] private Color _colorOff;

    private BoxCollider2D _collider;
    private SpriteRenderer _spriteRenderer;
    private Cicle _selfCicle;
    private Color _changingStep;

    private void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = _colorOn;
        _selfCicle = Cicle.Dicrement;
        _changingStep = (_colorOn - _colorOff) / _cicleTime;
    }

    private void Update()
    {
        Work(_selfCicle);
    }

    private void Work(Cicle cicle)
    {
        _spriteRenderer.color = GetChangedColor(_spriteRenderer.color, _changingStep * (int)cicle);
        _collider.enabled = IsHalfColor();
    }

    private Color GetChangedColor(Color color, Color step)
    {
        Color changedColor = color + step * Time.deltaTime;
        if (changedColor.r < _colorOff.r)
        {
            _selfCicle = Cicle.Increment;
            return _colorOff;
        }
        else if (changedColor.r > _colorOn.r)
        {
            _selfCicle = Cicle.Dicrement;
            return _colorOn;
        }
        else
            return changedColor;
    }

    private bool IsHalfColor()
    {
        Color color = _spriteRenderer.color;
        if (IsHalfValue(color.r, _colorOn.r, _colorOff.r) && IsHalfValue(color.g, _colorOn.g, _colorOff.g) && IsHalfValue(color.b, _colorOn.b, _colorOff.b))
            return true;
        else
            return false;
    }

    private bool IsHalfValue(float value, float onValue, float offValue)
    {
        return value > offValue + (onValue - offValue) / 2;
    }

    private enum Cicle
    {
        Increment = 1,
        Dicrement = -1
    }
}
