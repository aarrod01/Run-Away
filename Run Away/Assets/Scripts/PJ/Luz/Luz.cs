using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luz : MonoBehaviour
{


    LuzConica luzConica;
    LuzPuntual luzPuntual;

    void Start()
    {
        luzConica = GetComponentInChildren<LuzConica>();
        luzPuntual = GetComponentInChildren<LuzPuntual>();
    }
    public void LuzConica(bool a)
    {
        luzConica.Activa(a);
    }
    
    public void IntensidadLuz(float porcentaje)
    {
        luzConica.Largo(porcentaje);
        luzPuntual.Radio(porcentaje);
    }

}
