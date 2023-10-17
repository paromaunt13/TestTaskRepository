using UnityEngine;

public class PersistentDataManager
{
    public static void SetBool(string key, bool value)
    {
        PlayerPrefs.SetInt(key, value ? 1 : 0);
        PlayerPrefs.Save();
    }
    public static bool GetBool(string key)
    {
        return PlayerPrefs.GetInt(key, 0) == 1;
    }

    public static void SetInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
        PlayerPrefs.Save();
    }
    public static int GetInt(string key)
    {
        return PlayerPrefs.GetInt(key);
    }

    public static bool CheckKey(string key)
    {
        return PlayerPrefs.HasKey(key);
    }
}
