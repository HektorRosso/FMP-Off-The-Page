using UnityEngine;

public class ElevatorTrigger : MonoBehaviour
{
    private Elevator elevator;

    private void Start()
    {
        elevator = GetComponentInParent<Elevator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            elevator.SetInRange(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            elevator.SetInRange(false);
        }
    }
}
