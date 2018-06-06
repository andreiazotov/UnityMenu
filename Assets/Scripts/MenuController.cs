using UnityEngine;

public class MenuController : MonoBehaviour
{
    [Header("Items")]
    public MenuItem[] menuItems;

    [Header("Space between items")]
    [Range(0, 500)]
    public float offset;

    private void Awake()
    {
        PlayerSettings.LoadSettings();
    }

    private void Start()
    {
        if (menuItems == null || menuItems.Length == 0)
        {
            return;
        }

        int itemsCount = menuItems.Length;
        float totalContentWidth = 0.0f;
        for (int i = 0; i < itemsCount; i++)
        {
            var rectTransfrom = menuItems[i].GetComponent<RectTransform>();
            if (rectTransfrom == null)
            {
                continue;
            }

            var menuItem = menuItems[i];
            float menuItemWidth = rectTransfrom.sizeDelta.x;
            float xPosition = (i + 1) * offset + i * menuItemWidth;
            float yPosition = menuItem.transform.localPosition.y;
            menuItem.RefreshSetting();
            menuItem.transform.localPosition = new Vector2(xPosition, yPosition);
            totalContentWidth += menuItemWidth;
        }

        var contentRectTransform = GetComponent<RectTransform>();
        if (contentRectTransform)
        {
            contentRectTransform.sizeDelta = new Vector2(totalContentWidth + offset * (itemsCount + 1), contentRectTransform.sizeDelta.y);
        }
    }

    private void OnApplicationQuit()
    {
        PlayerSettings.SaveSettings();
    }
}
