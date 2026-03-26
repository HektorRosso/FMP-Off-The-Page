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

    private void Awake()
    {
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
                collision.GetComponent<Health>().AddHealth(healthValue);
                gameObject.SetActive(false);
            }

            if (isInk)
            {
                checkpointSystem.ink += inkValue;
                gameObject.SetActive(false);
            }
        }
    }
}
