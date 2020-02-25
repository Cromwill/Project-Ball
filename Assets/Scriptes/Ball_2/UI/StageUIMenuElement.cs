using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Mask))]
public class StageUIMenuElement : UIMenuElement
{
    [SerializeField] private float _workTime;

    private Vector2 _cacheOffset;
    private float _workSpeed;
    private bool _isWork = false;
    private State _state = State.Open;
    private Mask _selfMask;

    public override bool IsWork { get => _isWork; protected set => value =_isWork; }
    public override bool IsOpen { get => _state == State.Open;}

    private void Start()
    {
        _selfTransform = GetComponent<RectTransform>();
        _selfMask = GetComponent<Mask>();
        _cacheOffset = IsOffsetPositive() ? _selfTransform.offsetMax : _selfTransform.offsetMin;
        UseMenuElement();

    }

    private void Update()
    {
        ChangeTracking();
    }

    public override void UseMenuElement()
    {
        if (IsOpen)
        {
            for (int i = 0; i < _elements.Length; i++)
            {
                if (_elements[i].IsMenuOpen)
                {
                    _elements[i].Closing();
                }
            }
        }
        _state = IsOpen ? State.Close : State.Open;
        _workSpeed = _cacheOffset.x != 0 ? _cacheOffset.x / _workTime : _cacheOffset.y / _workTime;
        _isWork = true;
    }

    private void ChangeTracking()
    {
        if (_isWork)
        {
            ChangeMenuElementState((int)_state);
        }
        if (IsFinishedWork())
        {
            _isWork = false;
            if (IsOffsetPositive())
                _selfTransform.offsetMax = IsOpen ? _cacheOffset : _selfTransform.offsetMin;
            else
                _selfTransform.offsetMin = IsOpen ? _cacheOffset : _selfTransform.offsetMax;
        }

        if (_selfMask != null)
            _selfMask.enabled = !_isWork && IsOpen ? false : true;
    }

    private bool IsFinishedWork()
    {
        if (IsOffsetPositive())
        {
            if (!IsNumberInTheRange(_selfTransform.offsetMax.x, 0, _cacheOffset.x) ||
                !IsNumberInTheRange(_selfTransform.offsetMax.y, 0, _cacheOffset.y))
                return true;
        }
        else if (!IsOffsetPositive())
        {
            if (!IsNumberInTheRange(_selfTransform.offsetMin.x, _cacheOffset.x, 0) ||
                !IsNumberInTheRange(_selfTransform.offsetMin.y, _cacheOffset.y, 0))
                return true;
        }
        return false;
    }

    private bool IsNumberInTheRange(float value, float min, float max)
    {
        return min <= value && value <= max;
    }

    private void ChangeMenuElementState(int direction)
    {
        float offset = _workSpeed * direction * Time.deltaTime;

        if (_cacheOffset.x != 0)
            SetNextOffsetValue(offset, _cacheOffset.y);
        if (_cacheOffset.y != 0)
            SetNextOffsetValue(_cacheOffset.x, offset);
    }

    private void SetNextOffsetValue(float x, float y)
    {
        if (IsOffsetPositive())
            _selfTransform.offsetMax += new Vector2(x, y);
        else
            _selfTransform.offsetMin += new Vector2(x, y);
    }

    private bool IsOffsetPositive()
    {
        if (_cacheOffset == Vector2.zero)
            return _selfTransform.offsetMax != Vector2.zero;
        else
            return _cacheOffset.x == 0 ? _cacheOffset.y >= 0 : _cacheOffset.x >= 0;
    }
}
