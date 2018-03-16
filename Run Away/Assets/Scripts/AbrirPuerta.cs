using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbrirPuerta : MonoBehaviour {

	public bool abrirIzquierda;
	public bool interactuado;

	private Rigidbody2D rb2D;

	// Use this for initialization
	void Start () {
		abrirIzquierda = true;
		interactuado = false;

		rb2D = this.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (interactuado) {
			if (abrirIzquierda) {
				rb2D.MoveRotation (rb2D.rotation + 90);
			} else {
				rb2D.MoveRotation (rb2D.rotation - 90);
			}
			this.GetComponentInChildren<AbrirPuerta>().enabled = false; 
		}
	}
}
