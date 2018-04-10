using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PunteroRetardo : MonoBehaviour {

    Interactuable objetoInteractuable = null, objetoInteractuable2 = null;
    Jugador jugador;
    Rigidbody2D puntero;
    public float lerp = 0.5f;

	// Use this for initialization
	void Start () {
        jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Jugador>();
        puntero = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        puntero.MovePosition(Vector2.Lerp(puntero.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), lerp));
        if (Input.GetMouseButtonDown(0) && objetoInteractuable != null && objetoInteractuable.EsPosibleLaInteraccion(jugador))
        {
            objetoInteractuable.Accion(jugador);
        }

	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Interactuable aux = collision.gameObject.GetComponent<Interactuable>();
        if (aux != null)
            objetoInteractuable = aux;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (objetoInteractuable != null && objetoInteractuable.gameObject == collision.gameObject)
            objetoInteractuable = null;
    }
}
