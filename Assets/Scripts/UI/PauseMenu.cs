using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private void Start()
    {
        Pause();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
    }
}
