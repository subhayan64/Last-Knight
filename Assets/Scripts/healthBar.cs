﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    public Slider slider;
    //public Gradient gradient;
    //public Image fill;

    public void maxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
        //fill.color = gradient.Evaluate(1f);
    }
    public void setHealthValue(float health)
    {
        slider.value = health;
        //fill.color = gradient.Evaluate(slider.normalizedValue);
    }
   
}
