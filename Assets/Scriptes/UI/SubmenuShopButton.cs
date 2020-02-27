using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmenuShopButton : ShopElement
{

    private void Update()
    {
        if (_menuElement.IsWork)
            _selfButton.interactable = false;
        else
            _selfButton.interactable = true;
    }

    protected override void AddListenerToOnClick()
    {
        _selfButton.onClick.AddListener(_menuElement.UseMenuElement);
    }
}
