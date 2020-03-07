using UnityEngine;
using UnityEngine.UI;

public class ScorreUpgradeButton : MonoBehaviour
{
    [SerializeField] private Text _name;
    [SerializeField] private Text _price;
    [SerializeField] private ScorreCounter _scorreCounter;
    [SerializeField] private ActionObjectScriptableObject _product;
    [SerializeField] private GameEconomy _economy;

    private Button _selfButton;

    private void Start()
    {
        _selfButton = GetComponentInChildren<Button>();
        _name.text = _product.BuyableObject.Name;
        _price.text = _product.BuyableObject.Price.ToString();

        _selfButton.onClick.AddListener(UpgradeScorre);
    }

    private void Update()
    {
        OpportunityBuy();
    }

    private void OpportunityBuy()
    {
        _selfButton.interactable = _economy.EnoughPoints(_economy.GetPrice(_product.BuyableObject.Price));
    }

    private void UpgradeScorre()
    {
        _economy.ScorreUpgrade(_product.ActionObject as UpgradeObject);
    }
}
