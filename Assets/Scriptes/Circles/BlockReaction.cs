using System.Collections.Generic;
using UnityEngine;

public class BlockReaction : MonoBehaviour
{
    [SerializeField]
    private List<Color> _colors;

    private SpriteRenderer _selfSpriteRenderer;
    private PolygonCollider2D _selfColider;
    private int _colorCounter;

    private void Start()
    {
        _selfSpriteRenderer = GetComponent<SpriteRenderer>();
        _selfColider = GetComponent<PolygonCollider2D>();
        _colorCounter = _colors.Count - 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _selfSpriteRenderer.color = _colors[_colorCounter];
        _colorCounter--;
        if(_colorCounter < 0)
        {
            _colorCounter = _colors.Count - 1;
        }
    }
}
