using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbrirPuerta : MonoBehaviour {

	public bool abierta;
	public Sprite encendido, apagado;

	private Rigidbody2D rb2D;


	void Awake () {
		rb2D = this.GetComponent<Rigidbody2D> ();
		abierta = false;
	}

	public void abrir() {
		if (abierta) {
			GetComponent<Collider2D> ().enabled = false;
			GetComponent<SpriteRenderer> ().sprite = encendido;
			GetComponent<AudioSource> ().Play ();
		} else {
			GetComponent<Collider2D> ().enabled = true;
			GetComponent<SpriteRenderer> ().sprite = apagado;
			GetComponent<AudioSource> ().Play ();
		}
	}
}
