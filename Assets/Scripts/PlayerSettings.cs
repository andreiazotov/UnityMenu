using System;
using UnityEngine;

public enum GraphicConfiguration
{
    Low,
    Average,
    High,
    Ultra
}

public enum LanguageConfiguration
{
    EN,
    RU
}

public static class PlayerSettings
{
    public static bool musicOn;
    public static bool soundOn;
    public static LanguageConfiguration language;
    public static GraphicConfiguration graphic;

    public static void LoadSettings()
    {
        musicOn = Convert.ToBoolean(PlayerPrefs.GetInt("musicOn", 0));
        soundOn = Convert.ToBoolean(PlayerPrefs.GetInt("soundOn", 0));
        language = (LanguageConfiguration)PlayerPrefs.GetInt("language", 0);
        graphic = (GraphicConfiguration)PlayerPrefs.GetInt("graphic", 0);
    }

    public static void SaveSettings()
    {
        PlayerPrefs.SetInt("musicOn", Convert.ToInt32(musicOn));
        PlayerPrefs.SetInt("soundOn", Convert.ToInt32(soundOn));
        PlayerPrefs.SetInt("language", (int)language);
        PlayerPrefs.SetInt("graphic", (int)graphic);
        PlayerPrefs.Save();
    }
}
