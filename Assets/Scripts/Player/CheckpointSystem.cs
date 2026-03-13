using UnityEngine;
using TMPro;

public class CheckpointSystem : MonoBehaviour
{
    [HideInInspector] public float ink;
    public float inkMax = 100f;
    private float lastInk;

    [HideInInspector] public float cost;
    public float coins;

    [HideInInspector] public float sharpener;
    [HideInInspector] public float sharpenerMax;

    public TMP_Text inkText;
    public TMP_Text funds;

    public Transform currentCheckpoint;

    [SerializeField] private DrawingErasing pencil;

    void Start()
    {
        ink = inkMax;
        UpdateFunds();
    }

    void Update()
    {
        if (ink != lastInk)
        {
            float inkPercentage = Mathf.RoundToInt((ink / inkMax) * 100);
            inkText.text = inkPercentage + "%";
            lastInk = ink;
        }
    }

    void UpdateFunds()
    {
        funds.text = coins + " coins";
    }

    public void Purchase()
    {
        if (cost > coins || cost == 0)
            return;

        coins -= cost;

        float inkBefore = ink;

        ink = Mathf.Clamp(ink + sharpener, 0, inkMax);

        float inkUsed = ink - inkBefore;

        sharpenerMax = Mathf.Max(0, sharpenerMax - inkUsed);

        UpdateFunds();
    }
}