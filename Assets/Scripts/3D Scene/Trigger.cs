using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField] private GameObject interactionText;
    [SerializeField] private KeyCode interactKey = KeyCode.E;

    private bool playerInRange = false;

    void Start()
    {
        if (interactionText != null)
            interactionText.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactKey))
        {
            Interact();
        }
    }

    private void Interact()
    {
        Debug.Log("Interacted!");
        // Put your interaction logic here
        // e.g. open door, pick up item, show dialogue, etc.
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            if (interactionText != null)
                interactionText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            if (interactionText != null)
                interactionText.SetActive(false);
        }
    }
}
