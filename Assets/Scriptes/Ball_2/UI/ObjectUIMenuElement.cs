using System;

public class ObjectUIMenuElement : UIMenuElement
{
    public static event Action<bool> Confirm;

    public override bool IsWork { get => throw new System.NotImplementedException();
        protected set => throw new System.NotImplementedException(); }

    public override bool IsOpen { get => throw new System.NotImplementedException();}

    public override void UseMenuElement()
    {
      
    }

    public void OnConfirm(bool choose)
    {
        Confirm?.Invoke(choose);
        gameObject.SetActive(false);
    }

}
