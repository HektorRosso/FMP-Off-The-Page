using UnityEngine;

public class ElevatorPlayer : MonoBehaviour
{
    private Elevator elevatorController;

    private void Awake()
    {
        elevatorController = GetComponentInParent<Elevator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}