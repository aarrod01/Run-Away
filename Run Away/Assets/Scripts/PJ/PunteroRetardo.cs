using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PunteroRetardo : MonoBehaviour {

    public static PunteroRetardo instance = null;

    Interactuable objetoInteractuable = null;
    Jugador jugador;

    Rigidbody2D puntero;
	Animator punteroAnimacion;

    public float lerp = 0.5f;

	void Start () {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(this.gameObject);
        jugador = GameObject.FindObjectOfType<Jugador>();
        puntero = GetComponent<Rigidbody2D>();
		punteroAnimacion = GetComponent<Animator> ();
	}

	void Update () 
	{
        puntero.MovePosition(Vector2.Lerp(puntero.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), lerp));
        if (objetoInteractuable != null && objetoInteractuable.EsPosibleLaInteraccion(jugador))
        {
            
            if (Input.GetMouseButtonDown(0))
                objetoInteractuable.Accion(jugador);
        }
            
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Interactuable aux = collision.gameObject.GetComponent<Interactuable>();

		if (aux != null) 
		{
            punteroAnimacion.SetTrigger("Interactuar");
            objetoInteractuable = aux;
		}
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
		if (objetoInteractuable != null && objetoInteractuable.gameObject == collision.gameObject) 
		{
            punteroAnimacion.SetTrigger("Estatico");
            objetoInteractuable = null;
		}
    }
}
