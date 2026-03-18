using UnityEngine;
using TMPro;

public class SharpenPencil : MonoBehaviour
{
    [HideInInspector] public float sharpenerInk;
    public float sharpenerInkMax = 5;

    public CheckpointSystem player;

    public GameObject sharpenerCanvas;
    public TMP_Text sharpenerHeader;
    public TMP_Text sharpenerCost;

    void Start()
    {
        sharpenerInk = sharpenerInkMax;
        sharpenerCanvas.SetActive(false);
    }

    void Update()
    {
        // Only update UI if this is the current checkpoint
        if (player.currentCheckpoint != transform)
            return;

        float missingInk = player.inkMax - player.ink;
        float refillAmount = Mathf.Min(missingInk, sharpenerInk);

        float refillPercent = Mathf.RoundToInt((refillAmount / player.inkMax) * 100);
        float remainingPercent = Mathf.RoundToInt((sharpenerInk / player.inkMax) * 100);

        float cost = refillPercent * 10;

        sharpenerHeader.text = "This sharpener has " + remainingPercent + "% of power left";
        sharpenerCost.text = "Refill " + refillPercent + "% for " + cost + " coins";
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            sharpenerCanvas.SetActive(true);
            player.currentCheckpoint = transform;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            sharpenerCanvas.SetActive(false);
            if (player.currentCheckpoint == transform)
                player.currentCheckpoint = null;
        }
    }
}