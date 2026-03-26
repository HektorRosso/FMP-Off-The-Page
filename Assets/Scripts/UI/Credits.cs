using UnityEngine;

public class Credits : MonoBehaviour
{
    public float scrollSpeed = 1f;

    public float minYPos;
    public float maxYPos;

    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        Vector2 pos = rectTransform.anchoredPosition;

        if (pos.y < maxYPos)
        {
            pos.y += scrollSpeed * Time.deltaTime;
        }
        else
        {
            pos.y = minYPos;
        }

        rectTransform.anchoredPosition = pos;
    }
}
