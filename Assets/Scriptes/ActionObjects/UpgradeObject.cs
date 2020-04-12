using UnityEngine;

public class UpgradeObject : ActionObject
{
    [SerializeField] private float _changingValue;

    public float ChangingValue => _changingValue;
}
