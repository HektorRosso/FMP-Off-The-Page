using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject gameOverMusic;
    [SerializeField] private GameObject mainMusic;

    private void Start()
    {
        gameOver.SetActive(false);
    }

    public void Defeat()
    {
        PauseMenu pauseMenu = FindFirstObjectByType<PauseMenu>();

        pauseMenu.Pause();

        gameOver.SetActive(true);
        gameOverMusic.SetActive(true);
        mainMusic.SetActive(false);
    }
}
