using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderSonidos : MonoBehaviour {

    Slider slider;

    // Use this for initialization
    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    void OnEnable()
    {
        slider.value = SoundManager.instance.PorcentajeVolumenEfectos();
    }
}
