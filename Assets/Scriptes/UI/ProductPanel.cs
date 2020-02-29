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

    private event Action<IGeneratedBy> _chooseProduct;

    private void Start()
    {
        _productButton = GetComponentInChildren<Button>();
        _nameViewer.text = _product.ActionObject.name;
        _priceViewer.text = _product.ActionObject.Price.ToString();

        _productButton.onClick.AddListener(OnChooseProduct);
    }

    public void AddListenerToButton(Action<IGeneratedBy> listener, UnityAction closePanel, UnityAction openConfirmPanel)
    {
        _chooseProduct += listener;
        _productButton.onClick.AddListener(closePanel);
        _productButton.onClick.AddListener(openConfirmPanel);
    }

    public void RemoveListenerToButton(Action<IGeneratedBy> listener)
    {
        _chooseProduct -= listener;
    }

    private void OnChooseProduct()
    {
        _chooseProduct?.Invoke(_product);
    }
}
