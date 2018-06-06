using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum Transparency
{
    Null = 0,
    Full = 1
}

public class SubMenuItem : MonoBehaviour
{
    public Text textUI;
    public Image imageUI;
    public float appearanceTime = 0.1f;

    [NonSerialized]
    public Vector2 initialPosition;

    [NonSerialized]
    public Vector2 targetPosition;

    public IEnumerator DecideEnable(bool needEnable)
    {
        var vectorFrom = initialPosition;
        var vectorTo = targetPosition;
        var imageColor = imageUI.color;
        var textColor = textUI.color;
        float alphaFrom = (float)Transparency.Null;
        float alphaTo = (float)Transparency.Full;

        if (!needEnable)
        {
            vectorFrom = targetPosition;
            vectorTo = initialPosition;
            alphaFrom = (float)Transparency.Full;
            alphaTo = (float)Transparency.Null;
        }

        for (float t = 0f; t < appearanceTime; t += Time.deltaTime)
        {
            transform.localPosition = Vector2.Lerp(vectorFrom, vectorTo, t / appearanceTime);
            float alpha = Mathf.Lerp(alphaFrom, alphaTo, t / appearanceTime);
            imageUI.color = new Color(imageColor.r, imageColor.g, imageColor.b, alpha);
            textUI.color = new Color(textColor.r, textColor.g, textColor.b, alpha);
            yield return null;
        }
        imageUI.color = new Color(imageColor.r, imageColor.g, imageColor.b, alphaTo);
        textUI.color = new Color(textColor.r, textColor.g, textColor.b, alphaTo);
        transform.localPosition = vectorTo;
    }

    public void ResetItem()
    {
        transform.localPosition = initialPosition;
        imageUI.color = new Color(imageUI.color.r, imageUI.color.g, imageUI.color.b, (float)Transparency.Null);
        textUI.color = new Color(textUI.color.r, textUI.color.g, textUI.color.b, (float)Transparency.Null);
    }

    public virtual void OnClick()
    {
    }
}