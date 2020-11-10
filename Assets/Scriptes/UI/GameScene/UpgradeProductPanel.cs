using UnityEngine.UI;

public class UpgradeProductPanel : ProductPanel
{
    private void OnEnable()
    {
        _productButton = GetComponentInChildren<Button>();
    }

    private void Start()
    {
        _nameViewer.text = _product.ActionObject.ObjectName.ToUpper();
        _productButton.onClick.AddListener(OnChooseProduct);
        _currentState = _productButton.interactable;
        _buttonImage = _productButton.GetComponent<Image>();
        _isPossibleToUse = true;
    }

    public override void ChangePrice()
    {
        var price = _economy.GetPrice(_product.ActionObject.Price, _product.ActionObject.ObjectType);
        _priceViewer.text = _scorreForm.GetConvertedScore(price);
    }
}
