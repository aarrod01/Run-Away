using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Colores;

[RequireComponent(typeof(Animator))]

public class Puerta : MonoBehaviour 
{
	private Rigidbody2D rb2D;
    Animator puertaAnimacion;
    Colision[] colisiones;

    public Colores.Colores color;
	public bool abierta;
    public AudioSource sonido;
    public float intensidadDelSonido = 10f;

    void Awake()
    {
        puertaAnimacion = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        colisiones = GetComponentsInChildren<Colision>();
        for (int i = 0; i < colisiones.Length; i++)
            colisiones[i].Iniciar(abierta);
        puertaAnimacion.SetBool("Abierta", abierta);
        puertaAnimacion.SetInteger("Color", (int)color);

    }

    public void Abrir(Vector2 pos)
    {
        sonido.volume = intensidadDelSonido / (pos - (Vector2)transform.position).sqrMagnitude;
        sonido.Play();
        abierta = !abierta;
        for (int i = 0; i < colisiones.Length; i++)
            colisiones[i].Cambiar();
        puertaAnimacion.SetBool("Abierta", abierta);
    }
}
