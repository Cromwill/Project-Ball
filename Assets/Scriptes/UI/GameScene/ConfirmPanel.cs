using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ConfirmPanel : MonoBehaviour
{
    [SerializeField] private Button _confirm;
    [SerializeField] private Button _cancel;
    [SerializeField] private GameObject _shop;

    public void SetConfirmListener(params UnityAction[] listeners)
    {
        SetListeners(_confirm, listeners);
    }

    public void SetCancelListener(params UnityAction[] listeners)
    {
        SetListeners(_cancel, listeners);
    }

    public void ToggleActiveButtons()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        _shop.SetActive(!gameObject.activeSelf);
    }

    private void SetListeners(Button button, UnityAction[] listeners)
    {
        for (int i = 0; i < listeners.Length; i++)
        {
            button.onClick.AddListener(listeners[i]);
        }
    }
}
