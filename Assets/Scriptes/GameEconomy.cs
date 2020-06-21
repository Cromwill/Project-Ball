using System;
using UnityEngine;

public class GameEconomy : MonoBehaviour
{
    [SerializeField] private float _objectIncrease;
    [SerializeField] private float _upgradeIncrease; 
    [SerializeField] private ScoreCounter _scorreCounter;

    private ObjectsCountOnScene _objectsOnScene;

    private ObjectsCountOnScene ObjectOnScene
    {
        get
        {
            if(_objectsOnScene == null)
                _objectsOnScene = GameDataStorage.GetObjectsOnCurrentScene();
            return _objectsOnScene;
        }
    }

    public event Action PurchaseCompleted;

    public void OnPurchaseCompleted(IBuyable buyable)
    {
        _scorreCounter.ReductionScorre(GetPrice(buyable.Price, buyable.ObjectType));
        ObjectOnScene.AddCount(buyable.ObjectType);
        GameDataStorage.SaveObjectsOnScene(ObjectOnScene);
        PurchaseCompleted?.Invoke();
    }

    public int GetPrice(float price, ActionObjectType objectType)
    {
        int count = ObjectOnScene.GetCount(objectType);
        if (count != 0)
        {
            if(objectType == ActionObjectType.Action || objectType == ActionObjectType.Phisics || objectType == ActionObjectType.Spawn)
                return (int)(price * GetСoefficient(count) * _objectIncrease);
            else if(objectType == ActionObjectType.UpgradeSpawn || objectType == ActionObjectType.UpgradeScorre)
                return (int)(price * GetСoefficient(count) * _upgradeIncrease);
            return (int)price;
        }
        else
            return (int)price;
    }

    public bool EnoughPoints(int scorre)
    {
        return scorre <= _scorreCounter.Score;
    }

    public void ScorreUpgrade(UpgradeObject upgradeObject)
    {
        _scorreCounter.Upgrade(upgradeObject.ChangingValue);
        OnPurchaseCompleted(upgradeObject);
    }

    public int GetCount(ActionObjectType objectType)
    {
        return ObjectOnScene.GetCount(objectType);
    }

    private int GetСoefficient(int count)
    {
        return (int)Mathf.Pow(2, count);
    }
}
