using UnityEngine;

public class PlayRandomVictory : MonoBehaviour
{
    private AudioSource victoryAudioSource;

    public AudioClip[] victoryClips;

    private void Awake()
    {
        victoryAudioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        int randomIndex = Random.Range(0, victoryClips.Length);
        victoryAudioSource.PlayOneShot(victoryClips[randomIndex]);
    }
}