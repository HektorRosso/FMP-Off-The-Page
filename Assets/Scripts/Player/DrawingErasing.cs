using UnityEngine;

public class DrawingErasing : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject brush;

    private LineRenderer currentLineRenderer;
    private EdgeCollider2D edgeCollider;

    private Vector3 lastPos;

    void Update()
    {
        Draw();
    }

    void Draw()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            CreateBrush();
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10f;

            Vector3 worldPos = mainCamera.ScreenToWorldPoint(mousePos);

            if (Vector3.Distance(worldPos, lastPos) > 0.05f)
            {
                AddPoint(worldPos);
                lastPos = worldPos;
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            currentLineRenderer = null;
            edgeCollider = null;
        }
    }

    void CreateBrush()
    {
        GameObject brushInstance = Instantiate(brush);
        currentLineRenderer = brushInstance.GetComponent<LineRenderer>();
        edgeCollider = brushInstance.GetComponent<EdgeCollider2D>();

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(mousePos);

        currentLineRenderer.positionCount = 2;
        currentLineRenderer.SetPosition(0, worldPos);
        currentLineRenderer.SetPosition(1, worldPos);

        edgeCollider.points = new Vector2[] { worldPos, worldPos };

        lastPos = worldPos;
    }

    void AddPoint(Vector3 pointPos)
    {
        if (currentLineRenderer == null) return;

        currentLineRenderer.positionCount++;

        int positionIndex = currentLineRenderer.positionCount - 1;
        currentLineRenderer.SetPosition(positionIndex, pointPos);

        UpdateCollider();
    }

    void UpdateCollider()
    {
        int count = currentLineRenderer.positionCount;

        Vector3[] linePositions = new Vector3[count];
        currentLineRenderer.GetPositions(linePositions);

        Vector2[] colliderPoints = new Vector2[count];

        for (int i = 0; i < count; i++)
        {
            colliderPoints[i] = new Vector2(linePositions[i].x, linePositions[i].y);
        }

        edgeCollider.points = colliderPoints;
    }
}