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
    public TMP_Text leadText;
    public Transform leadBar;
    public float leadbarWidth;
    public TMP_Text funds;

    [Header("Audio")]
    private AudioSource audioSource;
    public AudioClip sharpen;

    [HideInInspector] public Transform currentCheckpoint;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        UpdateLead();
        UpdateFunds();
    }

    void Update()
    {
        if (ink != lastInk)
            UpdateLead();
    }

    void SetLead()
    {
        float newWidth = (ink / inkMax) * leadbarWidth;
        leadBar.localScale = new Vector3(newWidth, 1f, 1f);
    }

    void UpdateLead()
    {
        float normalized = ink / inkMax;
        float percent = Mathf.RoundToInt(normalized * 100);

        leadText.text = percent + "%";

        if (normalized > 0.5f)
        {
            leadText.color = Color.Lerp(Color.yellow, Color.green, (normalized - 0.5f) * 2f);
        }
        else if (normalized > 0.25f)
        {
            leadText.color = Color.Lerp(Color.red, Color.yellow, (normalized - 0.25f) * 4f);
        }
        else
        {
            leadText.color = Color.Lerp(Color.black, Color.red, normalized * 4f);
        }

        lastInk = ink;
        SetLead();
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

        audioSource.PlayOneShot(sharpen);

        UpdateFunds();
    }
}