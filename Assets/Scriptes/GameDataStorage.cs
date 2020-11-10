using System;
using UnityEngine;

static class GameDataStorage
{
    public static string CurentLevel;
    public static int Counter = 0;

    public static int Count()
    {
        Counter++;
        return Counter;
    }
    public static SavedObject GetSavedObject(string anchorType, int index)
    {
        string keyString = "_" + anchorType + "AnchorIndex_" + index;

        Vector2 savedPosition = new Vector2(PlayerPrefs.GetFloat(CurentLevel + keyString + "_positionX"),
    PlayerPrefs.GetFloat(CurentLevel + keyString + "_positionY"));

        string name = PlayerPrefs.GetString(CurentLevel + keyString + "_object");

        return new SavedObject(name, savedPosition);
    }

    public static void SaveActionObjects(int anchorIndex, Vector2 position, ActionObject actionObject)
    {
        SaveObject("action", anchorIndex, position, actionObject.name.Split(new char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries)[0]);
    }

    public static void SaveScore(SavedScore savedScore)
    {
        SaveScore(CurentLevel, savedScore.Score, savedScore.ScorePerSecond, savedScore.ScoreFactor);
    }

    public static void SaveScore(string level, SavedScore savedScore)
    {
        SaveScore(level, savedScore.Score, savedScore.ScorePerSecond, savedScore.ScoreFactor);
    }

    public static void SaveScore(float scorre, float scorrePerSecond, float scorreFactor)
    {
        SaveScore(CurentLevel, scorre, scorrePerSecond, scorreFactor);
    }

    public static void SaveScore(string level, float scorre, float scorrePerSecond, float scorreFactor)
    {
        PlayerPrefs.SetFloat(level + "_scorre", scorre);
        PlayerPrefs.SetFloat(level + "_scorrePerSecond", scorrePerSecond);
        PlayerPrefs.SetFloat(level + "_scorreFactor", scorreFactor);
        PlayerPrefs.Save();
    }

    public static SavedScore GetSavedScore()
    {
        return GetSavedScore(CurentLevel);
    }

    public static SavedScore GetSavedScore(string levelName)
    {
        if (PlayerPrefs.HasKey(levelName + "_scorre"))
        {
            float scorre = PlayerPrefs.GetFloat(levelName + "_scorre");
            float scorrePerSecond = PlayerPrefs.GetFloat(levelName + "_scorrePerSecond");
            float scorreFactor = PlayerPrefs.GetFloat(levelName + "_scorreFactor");

            return new SavedScore(scorre, scorrePerSecond, scorreFactor);
        }

        return null;
    }

    public static void SaveSpawnObjects(int anchorIndex, Vector2 position, Spawn spawnObject, float spawnTime)
    {
        SaveObject("spawn", anchorIndex, position, spawnObject.name.Split(new char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries)[0]);
        PlayerPrefs.SetFloat(CurentLevel + "_spawnIndex_" + anchorIndex + "_spawnTime", spawnTime);
        PlayerPrefs.Save();
    }

    public static void RemoveActionObject(int anchorIndex)
    {
        PlayerPrefs.DeleteKey(CurentLevel + "_actionAnchorIndex_" + anchorIndex + "_positionX");
        PlayerPrefs.DeleteKey(CurentLevel + "_actionAnchorIndex_" + anchorIndex + "_positionY");
        PlayerPrefs.DeleteKey(CurentLevel + "_actionAnchorIndex_" + anchorIndex + "_object");
        PlayerPrefs.Save();
    }

    public static void SaveUpgradeCount(UpgradesType type, int count)
    {
        string key = type == UpgradesType.Score ? "_scorreUpgradeCount" : "_spawnUpgradeCount";
        PlayerPrefs.SetInt(CurentLevel + key, count);
        PlayerPrefs.Save();
    }

    public static void SaveObjectsOnScene(ObjectsCountOnScene objectsOnScene)
    {
        PlayerPrefs.SetInt(CurentLevel + "_actionObjectsOnScene", objectsOnScene.GetCount(ActionObjectType.Action));
        PlayerPrefs.SetInt(CurentLevel + "_physicsObjectsOnScene", objectsOnScene.GetCount(ActionObjectType.Phisics));
        PlayerPrefs.SetInt(CurentLevel + "_spawnObjectsOnScene", objectsOnScene.GetCount(ActionObjectType.Spawn));
        PlayerPrefs.SetInt(CurentLevel + "_scorreUpgradeOnScene", objectsOnScene.GetCount(ActionObjectType.UpgradeScorre));
        PlayerPrefs.SetInt(CurentLevel + "_spawnUpgradeOnScene", objectsOnScene.GetCount(ActionObjectType.UpgradeSpawn));
        PlayerPrefs.Save();
    }

    public static ObjectsCountOnScene GetObjectsOnCurrentScene()
    {
        ObjectsCountOnScene objectsOnScene = new ObjectsCountOnScene();
        objectsOnScene.SetCount(ActionObjectType.Action, PlayerPrefs.GetInt(CurentLevel + "_actionObjectsOnScene"));
        objectsOnScene.SetCount(ActionObjectType.Phisics, PlayerPrefs.GetInt(CurentLevel + "_physicsObjectsOnScene"));
        objectsOnScene.SetCount(ActionObjectType.Spawn, PlayerPrefs.GetInt(CurentLevel + "_spawnObjectsOnScene"));
        objectsOnScene.SetCount(ActionObjectType.UpgradeScorre, PlayerPrefs.GetInt(CurentLevel + "_scorreUpgradeOnScene"));
        objectsOnScene.SetCount(ActionObjectType.UpgradeSpawn, PlayerPrefs.GetInt(CurentLevel + "_spawnUpgradeOnScene"));

        return objectsOnScene;
    }

    public static void SaveUTM(string utm) => PlayerPrefs.SetString("utm", utm);

    public static string GetUTM() => PlayerPrefs.GetString("utm");

    public static int NumberOfLaunches(int levelIndex)
    {
        string key = "NumberOfLaunches_" + levelIndex.ToString();
        int count = PlayerPrefs.GetInt(key);
        count++;

        Debug.Log(key + count);
        PlayerPrefs.SetInt(key, count);
        PlayerPrefs.Save();
        return count;
    }

    public static bool IsProductOpened(string name)
    {
        if (PlayerPrefs.HasKey("OpenedProduct_level_" + CurentLevel + "_" + name))
            Debug.Log("Load - OpenedProduct_level_" + CurentLevel + "_" + name);

        return PlayerPrefs.HasKey("OpenedProduct_level_" + CurentLevel + "_" + name);
    }

    public static void SaveOpenedProduct(string name)
    {
        Debug.Log("Save - OpenedProduct_level_" + CurentLevel + "_" + name);
        PlayerPrefs.SetInt("OpenedProduct_level_" + CurentLevel + "_" + name, 1);
        PlayerPrefs.Save();
    }

    private static void SaveObject(string anchorType, int index, Vector2 position, string name)
    {
        string key = "_" + anchorType + "AnchorIndex_" + index;
        PlayerPrefs.SetFloat(CurentLevel + key + "_positionX", position.x);
        PlayerPrefs.SetFloat(CurentLevel + key + "_positionY", position.y);
        PlayerPrefs.SetString(CurentLevel + key + "_object", name);
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

public class SavedScore
{
    public SavedScore(float score, float scorePerSecond, float scoreFactor)
    {
        Score = score;
        ScorePerSecond = scorePerSecond;
        ScoreFactor = scoreFactor;
    }

    public float Score { get; set; }
    public float ScorePerSecond { get; }
    public float ScoreFactor { get; }

    public void AddScore(float time)
    {
        Score += ScorePerSecond * time;
    }

    public void ReductionScore(float score)
    {
        Score -= score;
    }
}


public enum UpgradesType
{
    Score,
    Spawn
}

public enum ColorPlace
{
    Background,
    MidleGround
}
