using UnityEngine;
using UnityEngine.UI;

public class UpgradeProductPanel : ProductPanel
{
    private int _upgradeCount = 0;

    private void OnEnable()
    {
        _productButton = GetComponentInChildren<Button>();
    }

    private void Start()
    {
        _nameViewer.text = _product.ActionObject.LevelName;
        _productButton.onClick.AddListener(OnChooseProduct);
    }

    public override void ChangePrice()
    {
        _priceViewer.text = _economy.GetPrice(_product.ActionObject.Price, _product.ActionObject.ObjectType).ToString();
    }
}
