using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class DynamicBackground : MonoBehaviour
{
    public RectTransform targetText;  // The text element the background should adjust to
    public Vector2 padding;  // Additional padding to add around the text

    private Image background;
    private RectTransform backgroundRect;

    void Awake()
    {
        background = GetComponent<Image>();
        backgroundRect = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (targetText.gameObject.activeInHierarchy)
        {
            AdjustBackgroundSize();
        }
        else
        {
            background.enabled = false;  // Optionally hide the background if the text is inactive
        }
    }

    void AdjustBackgroundSize()
    {
        backgroundRect.sizeDelta = new Vector2(targetText.sizeDelta.x + padding.x, targetText.sizeDelta.y + padding.y);
        backgroundRect.position = targetText.position;  // Align the background with the text
        background.enabled = true;
    }
}
