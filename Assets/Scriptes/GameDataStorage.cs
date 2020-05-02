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

    public static void SaveScorreCounter(float scorre, float scorrePerSecond, float scorreFactor)
    {
        PlayerPrefs.SetFloat(CurrentLevel + "_scorre", scorre);
        PlayerPrefs.SetFloat(CurrentLevel + "_scorrePerSecond", scorrePerSecond);
        PlayerPrefs.SetFloat(CurrentLevel + "_scorreFactor", scorreFactor);
        PlayerPrefs.Save();
    }

    public static SavedScorre GetScorreCounter()
    {
        if (PlayerPrefs.HasKey(CurrentLevel + "_scorre"))
        {
            float scorre = PlayerPrefs.GetFloat(CurrentLevel + "_scorre");
            float scorrePerSecond = PlayerPrefs.GetFloat(CurrentLevel + "_scorrePerSecond");
            float scorreFactor = PlayerPrefs.GetFloat(CurrentLevel + "_scorreFactor");

            return new SavedScorre(scorre, scorrePerSecond, scorreFactor);
        }

        return null;
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

    public static void SaveInstalledBuffCards(string levelNmae, BuffCardData _cardData, int index)
    {
        string key = levelNmae + "_installedBuffCard_" + index;
        string value = _cardData.NameCard;
        PlayerPrefs.SetString(key, value);
    }

    public static string[] GetInstalledBuffCards(string levelNmae, int count)
    {
        string[] cards = new string[count];

        for(int i = 0; i < count; i++)
        {
            string key = levelNmae + "_installedBuffCard_" + i;

            cards[i] = PlayerPrefs.HasKey(key) ? PlayerPrefs.GetString(key) : null;
        }

        return cards;
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

public class SavedScorre
{
    public SavedScorre(float scorre, float scorrePerSecond, float scorreFactor)
    {
        Scorre = scorre;
        ScorrePerSecond = scorrePerSecond;
        ScorreFactor = scorreFactor;
    }

    public float Scorre { get; }
    public float ScorrePerSecond { get; }
    public float ScorreFactor { get; }
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
