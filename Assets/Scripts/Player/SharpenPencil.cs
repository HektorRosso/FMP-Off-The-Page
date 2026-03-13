using UnityEngine;
using TMPro;

public class SharpenPencil : MonoBehaviour
{
    [HideInInspector] public float sharpenerInk;
    public float sharpenerInkMax;

    public CheckpointSystem inkChecker;

    public GameObject sharpenerCanvas;
    public TMP_Text sharpenerHeader;
    public TMP_Text sharpenerText;
    public TMP_Text sharpenerCost;

    void Start()
    {
        sharpenerCanvas.SetActive(false);
        inkChecker.sharpenerMax = sharpenerInkMax;
    }

    void Update()
    {
        float missingInk = inkChecker.inkMax - inkChecker.ink;

        sharpenerInk = Mathf.Min(missingInk, inkChecker.sharpenerMax);

        float refillPercent = Mathf.RoundToInt((sharpenerInk / inkChecker.inkMax) * 100);
        float sharpenerRemainingPercent = Mathf.RoundToInt((sharpenerInkMax / inkChecker.inkMax) * 100);

        inkChecker.sharpener = sharpenerInk;
        sharpenerInkMax = inkChecker.sharpenerMax;

        inkChecker.cost = refillPercent * 10;

        sharpenerHeader.text = "This sharpener has " + sharpenerRemainingPercent + "% of ink left";
        sharpenerText.text = "Refill " + refillPercent + "% of ink for";
        sharpenerCost.text = inkChecker.cost + " coins";
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        { sharpenerCanvas.SetActive(true); GameObject.Find("GameCanvas").GetComponent<CheckpointSystem>().currentCheckpoint = transform; }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        sharpenerCanvas.SetActive(false);
    }
}