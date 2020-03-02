using System;
using UnityEngine;

public class GameEconomy : MonoBehaviour
{
    [SerializeField] private float _increase;
    [SerializeField] private ScorreCounter _scorreCounter;

    private int _countObjectsOnStage = 1;
    public event Action PurchaseCompleted;

    public void OnPurchaseCompleted(IBuyable buyable)
    {
        _scorreCounter.ReductionScorre(GetPrice(buyable.Price));
        _countObjectsOnStage++;
        PurchaseCompleted?.Invoke();
    }

    public int GetPrice(float price)
    {
        return (int)(price * _countObjectsOnStage * _increase);
    }

    public bool EnoughPoints(int scorre)
    {
        return scorre <= _scorreCounter.Scorre;
    }
}
