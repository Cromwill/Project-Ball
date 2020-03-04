using UnityEngine;

public class DisappearingPlatform : ActionObject
{
    [SerializeField] private float _cicleTime;
    [SerializeField] private float _colorDeltaAlpha;

    [SerializeField] private SpriteRenderer[] _sprites;
    private BoxCollider2D[] _colliders;
    private Cicle _selfCicle;
    private float _changeAlphaStep;

    private void Start()
    {
        _colliders = new BoxCollider2D[_sprites.Length];

        for(int i = 0; i < _colliders.Length; i++)
        {
            _colliders[i] = _sprites[i].GetComponent<BoxCollider2D>();
        }

        _selfCicle = Cicle.Dicrement;

        for(int i = 0; i < _sprites.Length; i++)
        {
            _sprites[i].color = i % 2 == 0 ? _sprites[i].color : new Color(_sprites[i].color.r, _sprites[i].color.g, _sprites[i].color.b, _sprites[i].color.a - _colorDeltaAlpha);
        }
        _changeAlphaStep = _colorDeltaAlpha / _cicleTime;
    }

    private void Update()
    {
        Work(_selfCicle);
    }

    private void Work(Cicle cicle)
    {
        for (int i = 0; i < _sprites.Length; i++)
        {
            _sprites[i].color = i % 2 == 0 ? GetColorWithChangedAlpha(_sprites[i].color, _changeAlphaStep * (int)cicle) : GetColorWithChangedAlpha(_sprites[i].color, _changeAlphaStep * -(int)cicle); ;
            if (_sprites[i].color.a > 1 - _colorDeltaAlpha / 2)
                _colliders[i].enabled = true;
            else
                _colliders[i].enabled = false;
        }

        if (_sprites[0].color.a >= 1 || _sprites[0].color.a <= 1 - _colorDeltaAlpha)
            _selfCicle = _selfCicle == Cicle.Dicrement ? Cicle.Increment : Cicle.Dicrement;
    }

    private Color GetColorWithChangedAlpha(Color color, float alphaStep)
    {
        return new Color(color.r, color.g, color.b, color.a + alphaStep * Time.deltaTime);
    }

    private enum Cicle
    {
        Increment = 1,
        Dicrement = -1
    }
}
