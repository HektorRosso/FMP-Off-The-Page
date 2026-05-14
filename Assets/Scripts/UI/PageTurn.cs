using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void Freeze()
    {
        Time.timeScale = 0f;
    }

    public void Unfreeze()
    {
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void CityOfNylorx()
    {
        SceneManager.LoadScene("City Of Nylorx");
    }
}
