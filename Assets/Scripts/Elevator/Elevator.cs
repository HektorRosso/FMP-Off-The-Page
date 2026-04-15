using UnityEngine;
using System.Collections;

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

    [Header("Settings")]
    public float speed = 2f;

    [SerializeField] private bool moveToUpper;
    [HideInInspector] public bool inRange;

    private void Awake()
    {
        anim = elevator.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        StartElevator();
        UpdateInteraction();
    }

    void StartElevator()
    {
        // Trigger once per key press
        if (Input.GetKeyDown(KeyCode.E) && inRange)
        {
            audioSource.PlayOneShot(buttonPress);

            // Toggle direction
            moveToUpper = !moveToUpper;
        }

        // Decide target
        Vector2 target = moveToUpper ? upPos.position : downPos.position;

        // Move elevator
        elevator.position = Vector2.MoveTowards(
            elevator.position,
            target,
            speed * Time.deltaTime
        );
    }

    void UpdateInteraction()
    {
        bool isAtBottom = Vector2.Distance(elevator.position, downPos.position) < 0.01f;
        bool isAtTop = Vector2.Distance(elevator.position, upPos.position) < 0.01f;

        bool isMoving = !(isAtBottom || isAtTop);

        // Animator + UI
        anim.SetBool("isOn", isMoving);

        if (!isMoving && inRange)
            elevatorUI.SetActive(true);
        else
            elevatorUI.SetActive(false);

        // Visual indicators
        if (!isMoving)
        {
            groundOffVisual.SetActive(true);
            groundOnVisual.SetActive(false);

            upperOffVisual.SetActive(true);
            upperOnVisual.SetActive(false);
        }
        else
        {
            groundOffVisual.SetActive(false);
            groundOnVisual.SetActive(true);

            upperOffVisual.SetActive(false);
            upperOnVisual.SetActive(true);
        }
    }

    public void SetInRange(bool value)
    {
        inRange = value;
    }
}