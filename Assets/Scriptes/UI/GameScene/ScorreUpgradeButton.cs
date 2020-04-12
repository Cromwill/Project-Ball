using System;
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

    public override void AddListenerToButton(Action<IGeneratedBy> listener, UnityAction closePanel, UnityAction openConfirmPanel, GameEconomy economy)
    {
        _economy = economy;
        _economy.PurchaseCompleted += ChangePrice;
        ChangePrice();
    }

    public override void ChangePrice()
    {
        _priceViewer.text = _economy.GetPrice(_product.ActionObject.Price, _product.ActionObject.ObjectType).ToString();
        _scorreUpgradeViewer.text = ConvertDatasDorUpgradeViewer(_economy.GetCount(_product.ActionObject.ObjectType), (_product.ActionObject as UpgradeObject).ChangingValue);
    }

    private void UpgradeScorre()
    {
        _economy.ScorreUpgrade(_product.ActionObject as UpgradeObject);
    }

    private string ConvertDatasDorUpgradeViewer(int upgradeCount, float changingValue)
    {
        string result = ((changingValue - 1) * upgradeCount).ToString("0.##");

        return "+ " + result + " %";
    }
}
