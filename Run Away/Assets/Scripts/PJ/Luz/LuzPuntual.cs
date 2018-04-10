using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuzPuntual : MonoBehaviour {

    Vector2 escalaInicial;

    private void Start()
    {
        escalaInicial = transform.localScale;
    }

    public void Activa(bool a)
    {
        gameObject.SetActive(a);
    }


    public void Radio(float porcentaje)
    {
        transform.localScale = escalaInicial * porcentaje;
    }
}
