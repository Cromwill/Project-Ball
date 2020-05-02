using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuffCardsInventory : MonoBehaviour
{
    [SerializeField] private BuffCardData[] _buffCards;
    [SerializeField] private BuffCard _prefab;
    [SerializeField] private GameObject _prefabsParent;
    [SerializeField] private EquipPanel _equipPanel;
    [SerializeField] private BuffCardsSetter _cardSetter;

    private int[] _cardsCount;
    private BuffCard _currentBuffCard;
    private bool _isFerstlaunch = true;
    private string _levelName;

    public void Launch(string levelName)
    {
        if (_isFerstlaunch)
        {
            AddCardsToInventory();
        }
    }

    public void ConfirmSetCard()
    {
        _cardSetter.SetCard(_currentBuffCard.CardData);
        _currentBuffCard.Remove();
    }

    public BuffCardData GetCard(string datas)
    {
        if (datas == null)
            return null;
        else
            return _buffCards.Where(a => a.NameCard == datas).FirstOrDefault();
    }

    private void AddCardsToInventory()
    {
        if (_cardsCount == null)
            _cardsCount = new int[_buffCards.Length];

        for (int i = 0; i < _buffCards.Length; i++)
        {
            int count = _buffCards[i].GetCount();
            if (_cardsCount[i] < count)
            {
                var addingCount = count - _cardsCount[i];
                for (int j = 0; j < addingCount; j++)
                {
                    var card = Instantiate(_prefab, _prefabsParent.transform);
                    card.Filling(_buffCards[i]);
                    card.CardSetting += SetCard;
                }
            }
        }

        _isFerstlaunch = false;
    }

    private void SetCard(BuffCard card)
    {
        if (_cardSetter.IsHaveEmptyPlace)
        {
            Debug.Log("SetCard");
            _currentBuffCard = card;
            _equipPanel.gameObject.SetActive(true);
            _equipPanel.Filling(this);
        }
    }
}
