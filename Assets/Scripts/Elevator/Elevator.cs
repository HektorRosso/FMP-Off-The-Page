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

    public float speed;
    [SerializeField] bool isElevatorDown;
    [HideInInspector] public bool inRange;

    private void Awake()
    {
        anim = elevator.GetComponent<Animator>();
    }

    void Update()
    {
        StartElevator();
        UpdateInteraction();
    }

    void StartElevator()
    {
        if (Vector2.Distance(player.position, elevatorSwitch.position)! > 0f && Input.GetKey("e") && inRange == true)
        {
            if (elevator.position.y <= downPos.position.y)
            {
                isElevatorDown = true;
            }
            else if (elevator.position.y >= upPos.position.y)
            {
                isElevatorDown = false;
            }
        }

        if (isElevatorDown)
        {
            elevator.position = Vector2.MoveTowards(elevator.position, upPos.position, speed * Time.deltaTime);
        }
        else
        {
            elevator.position = Vector2.MoveTowards(elevator.position, downPos.position, speed * Time.deltaTime);
        }
    }

    void UpdateInteraction()
    {
        bool isMoving = !(elevator.position.y <= downPos.position.y || elevator.position.y >= upPos.position.y);

        if (!isMoving)
        {
            anim.SetBool("isOn", false);

            if (inRange)
                elevatorUI.SetActive(true);
            else
                elevatorUI.SetActive(false);
        }
        else
        {
            anim.SetBool("isOn", true);
            elevatorUI.SetActive(false);
        }

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
