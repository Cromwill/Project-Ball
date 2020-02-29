using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ProductPanel : MonoBehaviour
{
    [SerializeField] ActionObjectScriptableObject _product;
    [SerializeField] private Text _nameViewer;
    [SerializeField] private Text _priceViewer;

    private Button _productButton;
    private GameEconomy _economy;

    private event Action<IGeneratedBy> _chooseProduct;

    private void Start()
    {
        _productButton = GetComponentInChildren<Button>();
        _nameViewer.text = _product.ActionObject.name;
        _priceViewer.text = _product.ActionObject.Price.ToString();

        _productButton.onClick.AddListener(OnChooseProduct);
    }

    private void Update()
    {
        OpportunityBuy();
    }

    public void AddListenerToButton(Action<IGeneratedBy> listener, UnityAction closePanel, UnityAction openConfirmPanel, GameEconomy economy)
    {
        _chooseProduct += listener;
        _productButton.onClick.AddListener(closePanel);
        _productButton.onClick.AddListener(openConfirmPanel);
        _economy = economy;
        _economy.PurchaseCompleted += ChangePrice;
        ChangePrice();
    }

    public void ChangePrice()
    {
        _priceViewer.text = _economy.GetPrice(_product.ActionObject).ToString();
    }

    private void OpportunityBuy()
    {
        _productButton.interactable = _economy.EnoughPoints(_economy.GetPrice(_product.ActionObject));
    }

    private void OnChooseProduct()
    {
        _chooseProduct?.Invoke(_product);
    }
}
