using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;



public class SliderSonidoGlobal : MonoBehaviour
{

    Slider slider;

    // Use this for initialization
    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    void OnEnable()
    {
        slider.value = SoundManager.instance.PorcentajeVolumenGlobal();
    }
}
