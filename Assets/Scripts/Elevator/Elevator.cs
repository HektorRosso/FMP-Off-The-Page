using UnityEngine;

public class Elevator : MonoBehaviour
{
    [Header("Stations")]
    public BoxCollider2D groundStation;
    public BoxCollider2D upperStation;

    [Header("Transforms")]
    public Transform player;
    public Transform elevator;
    public Transform elevatorSwitch;
    public Transform downPos;
    public Transform upPos;

    [Header("Game Objects")]
    public GameObject elevatorUI;
    public GameObject groundOffVisual;
    public GameObject groundOnVisual;
    public GameObject upperOffVisual;
    public GameObject upperOnVisual;

    private Animator anim;

    [Header("Audio")]
    private AudioSource audioSource;
    public AudioClip buttonPress;
    public AudioClip elevatorMoving;

    [Header("Settings")]
    public float speed = 2f;

    [SerializeField] private bool moveToUpper;
    [HideInInspector] public bool inRange;

    private bool isMovingLastFrame;

    private void Awake()
    {
        anim = elevator.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        HandleInput();
        MoveElevator();
        UpdateInteraction();
    }

    void HandleInput()
    {
        bool isMoving = IsMoving();

        if (Input.GetKeyDown(KeyCode.E) && inRange && !isMoving)
        {
            audioSource.PlayOneShot(buttonPress);
            moveToUpper = !moveToUpper;
        }
    }

    void MoveElevator()
    {
        Vector2 target = moveToUpper ? upPos.position : downPos.position;

        if (Vector2.Distance(elevator.position, target) > 0.01f)
        {
            elevator.position = Vector2.MoveTowards(
                elevator.position,
                target,
                speed * Time.deltaTime
            );
        }
    }

    void UpdateInteraction()
    {
        bool isMoving = IsMoving();

        // Animator
        anim.SetBool("isOn", isMoving);

        // UI
        elevatorUI.SetActive(!isMoving && inRange);

        // Visuals
        if (!isMoving)
        {
            groundOnVisual.SetActive(false);
            upperOnVisual.SetActive(false);

            groundOffVisual.SetActive(true);
            upperOffVisual.SetActive(true);
        }
        else
        {
            groundOnVisual.SetActive(true);
            upperOnVisual.SetActive(true);

            groundOffVisual.SetActive(false);
            upperOffVisual.SetActive(false);
        }

        // Start moving sound
        if (isMoving && !isMovingLastFrame)
        {
            audioSource.clip = elevatorMoving;
            audioSource.loop = true;
            audioSource.Play();
        }

        // Stop moving sound ONLY if it's the movement clip
        if (!isMoving && isMovingLastFrame)
        {
            if (audioSource.clip == elevatorMoving)
            {
                audioSource.Stop();
            }
        }

        isMovingLastFrame = isMoving;
    }

    bool IsMoving()
    {
        Vector2 target = moveToUpper ? upPos.position : downPos.position;
        return Vector2.Distance(elevator.position, target) > 0.01f;
    }

    public void SetInRange(bool value)
    {
        inRange = value;
    }
}