using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Colision : MonoBehaviour {

    Collider2D[] cajaColision;
    public bool ActivoInicialmente;
	// Use this for initialization
	public void Iniciar (bool abierta) {
        cajaColision = GetComponents<Collider2D>();
        gameObject.SetActive(ActivoInicialmente^abierta);
    }
	
    public void Cambiar()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
