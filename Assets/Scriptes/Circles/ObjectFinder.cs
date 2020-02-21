using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ObjectFinder : MonoBehaviour
{
    public Action<Ball, Vector2> ObjectFinded;

    [SerializeField]
    private BallControl _ballControl;

    private SpriteRenderer _tuchCircle;
    private Transform _selfTransform;
    private bool isAction;

    private void Start()
    {
        _tuchCircle = GetComponent<SpriteRenderer>();
        _selfTransform = GetComponent<Transform>();
        isAction = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isAction)
        {
            ObjectFinded?.Invoke(collision.GetComponent<Ball>(), _selfTransform.position);
        }
    }

    public void Search(Vector2 position)
    {
        _tuchCircle.color = new Color(1, 1, 1, 30f / 255f);
        _selfTransform.position = position;
        isAction = true;
    }

    public void NotSearch()
    {
        _tuchCircle.color = new Color(1, 1, 1, 0);
        isAction = false;
    }
}
