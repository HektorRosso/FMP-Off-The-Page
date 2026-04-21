using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    [SerializeField] SpriteRenderer enemy;
    private float lifetime;

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

        if (lifetime > resetTime) Destroy(gameObject);
    }

    private void TriggerEnter2D(Collider2D collision)
    {
        TriggerEnter2D(collision);
        gameObject.SetActive(false);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            DrawingErasing drawer = FindFirstObjectByType<DrawingErasing>();

            if (drawer != null)
            {
                Vector3 hitPoint = other.ClosestPoint(transform.position);

                float scale = transform.localScale.x;
                float radius = scale * drawer.minBrushSize;

                drawer.EraseAtPoint(hitPoint, radius);

                Destroy(gameObject);
            }
        }
    }
}
