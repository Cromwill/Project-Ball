using System.Linq;
using UnityEngine;

public class SpawnObjectSpawner : ObjectSpawner
{
    [SerializeField] private PoolForObjects _spawnPool;

    public override void SetObjectOnScene(IGeneratedBy actionObject)
    {
        if(actionObject.UsedPlace == UsedPlace.Bound)
        {

        }

        base.SetObjectOnScene(actionObject);
    }

    public override void ConfirmSetObject()
    {
        if (IsUsing)
        {
            if (_currentObject.IsUpgrade())
                _currentObject.UsedUpgrade(false);
            else
                _currentObject.SetObjectOnScene(_spawnPool.GetObject() as ActionObject, false);

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
}