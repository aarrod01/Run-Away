using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactuable))]

public class Ejemplo : MonoBehaviour {
    Interactuable master;
	// Use this for initialization
	void Start () {
        master = GetComponent<Interactuable>();
        master.Accion= (Jugador a) => { /*Cosa que hace al interactuar*/ };
	}
	
}
