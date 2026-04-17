using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] protected float damage;
    private AudioSource audioSource;
    public AudioClip hit;
    public AudioClip playerHurt;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage);
            audioSource.PlayOneShot(hit);
            audioSource.PlayOneShot(playerHurt);
        }
    }
}
