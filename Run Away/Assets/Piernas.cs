using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piernas : MonoBehaviour {

    Jugador jugador;
    Animator piernas;
	// Use this for initialization
	void Start () {

        jugador = GetComponentInParent<Jugador>();
        piernas = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {
        piernas.SetFloat("velocidad", jugador.Velocidad());
	}
}
