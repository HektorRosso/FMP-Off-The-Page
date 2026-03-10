using UnityEngine;
using System.Collections.Generic;

public class DrawingErasing : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject drawPrefab;

    [Header("Settings")]
    public float pointDistance = 0.01f;
    public float eraseRadius = 0.25f;

    private LineRenderer currentLine;
    private EdgeCollider2D currentEdge;
    private Vector3 lastPoint;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        HandleDrawing();
        HandleErasing();
    }

    // ============================
    //  DRAWING (Left Click)
    // ============================

    void HandleDrawing()
    {
        if (Input.GetMouseButtonDown(0))
            StartDrawing();

        if (Input.GetMouseButton(0))
            ContinueDrawing();

        if (Input.GetMouseButtonUp(0))
            StopDrawing();
    }

    void StartDrawing()
    {
        GameObject lineObj = Instantiate(drawPrefab);
        currentLine = lineObj.GetComponent<LineRenderer>();
        currentEdge = lineObj.GetComponent<EdgeCollider2D>();

        Vector3 pos = GetMouseWorld();
        currentLine.positionCount = 2;
        currentLine.SetPosition(0, pos);
        currentLine.SetPosition(1, pos);

        currentEdge.points = new Vector2[]
        {
            new Vector2(pos.x, pos.y),
            new Vector2(pos.x, pos.y)
        };

        lastPoint = pos;
    }

    void ContinueDrawing()
    {
        if (currentLine == null) return;

        Vector3 pos = GetMouseWorld();
        if (Vector3.Distance(pos, lastPoint) > pointDistance)
        {
            currentLine.positionCount++;
            int index = currentLine.positionCount - 1;
            currentLine.SetPosition(index, pos);
            UpdateEdgeCollider(currentLine, currentEdge);
            lastPoint = pos;
        }
    }

    void StopDrawing()
    {
        currentLine = null;
        currentEdge = null;
    }

    void UpdateEdgeCollider(LineRenderer line, EdgeCollider2D edge)
    {
        int count = line.positionCount;
        Vector3[] positions = new Vector3[count];
        line.GetPositions(positions);

        Vector2[] colliderPoints = new Vector2[count];
        for (int i = 0; i < count; i++)
            colliderPoints[i] = new Vector2(positions[i].x, positions[i].y);

        edge.points = colliderPoints;
    }

    // ============================
    //  ERASING (Right Click)
    // ============================

    void HandleErasing()
    {
        if (!Input.GetMouseButton(1)) return;

        Vector3 erasePos = GetMouseWorld();

        // Find all lines with tag "Draw"
        GameObject[] lines = GameObject.FindGameObjectsWithTag("Ground");
        foreach (GameObject lineObj in lines)
        {
            LineRenderer line = lineObj.GetComponent<LineRenderer>();
            EdgeCollider2D edge = lineObj.GetComponent<EdgeCollider2D>();

            if (line == null) continue;

            // Split line into segments outside erase radius
            List<List<Vector3>> segments = new List<List<Vector3>>();
            List<Vector3> currentSegment = new List<Vector3>();

            for (int i = 0; i < line.positionCount; i++)
            {
                Vector3 p = line.GetPosition(i);

                if (Vector3.Distance(p, erasePos) > eraseRadius)
                {
                    currentSegment.Add(p);
                }
                else
                {
                    if (currentSegment.Count >= 2)
                        segments.Add(new List<Vector3>(currentSegment));
                    currentSegment.Clear();
                }
            }

            if (currentSegment.Count >= 2)
                segments.Add(currentSegment);

            // Destroy original if fully erased
            if (segments.Count == 0)
            {
                Destroy(lineObj);
                continue;
            }

            // Update first segment on original line
            line.positionCount = segments[0].Count;
            line.SetPositions(segments[0].ToArray());

            if (edge != null)
                edge.points = ConvertToVector2(segments[0]);

            // Create new line objects for remaining segments
            for (int i = 1; i < segments.Count; i++)
            {
                GameObject newLine = Instantiate(lineObj);
                LineRenderer newLineRenderer = newLine.GetComponent<LineRenderer>();
                EdgeCollider2D newEdge = newLine.GetComponent<EdgeCollider2D>();

                newLineRenderer.positionCount = segments[i].Count;
                newLineRenderer.SetPositions(segments[i].ToArray());

                if (newEdge != null)
                    newEdge.points = ConvertToVector2(segments[i]);
            }
        }
    }

    Vector2[] ConvertToVector2(List<Vector3> points)
    {
        Vector2[] result = new Vector2[points.Count];
        for (int i = 0; i < points.Count; i++)
            result[i] = new Vector2(points[i].x, points[i].y);
        return result;
    }

    Vector3 GetMouseWorld()
    {
        Vector3 pos = Input.mousePosition;
        pos.z = 10f;
        return mainCamera.ScreenToWorldPoint(pos);
    }
}