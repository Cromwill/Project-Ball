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

    private void OnEnable()
    {
        _productButton = GetComponentInChildren<Button>();
    }

    private void Start()
    {
        _nameViewer.text = _product.BuyableObject.Name;
        _priceViewer.text = _product.BuyableObject.Price.ToString();

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
        _priceViewer.text = _economy.GetPrice(_product.BuyableObject.Price).ToString();
    }

    private void OpportunityBuy()
    {
        _productButton.interactable = _economy.EnoughPoints(_economy.GetPrice(_product.BuyableObject.Price));
    }

    private void OnChooseProduct()
    {
        _chooseProduct?.Invoke(_product);
    }
}
