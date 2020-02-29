using UnityEngine;

public class GameEconomy : MonoBehaviour
{
    [SerializeField] private float _priceIncreaseRationObjects;
    [SerializeField] private float _priceIncreaseRationBoosts;
    [SerializeField] private ScorreCounter _scorreCounter;

    private IBuyable _currentGood;
    private int _countObjectsOnStage = 1;
    private int _currentPrice;

    public void StartPurchase(IBuyable buyable)
    {
        _currentGood = buyable;
        _currentPrice = (int)(_currentGood.Price * _countObjectsOnStage * _priceIncreaseRationObjects);
    }

    public void ShowImprovements()
    {

    }

    public void FinishPurchase(bool isPurchaseDone)
    {
        if(isPurchaseDone)
        {
            _scorreCounter.ReductionScorre((int)(_currentGood.Price * _countObjectsOnStage * _priceIncreaseRationObjects)); 
        }
    }

    public bool EnoughPoints()
    {
        return _currentPrice < _scorreCounter.Scorre;
    }
}
