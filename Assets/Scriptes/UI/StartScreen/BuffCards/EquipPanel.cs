using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipPanel : MonoBehaviour
{
    private BuffCardsInventory _inventory;

    public void Filling(BuffCardsInventory inventory)
    {
        if (_inventory == null)
            _inventory = inventory;
    }

    public void Confirm()
    {
        _inventory.ConfirmSetCard();
        gameObject.SetActive(false);
    }

    public void Cancel()
    {

        gameObject.SetActive(false);
    }
    
}
