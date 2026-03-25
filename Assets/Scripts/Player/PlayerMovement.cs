using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private SpriteRenderer player;
    private Rigidbody2D body;
    private bool grounded;
    private bool jumping;
    private float maxYVelocity;

    public AudioSource audioSource;
    public AudioClip jump;

    private Health health;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        body = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

        if (horizontalInput > 0.01f)
            player.flipX = false;
        else if (horizontalInput < -0.01f)
            player.flipX = true;

        if (Input.GetKey(KeyCode.Space) && grounded)
            Jump();

        if (!jumping && body.linearVelocity.y == 0)
        {
            jumping = true;

            if (maxYVelocity <= -10)
            {
                TakeFallDamage();
            }
        }
        else if (jumping)
        {
            if (body.linearVelocity.y < maxYVelocity)
            {
                maxYVelocity = body.linearVelocity.y;
            }
        }
    }

    private void Jump()
    {
        jumping = true;
        audioSource.PlayOneShot(jump);
        maxYVelocity = 0;
        body.linearVelocity = new Vector2(body.linearVelocity.x, speed);
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            grounded = true;
    }

    public void TakeFallDamage()
    {
        health.TakeDamage(1);
        Debug.Log("Fall damage");
    }
}
