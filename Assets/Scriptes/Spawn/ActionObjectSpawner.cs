using System;
using System.Linq;
using UnityEngine;

public class ActionObjectSpawner : ObjectSpawner
{
    [SerializeField] private ActionObjectScriptableObject _deletedObject;

    public event Action DeletingObject;
    public event Action AnchorsAppeared;

    public override void ConfirmSetObject()
    {
        if (IsUsing)
        {
            if (_currentObject.IsUpgrade())
                _currentObject.UsedUpgrade(true);
            else
            {
                _currentObject.SetObjectOnScene(Instantiate(_currentObject.ActionObject), true);
                Save(_levelName);
            }

            base.ConfirmSetObject();
        }
    }

    public override void DeletedObject(ActionObject actionObject)
    {
        var anchor = _anchors.Where(a => !a.IsFree).First(a => a.InstalledFacility.Equals(actionObject as IUpgradeable));
        FillingObjectSpawner(_deletedObject, anchor);
        OnDeletingObject();
        if (_anchors.Where(a => a.IsFree).Count() > 0)
            OnAnchorsAppeared();
    }

    public override void Load(string level)
    {
        _levelName = level;
        for (int i = 0; i < _anchors.Length; i++)
        {
            if (PlayerPrefs.HasKey(level + "_actionAnchorIndex_" + i + "_positionX"))
            {
                SavedObject savedObject = GameDataStorage.GetSavedObject("action", i);
                var actionObject = Instantiate(Resources.Load<ActionObject>("Prefabs/ActionObject/" + savedObject.Name));
                var anchor = _anchors.Where(a => a.GetPosition() == savedObject.Position).First();
                actionObject.SetPosition(anchor.GetPosition());
                anchor.SetChangeableObject(actionObject);
            }
        }
    }

    public override void Save(string level)
    {
        for (int i = 0; i < _anchors.Length; i++)
        {
            if (!_anchors[i].IsFree)
                GameDataStorage.SaveActionObjects(i, _anchors[i].GetPosition(), _anchors[i].InstalledFacility as ActionObject);
            else
                GameDataStorage.RemoveActionObject(i);
        }
    }

    private void OnDeletingObject() => DeletingObject?.Invoke();

    private void OnAnchorsAppeared() => AnchorsAppeared?.Invoke();
}

