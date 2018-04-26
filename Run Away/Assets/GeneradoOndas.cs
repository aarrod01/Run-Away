using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class GeneradoOndas : MonoBehaviour {

    Animator ondas;

	// Use this for initialization
	void Start () {
        ondas = GetComponent<Animator>();
	}

    public void GenerarOndas()
    {
        ondas.SetTrigger("Generar");
    }
    public void PararOndas()
    {
        ondas.SetTrigger("Parar");
    }
}
