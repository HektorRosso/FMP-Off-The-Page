using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Main()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void City()
    {
        SceneManager.LoadScene("City");
    }
}
