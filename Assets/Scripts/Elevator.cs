using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Transform player;
    public Transform elevatorSwitch;
    public Transform downPos;
    public Transform upPos;

    public GameObject elevator;
    private Animator anim;

    public float speed;
    bool isElevatorDown;
    bool inRange;

    private void Awake()
    {
        anim = GetComponent<Animator>();
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
            if (transform.position.y <= downPos.position.y)
            {
                isElevatorDown = true;
            }
            else if (transform.position.y >= upPos.position.y)
            {
                isElevatorDown = false;
            }
        }

        if (isElevatorDown)
        {
            transform.position = Vector2.MoveTowards(transform.position, upPos.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, downPos.position, speed * Time.deltaTime);
        }
    }

    void UpdateInteraction()
    {
        if ((transform.position.y <= downPos.position.y || transform.position.y >= upPos.position.y))
        {
            anim.SetBool("isOn", false);

            if (inRange)
                elevator.SetActive(true);
            else
                elevator.SetActive(false);
        }
        else
        {
            anim.SetBool("isOn", true);
            elevator.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") inRange = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        inRange = false;
    }
}
