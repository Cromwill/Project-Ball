using System.Linq;
using UnityEngine;

public class BuffCardsSetter : MonoBehaviour
{
    [SerializeField] private BuffCardsInventory _inventory;

    private string _levelName;
    private BuffCardPlace[] _cardPlaces;

    public bool IsHaveEmptyPlace => _cardPlaces.Where(a => a.IsEmptyPlace).Count() > 0;

    public void SetCard(BuffCardData _cardData)
    {
        int index = 0;

        for(int i = 0; i < _cardPlaces.Length; i++)
        {
            if (_cardPlaces[i].IsEmptyPlace)
            {
                index = i;
                break;
            }
        }

        _cardPlaces[index].SetCard(_cardData);
        GameDataStorage.SaveInstalledBuffCards(_levelName, _cardData, index);
    }

    public void OpenPanel(string levelName)
    {
        gameObject.SetActive(true);
        _levelName = levelName;
        _inventory.Launch(levelName);

        if (_cardPlaces == null)
        {
            _cardPlaces = GetComponentsInChildren<BuffCardPlace>();
            var savedCards = GameDataStorage.GetInstalledBuffCards(levelName, _cardPlaces.Length).ToArray();

            for(int i = 0; i < _cardPlaces.Length; i++)
            {
                _cardPlaces[i].Initialization(_inventory.GetCard(savedCards[i]));
            }
        }
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}
