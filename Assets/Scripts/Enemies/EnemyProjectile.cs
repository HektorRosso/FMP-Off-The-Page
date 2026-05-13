using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    [SerializeField] Transform enemy;
    private SpriteRenderer spriteRenderer;
    private float lifetime;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ActivateProjectile()
    {
        lifetime = 0;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        float movementSpeed = speed * Time.deltaTime;
        if (enemy.localScale.x == 1f)
        {
            spriteRenderer.flipX = false;
            transform.Translate(-movementSpeed, 0, 0);
        }
        else
        {
            spriteRenderer.flipX = true;
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
        if (other.CompareTag("DrawnGround"))
        {
            DrawingErasing drawer = FindFirstObjectByType<DrawingErasing>();

            if (drawer != null)
            {
                Vector3 hitPoint = other.ClosestPoint(transform.position);

                float scale = transform.localScale.x;
                float radius = scale * drawer.maxBrushSize;

                drawer.EraseAtPoint(hitPoint, radius);

                Destroy(gameObject);
            }
        }
    }
}
