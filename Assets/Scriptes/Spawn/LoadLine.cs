using UnityEngine;
using System;

public class LoadLine : MonoBehaviour, IRunable<float>
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

    public void Run(float value)
    {
        if (_selfTransform == null)
            _selfTransform = GetComponent<Transform>();

        _selfTransform.localScale = _startScale;
        _time = value;
        _step = 1 / _time;
    }

    private void loadLineMove()
    {
        _selfTransform.localScale += new Vector3(_step * Time.deltaTime, 0, 0);
        _time -= Time.deltaTime;
    }


}
