using UnityEngine;

public class Credits : MonoBehaviour
{
    public float scrollSpeed = 1f;

    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        Vector2 pos = rectTransform.anchoredPosition;

        if (pos.y < 180f)
        {
            pos.y += scrollSpeed * Time.deltaTime;
        }
        else
        {
            pos.y = 0f;
        }

        rectTransform.anchoredPosition = pos;
    }
}
