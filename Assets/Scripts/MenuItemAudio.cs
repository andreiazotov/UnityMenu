using UnityEngine.UI;

public class MenuItemAudio : MenuItem
{
    private const string TOGGLE_ON = "ON";
    private const string TOGGLE_OFF = "OFF";

    public Text musicStatus;
    public Text soundStatus;

    public override void RefreshSetting()
    {
        musicStatus.text = GetMusicStatus();
        soundStatus.text = GetSoundStatus();
    }

    public static string GetMusicStatus()
    {
        return string.Format("Music {0}", PlayerSettings.musicOn ? TOGGLE_ON : TOGGLE_OFF);
    }

    public static string GetSoundStatus()
    {
        return string.Format("Sound {0}", PlayerSettings.soundOn ? TOGGLE_ON : TOGGLE_OFF);
    }
}