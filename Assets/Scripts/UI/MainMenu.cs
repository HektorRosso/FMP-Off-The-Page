using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Main()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void LevelSelect3D()
    {
        SceneManager.LoadScene("Level Select 3D");
    }

    public void CityOfNylorx()
    {
        SceneManager.LoadScene("City Of Nylorx");
    }
}
