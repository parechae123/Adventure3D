using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    Slider slider;
    void Start()
    {
        if (slider == null) slider = GetComponent<Slider>();

    }
    public void OnValueChange(float val)
    {
        slider.value = val;
    }
}
