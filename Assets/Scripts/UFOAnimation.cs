using System.Collections;
using UnityEngine;

public class UFOAnimation : MonoBehaviour
{
    private Animator ufoAnim;
    private PlayerMovement playerMovement2D;

    [SerializeField] private Transform playerMove;
    [SerializeField] private Transform visualScale;
    [SerializeField] private Transform abductPoint;
    [SerializeField] private GameObject ufoCamera;

    [Header("Demo Screen")]
    [SerializeField] private GameObject demoScreen;

    private void Awake()
    {
        playerMovement2D = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        ufoAnim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerMovement2D.enabled = false;
            StartCoroutine(StartAbduction());
        }
    }

    public IEnumerator StartAbduction()
    {
        ufoAnim.SetInteger("currentFrame", 1);
        yield return null;
    }

    IEnumerator Abduction()
    {
        ufoAnim.SetInteger("currentFrame", 2);

        Vector3 startPos = playerMove.position;
        Vector3 startScale = visualScale.localScale;

        float duration = 1f;
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            float t = timer / duration;

            playerMove.position = Vector3.Lerp(startPos, abductPoint.position, t);

            visualScale.localScale = Vector3.Lerp(startScale, Vector3.zero, t);

            yield return null;
        }

        ufoCamera.SetActive(true);

        ufoAnim.SetInteger("currentFrame", 3);

        yield return null;
    }

    public void FlyAway()
    {
        ufoAnim.SetInteger("currentFrame", 4);
    }

    public void Demo()
    {
        Time.timeScale = 0f;
        gameObject.SetActive(false);
        demoScreen.SetActive(true);
    }
}