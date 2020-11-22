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
    [SerializeField] protected bool _isBuyWithCommercial;

    protected Button _productButton;
    protected GameEconomy _economy;
    protected bool _currentState;
    protected event Action<IGeneratedBy> _chooseProduct;
    protected Image _buttonImage;
    protected bool _isPossibleToUse;
    protected ScoreFormConverter _scorreForm;
    protected UnityAction[] _actionsForOpenedPanels;
    protected event Action<string> _showCommercial;

    public ActionObjectType ProductType => _product.ActionObject.ObjectType;
    public string ProductName => _product.ActionObject.ObjectName;
    private void OnEnable()
    {
        _productButton = GetComponentInChildren<Button>();
    }

    private void Start()
    {
        if (!_product.IsOpeningObject)
        {
            _nameViewer.text = _product.ActionObject.ObjectName.ToUpper();
            _productButton.onClick.AddListener(OnChooseProduct);
        }
        else
        {
            _nameViewer.text = _product.OpeningObjectText.ToUpper();
        }

        _currentState = _productButton.interactable;
        _buttonImage = _productButton.GetComponent<Image>();
        _isPossibleToUse = _product.IsOpeningObject ? false : true;
    }

    private void Update()
    {
        if (_isPossibleToUse)
        {
            OpportunityBuy();
        }
    }

    public virtual void AddListenerToButton(Action<IGeneratedBy> listener, UnityAction closePanel, UnityAction openConfirmPanel, Action<string> showCommercial,
        GameEconomy economy, ScoreFormConverter scorreForm)
    {
        _scorreForm = scorreForm;
        _chooseProduct += listener;


        if (_product.IsOpeningObject && !_isPossibleToUse)
        {
            _actionsForOpenedPanels = new UnityAction[2];
            _actionsForOpenedPanels[0] = closePanel;
            _actionsForOpenedPanels[1] = openConfirmPanel;
            _showCommercial += showCommercial;
            _productButton.onClick.AddListener(OnShowCommercial);
            _nameViewer.text = _product.OpeningObjectText.ToUpper();
        }
        else
        {
            _productButton.onClick.AddListener(closePanel);
            _productButton.onClick.AddListener(openConfirmPanel);
        }
        _economy = economy;
        _economy.PurchaseCompleted += ChangePrice;
        ChangePrice();
    }

    public void ClosePanel()
    {
        _isPossibleToUse = _productButton.interactable = false;
        _priceViewer.text = _productState.ClosedMessage;
    }

    public void OpenPanel()
    {
        _isPossibleToUse = true;
        ChangePrice();
    }

    public virtual void ChangePrice()
    {
        var price = _economy.GetPrice(_product.ActionObject.Price, _product.ActionObject.ObjectType);
        _priceViewer.text = _scorreForm.GetConvertedScore(price).ToString();
    }

    public void OpenPanelAfterCommercialWathcing()
    {
        _productButton.onClick.RemoveListener(OnShowCommercial);
        _productButton.onClick.AddListener(_actionsForOpenedPanels[0]);
        _productButton.onClick.AddListener(_actionsForOpenedPanels[1]);
        _productButton.onClick.AddListener(OnChooseProduct);
        _nameViewer.text = _product.ActionObject.ObjectName.ToUpper();
        OpenPanel();
    }

    protected void OnShowCommercial()
    {
        _showCommercial?.Invoke(_product.ActionObject.ObjectName);
    }

    protected void OpportunityBuy()
    {
        _productButton.interactable = _economy.EnoughPoints(_economy.GetPrice(_product.ActionObject.Price, _product.ActionObject.ObjectType));
        if (_currentState != _productButton.interactable)
        {
            TogglePanelState(!_currentState);
            _currentState = _productButton.interactable;
        }
    }

    protected void CloseCommertialPanelAfterPurchases()
    {
        _isPossibleToUse = false;
        _productButton.onClick.RemoveAllListeners();
        _productButton.onClick.AddListener(OnShowCommercial);
        _nameViewer.text = _product.OpeningObjectText.ToUpper();
    }

    protected void OnChooseProduct()
    {
        if (_isBuyWithCommercial)
        {
            var commercial = FindObjectOfType<RewardedVideoAds>();
            commercial.UnityAdsDidFinish += AdsShown;
            commercial.ShowRewardedVideo(true);
        }
        else if(_product.IsOpeningObject && _isPossibleToUse)
        {
            _chooseProduct?.Invoke(_product);
            CloseCommertialPanelAfterPurchases();
        }
        else
            _chooseProduct?.Invoke(_product);
    }

    protected virtual void AdsShown()
    {
        var commercial = FindObjectOfType<RewardedVideoAds>();
        commercial.UnityAdsDidFinish -= AdsShown;
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
