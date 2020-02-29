using System;
using UnityEngine;

public class GreatePanel : MonoBehaviour
{
    [SerializeField] private Animator _selfAnimator;
    [SerializeField] private ActionObjectSpawner _objectSpawner;
    [SerializeField] private ConfirmPanel _confirmPanel;
    

    private void Start()
    {
        _selfAnimator = GetComponent<Animator>();
        _confirmPanel.SetConfirmListener(_objectSpawner.ConfirmSetObject, _confirmPanel.ToggleActiveButtons);
        _confirmPanel.SetCancelListener(_objectSpawner.DeclineSetObject, _confirmPanel.ToggleActiveButtons);

        foreach(var product in GetComponentsInChildren<ProductPanel>())
        {
            product.AddListenerToButton(SendActionObjectToSpawner, Close, _confirmPanel.ToggleActiveButtons);
        }
    }

    public void Open()
    {
        _selfAnimator.Play("Close");
    }

    public void Close()
    {
        _selfAnimator.Play("Open");
    }

    private void SendActionObjectToSpawner(IGeneratedBy actionObject)
    {
        _objectSpawner.SetObjectOnScene(actionObject);
    }
}
