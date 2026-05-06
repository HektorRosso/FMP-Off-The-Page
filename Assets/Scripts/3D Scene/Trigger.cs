using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField] private GameObject interactionText;
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    [SerializeField] private GameObject[] gameObjects;

    public GameObject levelSelect;

    private bool playerInRange = false;

    void Start()
    {
        if (interactionText != null)
            interactionText.SetActive(false);

        levelSelect.SetActive(false);
        LockCursor();
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactKey))
        {
            Interact();
        }
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Interact()
    {
        foreach (GameObject obj in gameObjects)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }

        Time.timeScale = 0f;
        Debug.Log("Interacted!");
        levelSelect.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
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

    private void OnTriggerEnter2D(Collider2D other)
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

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            if (interactionText != null)
                interactionText.SetActive(false);
        }
    }
}
