using UnityEngine;
using UnityEngine.Events;

public class Toggle : MonoBehaviour
{
    [SerializeField] private UnityEvent toggleOn;
    [SerializeField] private UnityEvent toggleOff;

    [SerializeField] private bool isOn;
    [SerializeField] private GameObject checkmark;

    public bool IsOn => isOn;

    private void Update()
    {
        UpdateVisual();
    }

    public void SetState(bool state)
    {
        if (isOn == state) return;

        isOn = state;

        UpdateVisual();

        if (isOn)
            toggleOn?.Invoke();
        else
            toggleOff?.Invoke();
    }

    public void ToggleState()
    {
        SetState(!isOn);
    }

    public void TurnOn()
    {
        SetState(true);
    }

    public void TurnOff()
    {
        SetState(false);
    }

    private void UpdateVisual()
    {
        if (checkmark != null)
            checkmark.SetActive(isOn);
    }
}