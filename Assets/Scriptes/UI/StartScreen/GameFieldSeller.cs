using UnityEngine;
using UnityEngine.UI;

public class GameFieldSeller : MonoBehaviour, IBuyable
{
    [SerializeField] private string _name;
    [SerializeField] private float _price;
    [SerializeField] private Text _priceViewer;
    [SerializeField] private Button _buyButton;

    public float Price => _price;
    public string Name => _name;

    public ActionObjectType ObjectType => throw new System.NotImplementedException();

    private void Start()
    {
        _priceViewer.text = _price.ToString();
    }

    public void CloseSeller()
    {
        _buyButton.gameObject.SetActive(false);
        _priceViewer.gameObject.SetActive(false);
    }

    public bool isCanBuy(int scorre)
    {
        return scorre >= _price;
    }
}
