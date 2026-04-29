using UnityEngine;

public class PageTurn : MonoBehaviour
{
    public GameObject targetObject;

    public void ShowObject()
    {
        if (targetObject != null)
            targetObject.SetActive(true);
    }

    public void HideObject()
    {
        if (targetObject != null)
            targetObject.SetActive(false);
    }

    public void HideSelf()
    {
        gameObject.SetActive(false);
    }
}
