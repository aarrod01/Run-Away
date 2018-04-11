﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Colores;

public class Puerta : MonoBehaviour 
{
	private Rigidbody2D rb2D;
    GameObject cajaDeColision;

    public Colores.Colores color;
	public bool abierta;
	public Sprite encendido, apagado;

	void Awake () 
	{
		rb2D = this.GetComponent<Rigidbody2D> ();
        cajaDeColision = transform.GetChild(0).gameObject;
        if (abierta)
        {
            GetComponent<Collider2D>().enabled = false;
            cajaDeColision.SetActive(false);
            GetComponent<SpriteRenderer>().sprite = apagado;
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
        else
        {
            GetComponent<Collider2D>().enabled = true;
            cajaDeColision.SetActive(true);
            GetComponent<SpriteRenderer>().sprite = encendido;
            gameObject.layer = LayerMask.NameToLayer("LightObstacles");
        }
    }

	public void abrir() 
	{
        abierta = !abierta;
		if (abierta) 
		{
			GetComponent<Collider2D> ().enabled = false;
            cajaDeColision.SetActive(false);
            GetComponent<SpriteRenderer> ().sprite = apagado;
            gameObject.layer = LayerMask.NameToLayer("Default");
            GetComponent<AudioSource>().Play();
        } 
		else 
		{
			GetComponent<Collider2D> ().enabled = true;
            cajaDeColision.SetActive(true);
            GetComponent<SpriteRenderer> ().sprite = encendido;
            gameObject.layer = LayerMask.NameToLayer("LightObstacles");
            GetComponent<AudioSource>().Play();
        }
			
	}
}