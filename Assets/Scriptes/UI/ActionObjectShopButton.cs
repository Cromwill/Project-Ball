using System;
using UnityEngine;

public class ActionObjectShopButton : ShopElement
{
    [SerializeField] ActionObjectScriptableObject _actionObject;

    public static event Action<ActionObjectScriptableObject> UsingActionObject;

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
