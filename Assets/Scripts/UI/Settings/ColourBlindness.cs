using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ColourBlindness : MonoBehaviour
{
    [SerializeField] Volume volume;
    ColorAdjustments colorAdjustments;

    const string PREF_KEY = "ColorBlindMode";

    private void Start()
    {
        if (volume.profile.TryGet(out colorAdjustments))
        {
            colorAdjustments.hueShift.overrideState = true;
        }

        int mode = PlayerPrefs.GetInt(PREF_KEY, 0);

        switch (mode)
        {
            case 1: RedGreen(); break;
            case 2: BlueYellow(); break;
            case 3: Complete(); break;
            default: Off(); break;
        }
    }

    public void RedGreen()
    {
        if (volume.profile.TryGet<ColorAdjustments>(out colorAdjustments))
        {
            colorAdjustments.saturation.value = 0;
            colorAdjustments.hueShift.value = 64;
            colorAdjustments.hueShift.overrideState = true;

            PlayerPrefs.SetInt(PREF_KEY, 1);
        }
    }

    public void BlueYellow()
    {
        if (volume.profile.TryGet<ColorAdjustments>(out colorAdjustments))
        {
            colorAdjustments.saturation.value = 0;
            colorAdjustments.hueShift.value = -24;
            colorAdjustments.hueShift.overrideState = true;

            PlayerPrefs.SetInt(PREF_KEY, 2);
        }
    }

    public void Complete()
    {
        if (volume.profile.TryGet<ColorAdjustments>(out colorAdjustments))
        {
            colorAdjustments.saturation.value = -100;
            colorAdjustments.hueShift.value = 0;
            colorAdjustments.saturation.overrideState = true;

            PlayerPrefs.SetInt(PREF_KEY, 3);
        }
    }

    public void Off()
    {
        if (volume.profile.TryGet<ColorAdjustments>(out colorAdjustments))
        {
            colorAdjustments.saturation.value = 0;
            colorAdjustments.hueShift.value = 0;
            colorAdjustments.hueShift.overrideState = true;

            PlayerPrefs.SetInt(PREF_KEY, 0);
        }
    }
}