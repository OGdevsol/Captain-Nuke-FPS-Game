using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class healthUIScript : MonoBehaviour
{
    public static healthUIScript instance;
    public Slider healthUISlider;
    public TMP_Text healthDigit;
    

    private void Awake()
    {
        instance = this;
    }

    
}
