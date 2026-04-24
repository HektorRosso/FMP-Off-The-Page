using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float maxHeight = 10f;
    [SerializeField] private float followSpeed = 2f;
    [SerializeField] private float maxOffset = 5f; // how far it can move down

    private float startY;

    void Start()
    {
        startY = transform.position.y;
    }

    void Update()
    {
        float playerY = player.position.y;

        if (playerY > maxHeight)
        {
            // How far above the threshold the player is
            float excess = playerY - maxHeight;

            // Clamp so it doesn't go too far
            float offset = Mathf.Clamp(excess, 0f, maxOffset);

            float targetY = startY - offset;

            float newY = Mathf.Lerp(transform.position.y,targetY,followSpeed * Time.deltaTime);

            transform.position = new Vector2(transform.position.x, newY);
        }
        else
        {
            // Return to original position smoothly
            float newY = Mathf.Lerp(transform.position.y,startY,followSpeed * Time.deltaTime);

            transform.position = new Vector2(transform.position.x, newY);
        }
    }
}