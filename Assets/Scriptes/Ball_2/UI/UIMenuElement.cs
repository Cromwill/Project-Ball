using UnityEngine;

public abstract class UIMenuElement : MonoBehaviour
{
    [SerializeField] protected ShopElement[] _elements;
    protected RectTransform _selfTransform;

    public abstract bool IsWork{ get; protected set; }
    public abstract bool IsOpen { get;}

    public virtual void UseMenuElement() { }

    public enum State
    {
        Open = 1,
        Close = -1
    }
}
