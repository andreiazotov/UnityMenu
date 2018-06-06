public class SubMenuItemSound : SubMenuItem
{
    private void Start()
    {
        textUI.text = MenuItemAudio.GetSoundStatus();
    }

    public override void OnClick()
    {
        PlayerSettings.soundOn = !PlayerSettings.soundOn;
        textUI.text = MenuItemAudio.GetSoundStatus();
    }
}