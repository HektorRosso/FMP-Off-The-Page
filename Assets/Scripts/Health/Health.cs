using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    private bool invulnerable;

    CheckpointSystem checkpointSystem;
    private PlayerRespawn playerRespawn;

    private void Awake()
    {
        currentHealth = startingHealth;
        spriteRend = GetComponent<SpriteRenderer>();
        playerRespawn = GetComponent<PlayerRespawn>();
    }

    public void TakeDamage(float damage)
    {
        if (invulnerable) return;
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, 3);

        if (currentHealth <= 0)
        {
            if (!dead)
            {
                dead = true;

                foreach (Behaviour component in components)
                    component.enabled = false;
            }

            GameOver gameOver = FindFirstObjectByType<GameOver>();

            gameOver.Defeat();
        }

        StartCoroutine(Invulnerability());
        playerRespawn.Respawn();
    }

    public void AddHealth(float value)
    {
        currentHealth = Mathf.Clamp(currentHealth + value, 0, 3);
    }

    public void Respawn()
    {
        if (checkpointSystem.currentCheckpoint == null)
            return;

        dead = false;
        StartCoroutine(Invulnerability());
        transform.position = checkpointSystem.currentCheckpoint.position;
        foreach (Behaviour component in components)
            component.enabled = true;
    }

    private IEnumerator Invulnerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(3, 6, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / numberOfFlashes);
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / numberOfFlashes);
        }
        Physics2D.IgnoreLayerCollision(3, 6, false);
        invulnerable = false;
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
