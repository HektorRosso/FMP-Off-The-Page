using UnityEngine;
using TMPro;

public class CheckpointSystem : MonoBehaviour
{
    [Header("Player Ink")]
    public float ink;
    public float inkMax = 100f;
    private float lastInk;

    [Header("Currency")]
    public float coins;

    [Header("UI")]
    public TMP_Text inkText;
    public TMP_Text funds;

    [HideInInspector] public Transform currentCheckpoint;

    void Start()
    {
        UpdateInk();
        UpdateFunds();
    }

    void Update()
    {
        if (ink != lastInk)
            UpdateInk();
    }

    void UpdateInk()
    {
        float percent = Mathf.RoundToInt((ink / inkMax) * 100);
        inkText.text = percent + "%";
        lastInk = ink;
    }

    void UpdateFunds()
    {
        funds.text = coins + " coins";
    }

    public void Purchase()
    {
        if (currentCheckpoint == null)
            return;

        // Get the Sharpener component from the current checkpoint
        SharpenPencil sharpener = currentCheckpoint.GetComponent<SharpenPencil>();
        if (sharpener == null)
            return;

        float missingInk = inkMax - ink;
        float refillAmount = Mathf.Min(missingInk, sharpener.sharpenerInk);

        float refillPercent = Mathf.RoundToInt((refillAmount / inkMax) * 100);
        float cost = refillPercent * 10;

        if (coins < cost)
            return;

        if (sharpener.free == false) coins -= cost;

        ink += refillAmount;
        sharpener.sharpenerInk -= refillAmount;

        UpdateFunds();
    }
}