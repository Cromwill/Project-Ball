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
        CustomPlayerPrefs.SetFloat(CurrentLevel + "_spawnIndex_" + anchorIndex + "_spawnTime", spawnTime);
    }

    public static void RemoveActionObject(int anchorIndex)
    {
        PlayerPrefs.DeleteKey(CurrentLevel + "_actionAnchorIndex_" + anchorIndex + "_positionX");
        PlayerPrefs.DeleteKey(CurrentLevel + "_actionAnchorIndex_" + anchorIndex + "_positionY");
        PlayerPrefs.DeleteKey(CurrentLevel + "_actionAnchorIndex_" + anchorIndex + "_object");
    }

    private static void SaveObject(string anchorType, int index, Vector2 position, string name)
    {
        string key = "_" + anchorType + "AnchorIndex_" + index;
        CustomPlayerPrefs.SetFloat(CurrentLevel + key + "_positionX", position.x);
        CustomPlayerPrefs.SetFloat(CurrentLevel + key + "_positionY", position.y);
        CustomPlayerPrefs.SetString(CurrentLevel + key + "_object", name);
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

    public ScorrePerTime(int count, float startTime)
    {
        _datas = new TimeScorreData[count];
        _datas[_dataCounter] = new TimeScorreData(0, startTime);
    }

    public float GetValue(int point, float time)
    {
        int scorreSum = 0;
        float averageTime = 0;
        int firstIndex = 0;

        _dataCounter = GetNextIndex();
        firstIndex = _datas[_dataCounter] == null ? 0 : GetNextIndex();
        _datas[_dataCounter] = new TimeScorreData(point, time);


        for (int i = 0; i < _datas.Length; i++)
        {
            if (_datas[i] != null)
                scorreSum += _datas[i].Scorre;
        }

        averageTime = _datas[_dataCounter].Time - _datas[firstIndex].Time;
        return scorreSum / averageTime;
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