using UnityEngine.UI;

public class MenuItemLanguage : MenuItem
{
    public Text currentLanguage;

    public void OnSubMenuItemClicked(int language)
    {
        PlayerSettings.language = (LanguageConfiguration)language;
        RefreshSetting();
    }

    public override void RefreshSetting()
    {
        currentLanguage.text = PlayerSettings.language.ToString();
    }
}