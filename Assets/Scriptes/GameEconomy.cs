using System;
using UnityEngine;

public class GameEconomy : MonoBehaviour
{
    [SerializeField] private float _objectIncrease;
    [SerializeField] private float _upgradeIncrease; 
    [SerializeField] private ScorreCounter _scorreCounter;

    private ObjectsCountOnScene _objectsOnScene;

    private void Start()
    {
        _objectsOnScene = GameDataStorage.GetObjectsOnCurrentScene();
    }

    private void OnDisable()
    {
        _objectsOnScene.Save();
    }
    public event Action PurchaseCompleted;

    public void OnPurchaseCompleted(IBuyable buyable)
    {
        _scorreCounter.ReductionScorre(GetPrice(buyable.Price, buyable.ObjectType));
        _objectsOnScene.AddCount(buyable.ObjectType);
        PurchaseCompleted?.Invoke();
    }

    public int GetPrice(float price, ActionObjectType objectType)
    {
        int count = _objectsOnScene.GetCount(objectType);
        if (count != 0)
        {
            if(objectType == ActionObjectType.Action || objectType == ActionObjectType.Phisics || objectType == ActionObjectType.Spawn)
                return (int)(price * count * _objectIncrease);
            else if(objectType == ActionObjectType.UpgradeSpawn || objectType == ActionObjectType.UpgradeScorre)
                return (int)(price * count * _upgradeIncrease);
            return (int)price;
        }
        else
            return (int)price;
    }

    public bool EnoughPoints(int scorre)
    {
        return scorre <= _scorreCounter.Scorre;
    }

    public void ScorreUpgrade(UpgradeObject upgradeObject)
    {
        _scorreCounter.Upgrade(upgradeObject.ChangingValue);
        OnPurchaseCompleted(upgradeObject);
    }

    public int GetCount(ActionObjectType objectType)
    {
        return _objectsOnScene.GetCount(objectType);
    }
}
