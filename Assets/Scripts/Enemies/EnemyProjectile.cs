using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    [SerializeField] SpriteRenderer enemy;
    private float lifetime;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void ActivateProjectile()
    {
        lifetime = 0;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        float movementSpeed = speed * Time.deltaTime;
        if (enemy.flipX == false)
        {
            transform.Translate(-movementSpeed, 0, 0);
        }
        if (enemy.flipX == true)
        {
            transform.Translate(movementSpeed, 0, 0);
        }

        lifetime += Time.deltaTime;

        if (lifetime > resetTime) gameObject.SetActive(false);
    }

    private void TriggerEnter2D(Collider2D collision)
    {
        TriggerEnter2D(collision);
        gameObject.SetActive(false);
    }
}
