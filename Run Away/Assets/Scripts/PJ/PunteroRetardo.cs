using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PunteroRetardo : MonoBehaviour {

    public static PunteroRetardo instance = null;

    Interactuable objetoInteractuable = null;
    Jugador jugador;
    bool muerto;
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
        instance.Iniciar();
	}

    void Iniciar()
    {
        jugador = GameObject.FindObjectOfType<Jugador>();
        puntero = GetComponent<Rigidbody2D>();
        punteroAnimacion = GetComponent<Animator>();
        muerto = false;
    }

	void Update () 
	{
        puntero.MovePosition(Vector2.Lerp(puntero.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), lerp));
        if (!muerto)
        {
            if (objetoInteractuable != null && objetoInteractuable.EsPosibleLaInteraccion(jugador))
            {
                punteroAnimacion.SetTrigger("Interactuar");
                if (Input.GetMouseButtonDown(0))
                    objetoInteractuable.Accion(jugador);
            }
            else
                punteroAnimacion.SetTrigger("Estatico");
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
    public void Muerto(bool m)
    {
        muerto = m;
    }
}
