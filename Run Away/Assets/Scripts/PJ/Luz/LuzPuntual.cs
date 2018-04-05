using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuzPuntual : MonoBehaviour {

    Vector2 escalaInicial;

    private void Start()
    {
        escalaInicial = transform.localScale;
    }

    public void Apagar()
    {
        gameObject.SetActive(false);
    }

    public void Encender()
    {
        gameObject.SetActive(true);
    }

    public void Radio(float porcentaje)
    {
        transform.localScale = escalaInicial * porcentaje;
    }
}
