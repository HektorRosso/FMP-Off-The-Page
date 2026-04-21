using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject gameOver;

    private void Start()
    {
        gameOver.SetActive(false);
    }

    public void Defeat()
    {
        PauseMenu pauseMenu = FindFirstObjectByType<PauseMenu>();

        pauseMenu.Pause();

        gameOver.SetActive(true);
    }
}
