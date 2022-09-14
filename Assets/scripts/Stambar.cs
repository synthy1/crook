using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stambar : MonoBehaviour
{
    public Slider slider;

    public void setstam(float stamina)
    {
        slider.value = stamina;
    }
        
    public void setmaxstam(float stamina)
    {
        slider.maxValue = stamina;
        slider.value = stamina;
    }
        
}
