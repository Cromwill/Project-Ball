using UnityEngine;
using System;

public class LoadLine : MonoBehaviour, IRunable
{
    private Transform _selfTransform;
    private float _time;
    private float _step;
    private Vector3 _startScale = new Vector3(0, 0.25f, 1.0f);
    private Vector3 _finishScale = new Vector3(1, 0.25f, 1.0f);

    void Start()
    {
        _selfTransform = GetComponent<Transform>();
        _selfTransform.localScale = _startScale;
    }

    void Update()
    {
        if (_time > 0)
            loadLineMove();
        else
            _selfTransform.localScale = _finishScale;
    }

    public void Run()
    {
        throw new System.NotImplementedException();
    }

    public void Run<Single>(Single value)
    {
        if (_selfTransform == null)
            _selfTransform = GetComponent<Transform>();

        _selfTransform.localScale = _startScale;
        _time = Convert.ToSingle(value);
        _step = 1 / _time;
    }

    public void Run<T, V>(T valueT, V valueV)
    {
        throw new System.NotImplementedException();
    }

    private void loadLineMove()
    {
        _selfTransform.localScale += new Vector3(_step * Time.deltaTime, 0, 0);
        _time -= Time.deltaTime;
    }
}
