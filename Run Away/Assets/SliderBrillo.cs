using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.PostProcessing;

public class SliderBrillo : MonoBehaviour
{

    Slider slider;
    public PostProcessingProfile perfil;

    // Use this for initialization
    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    void OnEnable()
    {
        slider.value = perfil.colorGrading.settings.basic.postExposure;
    }
}