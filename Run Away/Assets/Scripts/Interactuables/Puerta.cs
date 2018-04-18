using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Colores;

[RequireComponent(typeof(Animator))]

public class Puerta : MonoBehaviour 
{
	private Rigidbody2D rb2D;

    GameObject cajaDeColision;
    Animator puertaAnimacion;

    public Colores.Colores color;
	public bool abierta;

	void Awake () 
	{
        puertaAnimacion = GetComponent<Animator>();
		rb2D = GetComponent<Rigidbody2D> ();
        cajaDeColision = transform.GetChild(0).gameObject;

        if (abierta)
        {
            puertaAnimacion.SetBool("Abierta", true);
            GetComponent<Collider2D>().enabled = false;
            cajaDeColision.SetActive(false);
        }
        else
        {
            puertaAnimacion.SetBool("Abierta", false);
            GetComponent<Collider2D>().enabled = true;
            cajaDeColision.SetActive(true);
        }
    }

	public void abrir() 
	{
        abierta = !abierta;
		if (abierta) 
		{
            puertaAnimacion.SetBool("Abierta", true);
            GetComponent<Collider2D> ().enabled = false;
            cajaDeColision.SetActive(false);
            GetComponent<AudioSource>().Play();
        } 
		else 
		{
            puertaAnimacion.SetBool("Abierta", false);
            GetComponent<Collider2D> ().enabled = true;
            cajaDeColision.SetActive(true);
            GetComponent<AudioSource>().Play();
        }
			
	}
}
