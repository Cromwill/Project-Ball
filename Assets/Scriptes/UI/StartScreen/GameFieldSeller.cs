using UnityEngine;

public class GameFieldSeller : MonoBehaviour, IBuyable
{
    [SerializeField] private string _levelName;
    [SerializeField] private float _price;
    [SerializeField] private GameObject _sellerPanel;

    public float Price => _price;
    public string LevelName => _levelName;

    public ActionObjectType ObjectType => throw new System.NotImplementedException();

    public void CloseSeller() => _sellerPanel.SetActive(false);
    public bool isCanBuy(float scorre) => scorre >= _price;
}
