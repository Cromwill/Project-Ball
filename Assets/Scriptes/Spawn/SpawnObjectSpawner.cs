using System;
using System.Linq;
using UnityEngine;

public class SpawnObjectSpawner : ObjectSpawner
{
    [SerializeField] private PoolForObjects _spawnPool;

    public event Action UsedMaxUpgrade;

    private IActionObjectAnchor _currenUpgradetAnchor;

    public override void ConfirmSetObject()
    {
        if (IsUsing)
        {
            if (_currentObject.IsUpgrade())
            {
                _currentObject.UsedUpgrade(false);
                _currenUpgradetAnchor = _currentObject.CurrentAnchor;
            }
            else
            {
                _currentObject.SetObjectOnScene(_spawnPool.GetObject() as ActionObject, false);
                Save(_levelName);
            }

            var anchors = _anchors.Where(a => !a.IsFree).ToArray();
            anchors = anchors.Where(a => a.InstalledFacility.IsCanUpgrade()).ToArray();
            if (anchors.Length == 0)
                UsedMaxUpgrade?.Invoke();
            base.ConfirmSetObject();
        }
    }

    public override void Load(string level)
    {
        _levelName = level;

        for (int i = 0; i < _anchors.Length; i++)
        {
            if (PlayerPrefs.HasKey(level + "_spawnAnchorIndex_" + i + "_positionX"))
            {
                SavedObject savedObject = GameDataStorage.GetSavedObject("spawn", i);
                var spawn = _spawnPool.GetObject() as Spawn;
                var anchor = _anchors.Where(a => a.GetPosition() == savedObject.Position).First();
                if (anchor.IsFree)
                {
                    spawn.Upgrade(spawn.SpawnTime - PlayerPrefs.GetFloat(level + "_spawnIndex_" + i + "_spawnTime"));
                    spawn.LeaveThePoolAndRun(anchor.GetPosition());
                    anchor.SetChangeableObject(spawn as IUpgradeable);
                }
            }
        }
    }

    public override void Save(string level)
    {
        for (int i = 0; i < _anchors.Length; i++)
        {
            var spawn = _anchors[i].InstalledFacility as Spawn;
            if (!_anchors[i].IsFree)
                GameDataStorage.SaveSpawnObjects(i, _anchors[i].GetPosition(), spawn, spawn.SpawnTime);
        }
    }

    public void ChangeCameraPosition() => (_spawnPool as GlobalSpawn).ChangeSpawnTimeViewerPosition();

    protected override void FillingObjectSpawner(IGeneratedBy actionObject, IActionObjectAnchor anchor)
    {
        var avatar = Instantiate(actionObject.Avatar).GetComponent<Transform>();
        _currentObject = new ObjectSpawnerData(actionObject, avatar);
        if (_currentObject.IsUpgrade())
        {
            anchor = _currenUpgradetAnchor != null ? _currenUpgradetAnchor : GetCorrectAnchorsArray().First();
        }
        else
        {
            anchor = GetCorrectAnchorsArray().First();
        }
        _currentObject.ChangeAnchor(anchor);
        ToggleAnchors();
    }

    protected override IActionObjectAnchor[] GetCorrectAnchorsArray()
    {
        var array = _anchors.Where(a => a.IsFree == !_currentObject.IsUpgrade()).ToArray();
        if(_currentObject.IsUpgrade())
        {
            return array.Where(a => a.InstalledFacility.IsCanUpgrade()).ToArray();
        }

        return array;
    }
}