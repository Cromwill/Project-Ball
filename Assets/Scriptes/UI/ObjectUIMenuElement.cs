using System;
using UnityEngine;
using UnityEngine.UI;

public class ObjectUIMenuElement : UIMenuElement
{
    public static event Action Confirm;

    public override bool IsWork { get => throw new System.NotImplementedException();
        protected set => throw new System.NotImplementedException(); }

    public override bool IsOpen { get => throw new System.NotImplementedException();}

    private void Update()
    {
        
    }

    public override void UseMenuElement()
    {
    }

    public void OnConfirm(bool choose)
    {
        Confirm?.Invoke();
        gameObject.SetActive(false);
    }

}
