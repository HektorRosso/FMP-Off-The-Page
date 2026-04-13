using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Transform player;
    public Transform elevator;
    public Transform elevatorSwitch;
    public Transform downPos;
    public Transform upPos;

    public GameObject elevatorUI;
    private Animator anim;

    public float speed;
    bool isElevatorDown;
    bool inRange;

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
        if ((elevator.position.y <= downPos.position.y || elevator.position.y >= upPos.position.y))
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") inRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inRange = false;
    }
}
