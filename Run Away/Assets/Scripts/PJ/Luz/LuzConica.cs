using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuzConica : MonoBehaviour {

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

    public void Largo(float porcentaje)
    {
        transform.localScale=escalaInicial * porcentaje;
    }
}
