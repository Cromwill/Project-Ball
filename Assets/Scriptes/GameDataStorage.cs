using System;
using UnityEngine;

static class GameDataStorage
{
    public static string CurrentLevel;

    public static SavedObject GetSavedObject(string anchorType, int index)
    {
        string keyString = "_" + anchorType + "AnchorIndex_" + index;

        Vector2 savedPosition = new Vector2(PlayerPrefs.GetFloat(CurrentLevel + keyString + "_positionX"),
    PlayerPrefs.GetFloat(CurrentLevel + keyString + "_positionY"));

        string name = PlayerPrefs.GetString(CurrentLevel + keyString + "_object");

        return new SavedObject(name, savedPosition);
    }

    public static void SaveActionObjects(int anchorIndex, Vector2 position, ActionObject actionObject)
    {
        SaveObject("action", anchorIndex, position, actionObject.name.Split(new char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries)[0]);
    }

    public static void SaveSpawnObjects(int anchorIndex, Vector2 position, Spawn spawnObject, float spawnTime)
    {
        SaveObject("spawn", anchorIndex, position, spawnObject.name.Split(new char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries)[0]);
        PlayerPrefs.SetFloat(CurrentLevel + "_spawnIndex_" + anchorIndex + "_spawnTime", spawnTime);
        PlayerPrefs.Save();
    }

    public static void RemoveActionObject(int anchorIndex)
    {
        PlayerPrefs.DeleteKey(CurrentLevel + "_actionAnchorIndex_" + anchorIndex + "_positionX");
        PlayerPrefs.DeleteKey(CurrentLevel + "_actionAnchorIndex_" + anchorIndex + "_positionY");
        PlayerPrefs.DeleteKey(CurrentLevel + "_actionAnchorIndex_" + anchorIndex + "_object");
        PlayerPrefs.Save();
    }

    public static void SaveUpgradeCount(UpgradesType type, int count)
    {
        string key = type == UpgradesType.Scorre ? "_scorreUpgradeCount" : "_spawnUpgradeCount";
        PlayerPrefs.SetInt(CurrentLevel + key, count);
        PlayerPrefs.Save();
    }

    public static void SaveObjectsOnScene(ObjectsCountOnScene objectsOnScene)
    {
        PlayerPrefs.SetInt(CurrentLevel + "_actionObjectsOnScene", objectsOnScene.GetCount(ActionObjectType.Action));
        PlayerPrefs.SetInt(CurrentLevel + "_physicsObjectsOnScene", objectsOnScene.GetCount(ActionObjectType.Phisics));
        PlayerPrefs.SetInt(CurrentLevel + "_spawnObjectsOnScene", objectsOnScene.GetCount(ActionObjectType.Spawn));
        PlayerPrefs.SetInt(CurrentLevel + "_scorreUpgradeOnScene", objectsOnScene.GetCount(ActionObjectType.UpgradeScorre));
        PlayerPrefs.SetInt(CurrentLevel + "_spawnUpgradeOnScene", objectsOnScene.GetCount(ActionObjectType.UpgradeSpawn));
        PlayerPrefs.Save();
    }

    public static ObjectsCountOnScene GetObjectsOnCurrentScene()
    {
        ObjectsCountOnScene objectsOnScene = new ObjectsCountOnScene();
        objectsOnScene.SetCount(ActionObjectType.Action, PlayerPrefs.GetInt(CurrentLevel + "_actionObjectsOnScene"));
        objectsOnScene.SetCount(ActionObjectType.Phisics, PlayerPrefs.GetInt(CurrentLevel + "_physicsObjectsOnScene"));
        objectsOnScene.SetCount(ActionObjectType.Spawn, PlayerPrefs.GetInt(CurrentLevel + "_spawnObjectsOnScene"));
        objectsOnScene.SetCount(ActionObjectType.UpgradeScorre, PlayerPrefs.GetInt(CurrentLevel + "_scorreUpgradeOnScene"));
        objectsOnScene.SetCount(ActionObjectType.UpgradeSpawn, PlayerPrefs.GetInt(CurrentLevel + "_spawnUpgradeOnScene"));

        return objectsOnScene;
    }

    private static void SaveObject(string anchorType, int index, Vector2 position, string name)
    {
        string key = "_" + anchorType + "AnchorIndex_" + index;
        PlayerPrefs.SetFloat(CurrentLevel + key + "_positionX", position.x);
        PlayerPrefs.SetFloat(CurrentLevel + key + "_positionY", position.y);
        PlayerPrefs.SetString(CurrentLevel + key + "_object", name);
        PlayerPrefs.Save();
    }
}

public class TimeScorreData
{
    public int Scorre;
    public float Time;

    public TimeScorreData(int scorre, float time)
    {
        Scorre = scorre;
        Time = time;
    }
}

public class ScorrePerTime
{
    private TimeScorreData[] _datas;
    private int _dataCounter = 0;
    private float _currentValue;

    public ScorrePerTime(int count, float startTime, float scorrePerSecond)
    {
        _datas = new TimeScorreData[count];
        _datas[_dataCounter] = new TimeScorreData((int)scorrePerSecond, startTime);
        _currentValue = scorrePerSecond;
    }

    public float GetValue(int point, float time)
    {
        if (_dataCounter == 0)
        {
            int scorreSum = 0;
            float averageTime;
            int firstIndex;

            _dataCounter = GetNextIndex();
            firstIndex = _datas[_dataCounter] == null ? 0 : GetNextIndex();
            _datas[_dataCounter] = new TimeScorreData(point, time);

            for (int i = 0; i < _datas.Length; i++)
            {
                if (_datas[i] != null)
                    scorreSum += _datas[i].Scorre;
            }

            averageTime = _datas[_dataCounter].Time - _datas[firstIndex].Time;
            var value = scorreSum / averageTime;
            if (value > _currentValue)
                _currentValue = value;
        }

        return _currentValue;
    }

    private int GetNextIndex()
    {
        int index = _dataCounter + 1;
        return index == _datas.Length ? 0 : index;
    }
}

public class SavedObject
{
    public SavedObject(string name, Vector2 position)
    {
        Name = name;
        Position = position;
    }

    public string Name { get; }
    public Vector2 Position { get; }
}

public enum UpgradesType
{
    Scorre,
    Spawn
}

public enum ColorPlace
{
    Background,
    MidleGround
}
