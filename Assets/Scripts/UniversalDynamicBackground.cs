using UnityEngine;
using UnityEngine.UI;
using System.Linq;

[RequireComponent(typeof(Image))]
public class UniversalDynamicBackground : MonoBehaviour
{
    public Canvas canvas;  // Assign the main canvas
    public Vector2 padding;  // Padding around the text
    private RectTransform backgroundRect;
    private Image background;

    void Awake()
    {
        background = GetComponent<Image>();
        backgroundRect = GetComponent<RectTransform>();
        gameObject.SetActive(false);  // Start with the background disabled
        Debug.Log("Background script initialized, waiting for text elements.");
    }

    void Update()
    {
        var texts = canvas.GetComponentsInChildren<Text>().Where(t => t.gameObject.activeInHierarchy).ToArray();
        Debug.Log($"Checking texts: Found {texts.Length} active text elements.");

        if (texts.Length > 0)
        {
            AdjustBackgroundToFitTexts(texts);
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
                Debug.Log("Background Activated");
            }
        }
        else
        {
            if (gameObject.activeSelf)
            {
                gameObject.SetActive(false);
                Debug.Log("Background Deactivated");
            }
        }
    }

    void AdjustBackgroundToFitTexts(Text[] texts)
    {
        if (texts.Length == 0) return;

        var combinedRect = texts.First().rectTransform.rect;
        Vector2 minPoint = texts.First().rectTransform.anchoredPosition - combinedRect.size * 0.5f;
        Vector2 maxPoint = texts.First().rectTransform.anchoredPosition + combinedRect.size * 0.5f;

        foreach (var text in texts.Skip(1))
        {
            var rect = text.rectTransform.rect;
            var position = text.rectTransform.anchoredPosition;
            minPoint = Vector2.Min(minPoint, position - rect.size * 0.5f);
            maxPoint = Vector2.Max(maxPoint, position + rect.size * 0.5f);
        }

        // Set position and size of the background
        Vector2 center = (minPoint + maxPoint) * 0.5f;
        Vector2 size = maxPoint - minPoint + padding;

        backgroundRect.anchoredPosition = center;
        backgroundRect.sizeDelta = size;
        Debug.Log("Background resized and positioned.");
    }
}
