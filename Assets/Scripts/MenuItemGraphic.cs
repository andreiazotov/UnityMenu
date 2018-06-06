using UnityEngine.UI;

public class MenuItemGraphic : MenuItem
{
    public Text graphic;

    public void OnSubMenuItemClicked(int configuration)
    {
        PlayerSettings.graphic = (GraphicConfiguration)configuration;
        RefreshSetting();
    }

    public override void RefreshSetting()
    {
        graphic.text = PlayerSettings.graphic.ToString();
    }
}