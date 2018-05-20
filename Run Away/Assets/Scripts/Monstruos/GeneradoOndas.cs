using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class GeneradoOndas : MonoBehaviour {

    Animator ondas;
    int numeroLuces;

	// Use this for initialization
	void Start () {
        ondas = GetComponent<Animator>();
        numeroLuces = 0;
	}

    public void Entrar(GameObject caster)
    {
        ondas.enabled = false;
    }

    public void Salir(GameObject caster)
    {
        ondas.enabled = true;
    }

    public void SumarLuz()
    {
        numeroLuces++;
        if(numeroLuces>0)
            ondas.SetTrigger("Parar");
    }
    public void RestarLuz()
    {
        numeroLuces--;
        if(numeroLuces<=0)
            ondas.SetTrigger("Generar");
    }

    public void GenerarOndas()
    {
        if (numeroLuces <= 0)
            ondas.SetTrigger("Generar");
    }
    public void PararOndas()
    {
            ondas.SetTrigger("Parar");
    }
}
