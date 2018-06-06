public class SubMenuItemMusic : SubMenuItem
{
    private void Start()
    {
        textUI.text = MenuItemAudio.GetMusicStatus();
    }

    public override void OnClick()
    {
        PlayerSettings.musicOn = !PlayerSettings.musicOn;
        textUI.text = MenuItemAudio.GetMusicStatus();
    }
}