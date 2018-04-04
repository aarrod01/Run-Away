using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour
{


    LuzConica luzConica;
    LuzPuntual luzPuntual;

    void Start()
    {
        luzConica = GetComponentInChildren<LuzConica>();
        luzPuntual = GetComponentInChildren<LuzPuntual>();
    }
    public void ApagarLuzConica()
    {
        luzConica.Apagar();
    }
    public void EncenderLuzConica()
    {
        luzConica.Encender();
    }
    public void IntensidadLuz(float porcentaje)
    {

    }

}
