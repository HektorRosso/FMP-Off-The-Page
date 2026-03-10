using UnityEngine;
using TMPro;

public class CheckpointSystem : MonoBehaviour
{
    [HideInInspector] public float ink;
    public float inkMax = 100f;
    private float lastInk;

    public TMP_Text inkText;

    public DrawingErasing pencil;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ink = inkMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (ink != lastInk)
        {
            float inkPercentage = Mathf.RoundToInt((ink / inkMax) * 100);
            inkText.text = inkPercentage + "%";
            lastInk = ink;
        }
    }
}
