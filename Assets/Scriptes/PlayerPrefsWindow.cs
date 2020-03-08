using System;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

public class PlayerPrefsWindow : EditorWindow
{

    IPrefsTable[] tables =
    {
        new PrefsTable<int>(PlayerPrefs.GetInt),
        new PrefsTable<float>(PlayerPrefs.GetFloat),
        new PrefsTable<string>(PlayerPrefs.GetString),
    };

    [MenuItem("Window/Player Prefs")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(PlayerPrefsWindow));
    }

    void OnGUI()
    {
        if (GUILayout.Button("Clear Prefs"))
        {
            PlayerPrefs.DeleteAll();
        }

        GUILayout.BeginHorizontal();
        {

            for (int i = 0; i < tables.Length; i++)
            {
                var table = tables[i];

                GUILayout.BeginVertical();
                {
                    GUILayout.BeginArea(new Rect(i * 200, 25, 200, 400));
                    GUILayout.Label(table.Type + ":");
                    if (PlayerPrefs.HasKey(table.Type))
                    {
                        DrawKeys(PlayerPrefs.GetString(table.Type).Split(','), table.Get);
                    }
                    GUILayout.EndArea();
                }
                GUILayout.EndVertical();

            }
        }
        GUILayout.EndHorizontal();
    }

    void DrawKeys(string[] keys, Func<string, object> getter)
    {
        foreach (var key in keys)
        {
            GUILayout.Label(string.Format("{0} : {1}", key, getter(key).ToString()));
        }
    }

    interface IPrefsTable
    {
        string Type { get; }
        object Get(string key);
    }

    class PrefsTable<T> : IPrefsTable
    {
        public Func<string, T> Getter;

        public PrefsTable(Func<string, T> getter)
        {
            Getter = getter;
        }

        public string Type
        {
            get
            {
                return TypeWrapper.TypeWrap[typeof(T)] + "Keys";
            }
        }

        public object Get(string key)
        {
            return Getter(key);
        }
    }

    private static class TypeWrapper
    {
        public static Dictionary<Type, string> TypeWrap = new Dictionary<System.Type, string>();

        static TypeWrapper()
        {
            TypeWrap.Add(typeof(int), "Int");
            TypeWrap.Add(typeof(string), "String");
            TypeWrap.Add(typeof(float), "Float");
        }
    }

}

# endif

public static class CustomPlayerPrefs
{
    public static void SetFloat(string key, float value)
    {
        SaveKey(key, "FloatKeys");
        PlayerPrefs.SetFloat(key, value);
    }

    public static void SetInt(string key, int value)
    {
        SaveKey(key, "IntKeys");
        PlayerPrefs.SetInt(key, value);
    }

    public static void SetString(string key, string value)
    {
        SaveKey(key, "StringKeys");
        PlayerPrefs.SetString(key, value);
    }

    private static void SaveKey(string key, string type)
    {
        if (PlayerPrefs.HasKey(type))
        {
            string keys = PlayerPrefs.GetString(type);
            keys += "," + key;
            PlayerPrefs.SetString(type, keys);
        }
        else
        {
            PlayerPrefs.SetString(type, key);
        }
    }
}