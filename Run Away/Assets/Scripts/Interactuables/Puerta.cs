using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Colores;

[RequireComponent(typeof(Animator))]

public class Puerta : MonoBehaviour 
{
	private Rigidbody2D rb2D;
    
    Animator puertaAnimacion;

    public Colores.Colores color;
	public bool abierta;

    void Awake()
    {
        puertaAnimacion = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        
        puertaAnimacion.SetBool("Abierta", abierta);
        puertaAnimacion.SetInteger("Color", (int)color);
        GetComponent<Collider2D>().enabled = !abierta;

    }

    public void abrir()
    {
        abierta = !abierta;

        puertaAnimacion.SetBool("Abierta", abierta);
        GetComponent<Collider2D>().enabled = !abierta;
        GetComponent<AudioSource>().Play();
    }
}
