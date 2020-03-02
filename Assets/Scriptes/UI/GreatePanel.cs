﻿using UnityEngine;
using UnityEngine.UI;

public class GreatePanel : MonoBehaviour
{
    [SerializeField] private ActionObjectSpawner _objectSpawner;
    [SerializeField] private ConfirmPanel _confirmPanel;
    [SerializeField] private GameEconomy _economy;
    [SerializeField] private Button _startButton;

    private ProductPanel[] _productPanels;

    private void Start()
    {
        _confirmPanel.SetConfirmListener(_objectSpawner.ConfirmSetObject, _confirmPanel.ToggleActiveButtons);
        _confirmPanel.SetCancelListener(_objectSpawner.DeclineSetObject, _confirmPanel.ToggleActiveButtons);
        _productPanels = GetComponentsInChildren<ProductPanel>();
        Debug.Log("ProductPanel count - " + _productPanels.Length);

        foreach (var product in _productPanels)
        {
            product.AddListenerToButton(SendActionObjectToSpawner, ToggleActive, _confirmPanel.ToggleActiveButtons, _economy);
        }
        _startButton.onClick.Invoke();
        ToggleActive();
    }

    public void ToggleActive()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    private void SendActionObjectToSpawner(IGeneratedBy actionObject)
    {
        _objectSpawner.SetObjectOnScene(actionObject);
    }
}
