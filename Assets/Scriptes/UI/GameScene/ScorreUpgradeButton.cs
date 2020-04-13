﻿using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ScorreUpgradeButton : ProductPanel
{
    [SerializeField] private Text _scorreUpgradeViewer;

    private void OnEnable()
    {
        _productButton = GetComponentInChildren<Button>();
    }

    private void Start()
    {
        _nameViewer.text = _product.ActionObject.LevelName;
        _productButton.onClick.AddListener(UpgradeScorre);
        _buttonImage = _productButton.GetComponent<Image>();
    }

    private void Update()
    {
        OpportunityBuy();
    }

    public override void AddListenerToButton(Action<IGeneratedBy> listener, UnityAction closePanel, UnityAction openConfirmPanel, GameEconomy economy,
        ScorreFormConverter scorreForm)
    {
        _economy = economy;
        _economy.PurchaseCompleted += ChangePrice;
        _scorreForm = scorreForm;
        ChangePrice();
    }

    public override void ChangePrice()
    {
        var price = _economy.GetPrice(_product.ActionObject.Price, _product.ActionObject.ObjectType);
        _priceViewer.text = _scorreForm.GetConvertedScorre(price);
        _scorreUpgradeViewer.text = ConvertDatasForUpgradeViewer(_economy.GetCount(_product.ActionObject.ObjectType), (_product.ActionObject as UpgradeObject).ChangingValue);
    }

    private void UpgradeScorre()
    {
        _economy.ScorreUpgrade(_product.ActionObject as UpgradeObject);
    }

    private string ConvertDatasForUpgradeViewer(int upgradeCount, float changingValue)
    {
        string result = ((changingValue - 1) * upgradeCount).ToString("0.00");

        return "+ " + result + " %";
    }
}
