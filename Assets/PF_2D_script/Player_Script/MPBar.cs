using UnityEngine.UI;
using UnityEngine;

public class MPBar : MonoBehaviour
{
    public Slider slider;
    public Image MPFill;

    public void SetMaxMagic(int magic)
    {
        slider.maxValue = magic;
        slider.value = magic;

    }
    public void SetMagic(int magic)
    {
        slider.value = magic;
    }
}
