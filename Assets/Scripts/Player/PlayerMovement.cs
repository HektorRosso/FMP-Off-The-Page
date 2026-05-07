using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 7.5f;
    [SerializeField] private SpriteRenderer player;
    private Animator anim;

    private Rigidbody2D body;
    private float inputX;

    private bool grounded;
    private bool jumping;
    private float maxYVelocity;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip jump;
    public AudioClip playerHurt;
    public AudioClip impact;

    private Health health;
    private PlayerRespawn playerRespawn;

    private bool fallDamage;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        body = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        health = GetComponent<Health>();
        playerRespawn = GetComponent<PlayerRespawn>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        body.linearVelocity = new Vector2(inputX * speed, body.linearVelocity.y);

        if (inputX > 0.01f)
            player.flipX = true;
        else if (inputX < -0.01f)
            player.flipX = false;

        if (Input.GetKey(KeyCode.Space) && grounded)
            Jump();

        if (!jumping && Mathf.Abs(body.linearVelocity.y) < 0.01f)
        {
            jumping = true;
        }
        else if (jumping)
        {
            if (body.linearVelocity.y < maxYVelocity)
                maxYVelocity = body.linearVelocity.y;

            if (maxYVelocity <= -15f)
                fallDamage = true;
        }

        anim.SetBool("jumping", !grounded);
        anim.SetBool("running", Mathf.Abs(inputX) > 0.1f);
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
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;

            if (fallDamage)
                TakeFallDamage();
        }
    }

    public void TakeFallDamage()
    {
        audioSource.PlayOneShot(impact);
        audioSource.PlayOneShot(playerHurt);
        health.TakeDamage(1);
        playerRespawn.Respawn();
        fallDamage = false;
        maxYVelocity = 0;
    }
}