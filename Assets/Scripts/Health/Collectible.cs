using UnityEngine;

public class Collectible : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float healthValue;
    private bool isHealth;

    [Header("Ink")]
    [SerializeField] private float inkValue;
    private bool isInk;
    CheckpointSystem checkpointSystem;

    [Header("Audio")]
    private AudioSource audioSource;
    public AudioClip collectible;

    private void Awake()
    {
        audioSource = GetComponentInParent<AudioSource>();
        checkpointSystem = GameObject.Find("CheckpointSystem").GetComponent<CheckpointSystem>();
    }

    private void Start()
    {
        isHealth = healthValue > 0f;
        isInk = inkValue > 0f;

        if (isInk) transform.localScale *= inkValue;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (isHealth)
            {
                audioSource.PlayOneShot(collectible);
                collision.GetComponent<Health>().AddHealth(healthValue);
                gameObject.SetActive(false);
            }

            if (isInk)
            {
                audioSource.PlayOneShot(collectible);
                checkpointSystem.ink += inkValue;
                gameObject.SetActive(false);
            }
        }
    }
}
