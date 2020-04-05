using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(LevelData))]
public class ActionObjectSpawner : MonoBehaviour
{
    [SerializeField] private Tilemap _actionObjectTilemap;
    [SerializeField] private Tilemap _spawnObjectTilemap;
    [SerializeField] private PoolForObjects _spawnPool;
    [SerializeField] private GameEconomy _gameEconomy;
    [SerializeField] private ActionObjectScriptableObject _deletedObject;

    public event Action DeletingObject;

    private IActionObjectAnchor[] _anchorsForActionObject;
    private IActionObjectAnchor[] _anchorsForSpawnObject;
    private ObjectSpawner _currenObject;
    private string _levelName;

    public event Action OutOfSpawnAnchors;
    public event Action OutOfSpawnUpgrade;
    public bool IsUsing => _currenObject != null;

    private void Awake()
    {
        _anchorsForActionObject = _actionObjectTilemap.GetComponentsInChildren<IActionObjectAnchor>();
        _anchorsForSpawnObject = _spawnObjectTilemap.GetComponentsInChildren<IActionObjectAnchor>();
    }

    public void ChangeAvatarPositionOnScene(IActionObjectAnchor anchor) => _currenObject.ChangeAnchor(anchor);
    public void SetObjectOnScene(IGeneratedBy actionObject) => FillingObjectSpawner(actionObject, null);

    public void DeletedObject(ActionObject actionObject)
    {
        var anchor = _anchorsForActionObject.Where(a => !a.IsFree).First(a => a.InstalledFacility.Equals(actionObject as IUpgradeable));
        FillingObjectSpawner(_deletedObject, anchor);
        OnDeletingObject();
    }

    public void ConfirmSetObject()
    {
        if (_currenObject.IsActionObject() && !_currenObject.IsUpgrade())
            _currenObject.SetObjectOnScene(Instantiate(_currenObject.ActionObject), true);
        else if (_currenObject.IsUpgrade())
            _currenObject.SetObjectOnScene(_currenObject.ActionObject as UpgradeObject);
        else
            _currenObject.SetObjectOnScene(_spawnPool.GetObject() as ActionObject, false);

        _gameEconomy.OnPurchaseCompleted(_currenObject.ActionObject);
        EndUse();
    }

    public void DeclineSetObject() => EndUse();

    public void Save(string level)
    {
        for (int i = 0; i < _anchorsForActionObject.Length; i++)
        {
            if (!_anchorsForActionObject[i].IsFree)
                GameDataStorage.SaveActionObjects(i, _anchorsForActionObject[i].GetPosition(), _anchorsForActionObject[i].InstalledFacility as ActionObject);
            else
                GameDataStorage.RemoveActionObject(i);
        }

        for (int i = 0; i < _anchorsForSpawnObject.Length; i++)
        {
            var spawn = _anchorsForSpawnObject[i].InstalledFacility as Spawn;
            if (!_anchorsForSpawnObject[i].IsFree)
                GameDataStorage.SaveSpawnObjects(i, _anchorsForSpawnObject[i].GetPosition(), spawn, spawn.SpawnTime);
        }
    }

    public void Load(string level)
    {
        _levelName = level;

        for (int i = 0; i < _anchorsForActionObject.Length; i++)
        {
            if (PlayerPrefs.HasKey(level + "_actionAnchorIndex_" + i + "_positionX"))
            {
                SavedObject savedObject = GameDataStorage.GetSavedObject("action", i);
                var actionObject = Instantiate(Resources.Load<ActionObject>("Prefabs/ActionObject/" + savedObject.Name));
                var anchor = _anchorsForActionObject.Where(a => a.GetPosition() == savedObject.Position).First();
                actionObject.SetPosition(anchor.GetPosition());
                anchor.SetChangeableObject(actionObject);
            }
        }
        for (int i = 0; i < _anchorsForSpawnObject.Length; i++)
        {
            if (PlayerPrefs.HasKey(level + "_spawnAnchorIndex_" + i + "_positionX"))
            {
                SavedObject savedObject = GameDataStorage.GetSavedObject("spawn", i);
                var spawn = _spawnPool.GetObject() as Spawn;
                var anchor = _anchorsForSpawnObject.Where(a => a.GetPosition() == savedObject.Position).First();
                if (anchor.IsFree)
                {
                    spawn.Upgrade(spawn.SpawnTime - PlayerPrefs.GetFloat(level + "_spawnIndex_" + i + "_spawnTime"));
                    spawn.LeaveThePoolAndRun(anchor.GetPosition());
                    anchor.SetChangeableObject(spawn as IUpgradeable);
                }
            }
        }
    }

    private void EndUse()
    {
        Destroy(_currenObject.Avatar.gameObject);
        ToggleAnchors();
        Save(_levelName);
        _currenObject = null;
    }

    private void FillingObjectSpawner(IGeneratedBy actionObject, IActionObjectAnchor anchor)
    {
        var avatar = Instantiate(actionObject.Avatar).GetComponent<Transform>();
        _currenObject = new ObjectSpawner(actionObject, avatar);
        anchor = anchor ?? GetCorrectAnchorsArray().First();
        _currenObject.ChangeAnchor(anchor);
        ToggleAnchors();
    }

    private void OnDeletingObject() => DeletingObject?.Invoke();

    private IActionObjectAnchor[] GetCorrectAnchorsArray()
    {
        return _currenObject.IsActionObject() ? _anchorsForActionObject.Where(a => a.IsFree == !_currenObject.IsUpgrade()).ToArray() :
            _anchorsForSpawnObject.Where(a => a.IsFree == !_currenObject.IsUpgrade()).ToArray();
    }

    private void ToggleAnchors()
    {
        var anchors = GetCorrectAnchorsArray();
        for (int i = 0; i < anchors.Length; i++)
            anchors[i].ToggleColor();
    }

    private bool IsCanUsing(IGeneratedBy actionObject)
    {
        if(actionObject.UsedPlace == UsedPlace.SpawnObjectFree)
        {
            return _anchorsForSpawnObject.Where(a => a.IsFree == true).Count() > 0;
        }
        return true;

    }
}
