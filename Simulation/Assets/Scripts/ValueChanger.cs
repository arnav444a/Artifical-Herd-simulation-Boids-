using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ValueChanger : MonoBehaviour
{
    private float startTime;
    public TextMeshProUGUI text;
    public Slider slider;

    void Update()
    {
        if (Time.time - startTime > 0.1)
        {
            startTime = Time.time;
            text.text = System.Math.Round(slider.value, 1).ToString();
        }
    }
}
