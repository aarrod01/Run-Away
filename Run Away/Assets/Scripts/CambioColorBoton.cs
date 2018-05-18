using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

public class CambioColorBoton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    Text texto;
    Color colorBase;
    public Color ColorAlEntrar;

    void Awake()
    {
        texto = GetComponentInChildren<Text>();
        colorBase = texto.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        texto.color = ColorAlEntrar;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        texto.color = colorBase;
    }
    
    void OnEnable()
    {
        texto.color = colorBase;
    }


}
