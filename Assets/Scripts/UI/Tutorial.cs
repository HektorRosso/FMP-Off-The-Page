using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject ui;
    private bool hasRead = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !hasRead)
        {
            Time.timeScale = 0;
            ui.SetActive(true);
        }
    }

    public void Read()
    {
        hasRead = true;
    }
}
