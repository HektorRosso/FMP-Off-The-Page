using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonInteract : MonoBehaviour
{
    public Vector3 normalScale = Vector3.one;
    public Vector3 hoverScale = new Vector3(1.2f, 1.2f, 1f);
    public float smoothTime = 0.2f;

    private Coroutine scaleCoroutine;

    public void OnPointerEnter(PointerEventData eventData)
    {
        StartScaleCoroutine(hoverScale);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StartScaleCoroutine(normalScale);
    }

    private void StartScaleCoroutine(Vector3 targetScale)
    {
        if (scaleCoroutine != null)
            StopCoroutine(scaleCoroutine);
        scaleCoroutine = StartCoroutine(SmoothScale(targetScale));
    }

    private IEnumerator SmoothScale(Vector3 targetScale)
    {
        Vector3 currentScale = transform.localScale;
        float timer = 0f;

        while (timer < smoothTime)
        {
            transform.localScale = Vector3.Lerp(currentScale, targetScale, timer / smoothTime);
            timer += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale; // Ensure exact target at the end
    }
}
