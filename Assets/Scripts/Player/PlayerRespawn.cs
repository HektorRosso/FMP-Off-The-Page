using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound;
    private Transform currentCheckpoint;
    private Health playerHealth;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void Respawn()
    {
        if (currentCheckpoint == null)
        {
            GameOver gameOver = FindFirstObjectByType<GameOver>();

            gameOver.Defeat();
        }

        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        rb.Sleep();

        rb.WakeUp();

        transform.position = currentCheckpoint.position;

        playerHealth.StartCoroutine("Invulnerability");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform;
        }
    }
}
