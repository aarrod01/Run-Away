using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class GeneradoOndas : MonoBehaviour {

    Animator ondas;
    int numeroOndas;

	// Use this for initialization
	void Start () {
        ondas = GetComponent<Animator>();
        numeroOndas = 0;
	}

    public void SumarLuz()
    {
        numeroOndas++;
    }
    public void RestarLuz()
    {
        numeroOndas--;
        if(numeroOndas<=0)
            ondas.SetTrigger("Parar");
    }

    public void GenerarOndas()
    {
        if (numeroOndas > 0)
            ondas.SetTrigger("Generar");
    }
    public void PararOndas()
    {
        ondas.SetTrigger("Parar");
    }
}
