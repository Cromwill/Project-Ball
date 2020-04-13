﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GreatePanel : MonoBehaviour
{
    [SerializeField] private ActionObjectSpawner _actionObjectSpawner;
    [SerializeField] private SpawnObjectSpawner _spawnObjectSpawner;
    [SerializeField] private ConfirmPanel _confirmPanel;
    [SerializeField] private Button _startButton;
    [SerializeField] private ScorreFormConverter _scorreForm;

    private ProductPanel[] _productPanels;
    private GameEconomy _economy;

    private void Start()
    {
        _confirmPanel.SetConfirmListener(_actionObjectSpawner.ConfirmSetObject, _spawnObjectSpawner.ConfirmSetObject, _confirmPanel.ToggleActiveButtons);
        _confirmPanel.SetCancelListener(_actionObjectSpawner.EndUse, _spawnObjectSpawner.EndUse, _confirmPanel.ToggleActiveButtons);
        _economy = _actionObjectSpawner.GetComponent<GameEconomy>();
        _productPanels = GetComponentsInChildren<ProductPanel>();
        _actionObjectSpawner.DeletingObject += _confirmPanel.ToggleActiveButtons;
        _actionObjectSpawner.OutOfAnchors += OutOfAnchors;
        _actionObjectSpawner.AnchorsAppeared += ActionAnchorsAppeared;
        _spawnObjectSpawner.OutOfAnchors += OutOfAnchors;

        foreach (var product in _productPanels)
        {
            product.AddListenerToButton(SendObjectToSpawner, ToggleActive, _confirmPanel.ToggleActiveButtons, _economy, _scorreForm);
        }
        _startButton.onClick.Invoke();
        ToggleActive();
    }

    public void ToggleActive()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        _actionObjectSpawner.EnoughAnchors();
        _spawnObjectSpawner.EnoughAnchors();
    }

    private void SendObjectToSpawner(IGeneratedBy actionObject)
    {
        ActionObjectType sendableObjectType = actionObject.ActionObject.ObjectType;

        if (IsActionObject(sendableObjectType))
            _actionObjectSpawner.SetObjectOnScene(actionObject);
        else if (IsSpawnObject(sendableObjectType))
            _spawnObjectSpawner.SetObjectOnScene(actionObject);
    }

    private bool IsActionObject(ActionObjectType type)
    {
        if (type == ActionObjectType.Action || type == ActionObjectType.Phisics)
            return true;
        return false;
    }

    private bool IsSpawnObject(ActionObjectType type)
    {
        if (type == ActionObjectType.Spawn || type == ActionObjectType.UpgradeSpawn)
            return true;
        return false;
    }

    private void OutOfAnchors(TypeForAnchor anchorType)
    {
        ProductPanel[] panels;

        if (anchorType == TypeForAnchor.SpawnObject)
        {
            panels = _productPanels.Where(a => a.ProductType == ActionObjectType.Spawn).ToArray();
        }
        else
        {
            panels = _productPanels.Where(a => IsActionObject(a.ProductType)).ToArray();
        }

        if (panels != null)
        {
            for (int i = 0; i < panels.Length; i++)
            {
                panels[i].ClosePanel();
            }
        }
    }

    private void ActionAnchorsAppeared()
    {
        ProductPanel[] panels = _productPanels.Where(a => IsActionObject(a.ProductType)).ToArray();

        if (panels != null)
        {
            for (int i = 0; i < panels.Length; i++)
            {
                panels[i].OpenPanel();
            }
        }
    }
}
