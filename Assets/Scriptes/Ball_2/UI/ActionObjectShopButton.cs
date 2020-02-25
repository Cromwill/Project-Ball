using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionObjectShopButton : ShopElement
{
    [SerializeField] PhysicsActionScriptableObject _actionObject;

    public static event Action<PhysicsActionScriptableObject> UsingActionObject;

    protected override void AddListenerToOnClick()
    {
        _selfButton.onClick.AddListener(OnUsingActionObjectAvatar);
    }

    private void OnUsingActionObjectAvatar()
    {
        _menuElement.gameObject.SetActive(true);
        UsingActionObject?.Invoke(_actionObject);
    }

}
