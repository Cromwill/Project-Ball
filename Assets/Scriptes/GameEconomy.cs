using UnityEngine;
using System.Collections;

public class GameEconomy : MonoBehaviour
{
    [SerializeField] private float _priceIncreaseRationObjects;
    [SerializeField] private float _priceIncreaseRationBoosts;
    [SerializeField] private ScorreCounter _scorreCounter;
    [SerializeField] private PurchaseDataDrawer _purchaseDataDrawer;

    private IBuyable _currentGood;
    private int _countObjectsOnStage = 1;
    private int _currentPrice;

    public void StartPurchase(IBuyable buyable)
    {
        _currentGood = buyable;
        _currentPrice = (int)(_currentGood.Price * _countObjectsOnStage * _priceIncreaseRationObjects);
        _purchaseDataDrawer.gameObject.SetActive(true);
        _purchaseDataDrawer.ShowData(_currentGood.Name, _currentPrice.ToString());
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
        _purchaseDataDrawer.gameObject.SetActive(false);
    }

    public bool EnoughPoints()
    {
        return _currentPrice < _scorreCounter.Scorre;
    }
}
