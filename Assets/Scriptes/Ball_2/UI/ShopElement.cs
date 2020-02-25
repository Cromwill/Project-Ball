using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ShopElement : MonoBehaviour
{
    [SerializeField] protected UIMenuElement _menuElement;
    protected Button _selfButton;

    public bool IsMenuOpen { get => _menuElement.IsOpen;}

    private void Start()
    {
        _selfButton = GetComponent<Button>();
        AddListenerToOnClick();
    }

    protected virtual void AddListenerToOnClick()
    {
    }

    public virtual void Closing()
    {
        if(IsMenuOpen)
        _selfButton.onClick?.Invoke();
    }

}
