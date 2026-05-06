using UnityEngine;
using System.Collections.Generic;

public class DrawingErasing : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject drawPrefab;

    [Header("Settings")]
    public float pointDistance = 0.01f;
    public float minBrushSize = 0.1f;
    public float maxBrushSize = 1f;
    [HideInInspector] public float drawRadius;
    [HideInInspector] public float eraseRadius;
    [HideInInspector] public float brushSize;

    [Header("Visuals")]
    [SerializeField] private GameObject pencil;
    [SerializeField] private GameObject eraser;

    private LineRenderer currentLine;
    private EdgeCollider2D currentEdge;
    private Vector3 lastPoint;
    private Vector3 startPoint;
    private Camera mainCamera;

    public CheckpointSystem inkChecker;

    private float lastBrushSize;

    private bool angleLocked = false;
    private float lockedAngle;

    private void Awake()
    {
        mainCamera = Camera.main;
        brushSize = minBrushSize;
        lastBrushSize = brushSize;

        drawRadius = brushSize;
        eraseRadius = brushSize;
    }

    private void Update()
    {
        HandleBrushSize();
        HandleDrawing();
        HandleErasing();
        UpdateVisuals();
    }

    // ============================
    // VISUALS
    // ============================
    void UpdateVisuals()
    {
        pencil.SetActive(Input.GetMouseButton(0));
        eraser.SetActive(Input.GetMouseButton(1));

        transform.position = GetMouseWorld();
    }

    // ============================
    // BRUSH SIZE
    // ============================
    void HandleBrushSize()
    {
        transform.position = GetMouseWorld();
        transform.localScale = Vector3.one * brushSize * 10f;

        float scroll = Input.mouseScrollDelta.y;
        if (scroll != 0)
        {
            eraser.SetActive(false);

            brushSize += scroll * 0.1f;
            brushSize = Mathf.Clamp(brushSize, minBrushSize, maxBrushSize);

            drawRadius = brushSize;
            eraseRadius = brushSize;

            if (currentLine != null)
            {
                currentLine.startWidth = brushSize;
                currentLine.endWidth = brushSize;
            }

            lastBrushSize = brushSize;
        }
    }

    // ============================
    // DRAWING
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
        if (inkChecker.ink <= 0f) return;

        GameObject lineObj = Instantiate(drawPrefab);
        currentLine = lineObj.GetComponent<LineRenderer>();
        currentEdge = lineObj.GetComponent<EdgeCollider2D>();

        currentLine.startWidth = brushSize;
        currentLine.endWidth = brushSize;

        Vector3 pos = GetMouseWorld();

        startPoint = pos;
        angleLocked = false;

        currentLine.positionCount = 1;
        currentLine.SetPosition(0, pos);

        lastPoint = pos;
    }

    void ContinueDrawing()
    {
        if (currentLine == null) return;

        Vector3 pos = GetMouseWorld();

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            Vector3 dir = pos - startPoint;

            if (dir.magnitude > 0.05f)
            {
                float angle = Mathf.Atan2(dir.y, dir.x);

                if (!angleLocked)
                {
                    lockedAngle = Mathf.Round(angle / (Mathf.PI / 360f)) * (Mathf.PI / 360f);
                    angleLocked = true;
                }

                float length = dir.magnitude;

                Vector3 snappedDir = new Vector3(Mathf.Cos(lockedAngle), Mathf.Sin(lockedAngle), 0f) * length;

                pos = startPoint + snappedDir;
            }
        }

        if (Time.timeScale == 0)
        {
            StopDrawing();
            return;
        }

        Collider2D hit = Physics2D.OverlapCircle(pos, brushSize);
        if (hit != null && hit.gameObject.layer != LayerMask.NameToLayer("Default"))
        {
            StopDrawing();
            return;
        }

        if (Vector3.Distance(pos, lastPoint) >= pointDistance)
        {
            int count = currentLine.positionCount;
            currentLine.positionCount = count + 1;
            currentLine.SetPosition(count, pos);

            if (currentEdge != null)
                UpdateEdgeCollider(currentLine, currentEdge);

            lastPoint = pos;

            inkChecker.ink -= brushSize * 0.01f;
            inkChecker.ink = Mathf.Max(inkChecker.ink, 0f);

            if (inkChecker.ink <= 0f)
                StopDrawing();
        }
    }

    void StopDrawing()
    {
        currentLine = null;
        currentEdge = null;

        angleLocked = false;
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
    // ERASING
    // ============================
    void HandleErasing()
    {
        if (!Input.GetMouseButton(1)) return;

        EraseAtPoint(GetMouseWorld(), eraseRadius);
    }

    public void EraseAtPoint(Vector3 erasePos, float radius)
    {
        GameObject[] lines = GameObject.FindGameObjectsWithTag("Ground");

        foreach (GameObject lineObj in lines)
        {
            LineRenderer line = lineObj.GetComponent<LineRenderer>();
            EdgeCollider2D edge = lineObj.GetComponent<EdgeCollider2D>();

            if (line == null) continue;

            List<List<Vector3>> segments = new List<List<Vector3>>();
            List<Vector3> currentSegment = new List<Vector3>();

            for (int i = 0; i < line.positionCount; i++)
            {
                Vector3 p = line.GetPosition(i);

                if (Vector3.Distance(p, erasePos) > radius)
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

            if (segments.Count == 0)
            {
                Destroy(lineObj);
                continue;
            }

            line.positionCount = segments[0].Count;
            line.SetPositions(segments[0].ToArray());

            if (edge != null)
                edge.points = ConvertToVector2(segments[0]);

            for (int i = 1; i < segments.Count; i++)
            {
                GameObject newLine = Instantiate(drawPrefab);
                LineRenderer newLineRenderer = newLine.GetComponent<LineRenderer>();
                EdgeCollider2D newEdge = newLine.GetComponent<EdgeCollider2D>();

                newLineRenderer.startWidth = brushSize;
                newLineRenderer.endWidth = brushSize;

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