using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ProductPanel : MonoBehaviour
{
    [SerializeField] protected ActionObjectScriptableObject _product;
    [SerializeField] protected Text _nameViewer;
    [SerializeField] protected Text _priceViewer;

    protected Button _productButton;
    protected GameEconomy _economy;

    protected event Action<IGeneratedBy> _chooseProduct;

    private void OnEnable()
    {
        _productButton = GetComponentInChildren<Button>();
    }

    private void Start()
    {
        _nameViewer.text = _product.ActionObject.Name;
        _productButton.onClick.AddListener(OnChooseProduct);
    }

    private void Update()
    {
        OpportunityBuy();
    }

    public virtual void AddListenerToButton(Action<IGeneratedBy> listener, UnityAction closePanel, UnityAction openConfirmPanel, GameEconomy economy)
    {
        _chooseProduct += listener;
        _productButton.onClick.AddListener(closePanel);
        _productButton.onClick.AddListener(openConfirmPanel);
        _economy = economy;
        _economy.PurchaseCompleted += ChangePrice;
        ChangePrice();
    }

    public virtual void ChangePrice()
    {
        _priceViewer.text = _economy.GetPrice(_product.ActionObject.Price, _product.ActionObject.ObjectType).ToString();
    }

    protected void OpportunityBuy()
    {
        _productButton.interactable = _economy.EnoughPoints(_economy.GetPrice(_product.ActionObject.Price, _product.ActionObject.ObjectType));
    }

    protected void OnChooseProduct()
    {
        _chooseProduct?.Invoke(_product);
    }
}
