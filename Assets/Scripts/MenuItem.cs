using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum Direction
{
    Forward = 1,
    Backward = -1
}

public class MenuItem : MonoBehaviour
{
    [Header("Items")]
    public SubMenuItem[] subMenuItems;

    [Header("Submenu Spacing")]
    [Range(-1000, 1000)]
    public float xOffset;

    [Range(-1000, 1000)]
    public float yOffset;

    [Range(0, 1000)]
    public float verticalOffset;

    [Header("Controllers")]
    public Button button;
    public float animationTime = 0.5f;
    public float leftBorderMargin = 50.0f;
    public float rightBorderMargin = 550.0f;

    private bool _isActive;
    private SubMenuItem[] _subMenuItems;
    private ScrollRect _parentScrollRect;
    private RectTransform _parentRectTransform;

    private void Start()
    {
        _isActive = false;
        _parentRectTransform = transform.parent.GetComponentInParent<RectTransform>();
        _parentScrollRect = transform.parent.GetComponentInParent<ScrollRect>();

        var rectTransform = GetComponent<RectTransform>();
        if (rectTransform == null || subMenuItems == null || subMenuItems.Length == 0)
        {
            return;
        }

        for (int i = 0; i < subMenuItems.Length; i++)
        {
            float xPosition = rectTransform.sizeDelta.x + xOffset;
            float yPosition = rectTransform.sizeDelta.y - yOffset * (i + 1);

            subMenuItems[i].initialPosition = new Vector2(xPosition, yPosition);
            subMenuItems[i].targetPosition = new Vector2(xPosition, yPosition + verticalOffset);
            subMenuItems[i].ResetItem();
        }
    }

    public void OnClick()
    {
        StartCoroutine(DoClick());
    }

    private IEnumerator DoClick()
    {
        _isActive = !_isActive;

        if (_isActive)
        {
            if (_parentScrollRect)
            {
                _parentScrollRect.movementType = ScrollRect.MovementType.Unrestricted;
                _parentScrollRect.horizontal = !_isActive;
            }
            StartCoroutine(ConfigureScroll());
            yield return StartCoroutine(ConfigureSiblings());
            yield return StartCoroutine(ConfigureSubMenu());
        }
        else
        {
            yield return StartCoroutine(ConfigureSubMenu());
            yield return StartCoroutine(ConfigureSiblings());
            if (_parentScrollRect)
            {
                _parentScrollRect.movementType = ScrollRect.MovementType.Elastic;
                _parentScrollRect.horizontal = !_isActive;
            }
        }
    }

    private IEnumerator ConfigureScroll()
    {
        if (_parentRectTransform)
        {
            var vectorFrom = new Vector2(_parentRectTransform.localPosition.x, _parentRectTransform.localPosition.y);
            var vectorTo = new Vector2(-transform.localPosition.x + leftBorderMargin, _parentRectTransform.localPosition.y);
            for (float t = 0f; t < animationTime; t += Time.deltaTime)
            {
                _parentRectTransform.localPosition = Vector2.Lerp(vectorFrom, vectorTo, t / animationTime);
                yield return null;
            }
            _parentRectTransform.localPosition = vectorTo;
        }
    }

    private IEnumerator ConfigureSubMenu()
    {
        for (int i = 0; i < subMenuItems.Length; i++)
        {
            yield return subMenuItems[i].DecideEnable(_isActive);
        }
    }

    private IEnumerator ConfigureSiblings()
    {
        var siblings = transform.parent.GetComponentsInChildren<MenuItem>();
        if (siblings == null || siblings.Length == 0)
        {
            yield break;
        }

        var vectorFrom = new Vector2[siblings.Length];
        var vectorTo = new Vector2[siblings.Length];
        float direction = _isActive ? (float)Direction.Forward : (float)Direction.Backward;
        int itemIndex = transform.GetSiblingIndex();

        for (int i = 0; i < siblings.Length; i++)
        {
            var siblingTransform = siblings[i].transform;

            if (siblingTransform.GetSiblingIndex() != itemIndex)
            {
                siblings[i].button.interactable = !_isActive;
            }

            vectorFrom[i] = siblingTransform.localPosition;
            vectorTo[i] = new Vector2(siblingTransform.localPosition.x + direction * rightBorderMargin, siblingTransform.localPosition.y);
        }

        for (float t = 0f; t < animationTime; t += Time.deltaTime)
        {
            for (int i = 0; i < siblings.Length; i++)
            {
                var siblingTransform = siblings[i].transform;
                if (siblingTransform.GetSiblingIndex() > itemIndex)
                {
                    siblingTransform.localPosition = Vector2.Lerp(vectorFrom[i], vectorTo[i], t / animationTime);
                }
            }
            yield return null;
        }

        for (int i = 0; i < siblings.Length; i++)
        {
            if (siblings[i].transform.GetSiblingIndex() > itemIndex)
            {
                siblings[i].transform.localPosition = vectorTo[i];
            }
        }
    }

    public virtual void RefreshSetting()
    {
    }
}