using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPitchShift : MonoBehaviour
{
    [SerializeField] private float minPitch = 0.5f;
    [SerializeField] private float maxPitch = 1f;
    [SerializeField] private float smoothSpeed = 3f;

    private AudioSource audioSource;
    private CheckpointSystem checkpointSystem;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        GameObject checkpointObj = GameObject.Find("CheckpointSystem");

        if (checkpointObj != null)
        {
            checkpointSystem = checkpointObj.GetComponent<CheckpointSystem>();
        }
        else
        {
            Debug.LogError("CheckpointSystem object not found!");
        }
    }

    private void Update()
    {
        if (checkpointSystem == null) return;

        UpdatePitchShift();
    }

    private void UpdatePitchShift()
    {
        float normalizedInk = checkpointSystem.ink / checkpointSystem.inkMax;

        float targetPitch;

        if (normalizedInk >= 0.5f)
        {
            targetPitch = 1f;
        }
        else
        {
            float t = Mathf.InverseLerp(0f, 0.5f, normalizedInk);
            targetPitch = Mathf.Lerp(minPitch, maxPitch, t);
        }

        audioSource.pitch = Mathf.Lerp(audioSource.pitch,targetPitch,Time.deltaTime * smoothSpeed);
    }
}