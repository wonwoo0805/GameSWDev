using UnityEngine;
using UnityEngine.UI;

public class GuageBar : MonoBehaviour
{
    public Slider sliderStat;

    public float maxGuage;
    public float currentGuage;
    public Image fillImage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void Initialize(float max, float current, Color color)
    {
        maxGuage = max;
        currentGuage = current;
        sliderStat.maxValue = max;
        sliderStat.value = current;
        fillImage.color = color;

    }

    // Update is called once per frame
    public void UpdateBar(float current)
    {
        currentGuage = current;
        sliderStat.value = current;
    }
}
