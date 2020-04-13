using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ProductPanel : MonoBehaviour
{
    [SerializeField] protected ActionObjectScriptableObject _product;
    [SerializeField] protected Text _nameViewer;
    [SerializeField] protected Text _priceViewer;
    [SerializeField] protected Image _checkBoxImage;
    [SerializeField] protected ProductPanelState _productState;

    protected Button _productButton;
    protected GameEconomy _economy;
    protected bool _currentState;
    protected event Action<IGeneratedBy> _chooseProduct;
    protected Image _buttonImage;
    protected bool _isPossibleToUse;
    protected ScorreFormConverter _scorreForm;

    public ActionObjectType ProductType => _product.ActionObject.ObjectType;
    private void OnEnable()
    {
        _productButton = GetComponentInChildren<Button>();
    }

    private void Start()
    {
        _nameViewer.text = _product.ActionObject.LevelName.ToUpper();
        _productButton.onClick.AddListener(OnChooseProduct);
        _currentState = _productButton.interactable;
        _buttonImage = _productButton.GetComponent<Image>();
        _isPossibleToUse = true;
    }

    private void Update()
    {
        if (_isPossibleToUse)
        {
            OpportunityBuy();
        }
    }

    public virtual void AddListenerToButton(Action<IGeneratedBy> listener, UnityAction closePanel, UnityAction openConfirmPanel, GameEconomy economy,
        ScorreFormConverter scorreForm)
    {
        _scorreForm = scorreForm;
        _chooseProduct += listener;
        _productButton.onClick.AddListener(closePanel);
        _productButton.onClick.AddListener(openConfirmPanel);
        _economy = economy;
        _economy.PurchaseCompleted += ChangePrice;
        ChangePrice();
    }

    public void ClosePanel()
    {
        _isPossibleToUse = _productButton.interactable = false;
        _priceViewer.text = _scorreForm.OutOfAnchorsMessage;
    }

    public void OpenPanel()
    {
        _isPossibleToUse = true;
        ChangePrice();
    }

    public virtual void ChangePrice()
    {
        var price = _economy.GetPrice(_product.ActionObject.Price, _product.ActionObject.ObjectType);
        _priceViewer.text = _scorreForm.GetConvertedScorre(price).ToString();
    }

    protected void OpportunityBuy()
    {
        _productButton.interactable = _economy.EnoughPoints(_economy.GetPrice(_product.ActionObject.Price, _product.ActionObject.ObjectType));
        if(_currentState != _productButton.interactable)
        {
            TogglePanelState(!_currentState);
            _currentState = _productButton.interactable;
        }
    }

    protected void OnChooseProduct()
    {
        _chooseProduct?.Invoke(_product);
    }

    protected void TogglePanelState(bool stay)
    {
        int index = stay == false ? 0 : 1;

        _buttonImage.sprite = _productState.ButtonSprites[index];
        _checkBoxImage.sprite = _productState.CheckBoxSprites[index];
        _nameViewer.color = _productState.TextColor[index];
    }
}
