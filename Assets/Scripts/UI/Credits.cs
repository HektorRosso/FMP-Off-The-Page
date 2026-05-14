using UnityEngine;

public class Credits : MonoBehaviour
{
    [Header("Auto Scroll")]
    public float scrollSpeed = 100f;

    [Header("Bounds")]
    public float minYPos;
    public float maxYPos;

    [Header("Drag")]
    public float dragSensitivity = 1f;

    [Header("Inertia")]
    public float inertiaDamping = 5f;

    private RectTransform rectTransform;

    private float autoY;

    private bool dragging;
    private float lastMouseY;

    private float velocity;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        autoY = rectTransform.anchoredPosition.y;
    }

    void Update()
    {
        // Start drag
        if (Input.GetMouseButtonDown(0))
        {
            Dragging();
            lastMouseY = Input.mousePosition.y;
            velocity = 0f; // cancel previous momentum when new drag starts
        }

        // End drag
        if (Input.GetMouseButtonUp(0))
        {
            NotDragging();
        }

        if (dragging)
        {
            if (autoY > maxYPos)
                autoY = minYPos;

            if (autoY < minYPos)
                autoY = maxYPos;

            float mouseY = Input.mousePosition.y;
            float mouseDelta = mouseY - lastMouseY;

            lastMouseY = mouseY;

            // Convert drag movement into velocity
            velocity = mouseDelta * dragSensitivity / Time.deltaTime;

            autoY += mouseDelta * dragSensitivity;
        }
        else
        {
            // Apply inertia first
            autoY += velocity * Time.deltaTime;

            // Apply friction (slowly reduce velocity)
            velocity = Mathf.Lerp(velocity, 0f, inertiaDamping * Time.deltaTime);

            // Then auto-scroll continues gently on top
            autoY += scrollSpeed * Time.deltaTime;

            // Loop bounds
            if (autoY > maxYPos)
                autoY = minYPos;

            if (autoY < minYPos)
                autoY = maxYPos;
        }

        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x,autoY);
    }

    public void Dragging()
    {
        dragging = true;
    }

    public void NotDragging()
    {
        dragging = false;
    }
}