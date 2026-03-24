using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Transform player;
    public Transform elevatorSwitch;
    public Transform downPos;
    public Transform upPos;

    public GameObject elevator;

    public float speed;
    bool isElevatorDown;
    bool hasCollided;

    void Update()
    {
        StartElevator();
        UpdateInteraction();
    }

    void StartElevator()
    {
        if (Vector2.Distance(player.position, elevatorSwitch.position)! > 0f && Input.GetKey("e") && hasCollided == true)
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
        if ((transform.position.y <= downPos.position.y || transform.position.y >= upPos.position.y) && hasCollided)
        {
            elevator.SetActive(true);
        }
        else
        {
            elevator.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") hasCollided = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        hasCollided = false;
    }
}
