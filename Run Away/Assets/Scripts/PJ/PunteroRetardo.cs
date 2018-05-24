using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PunteroRetardo : MonoBehaviour {

    public static PunteroRetardo instance = null;

    List<Interactuable> objetoInteractuable ;
    Jugador jugador;
    bool muerto;
    Rigidbody2D puntero;
	Animator punteroAnimacion;

    public float lerp = 0.5f;

	void Awake () {
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
        objetoInteractuable = new List<Interactuable>();
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
            bool encima = false;
            foreach(Interactuable item in objetoInteractuable)
            {
                if(item.EsPosibleLaInteraccion(jugador))
                {
                    encima = true;
                    if (Input.GetMouseButtonDown(0))
                        item.Accion(jugador);
                }
            }
            if(encima)
                punteroAnimacion.SetTrigger("Interactuar");
            else
                punteroAnimacion.SetTrigger("Estatico");                
        }
            
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Interactuable aux = collision.gameObject.GetComponent<Interactuable>();

		if (aux != null) 
            objetoInteractuable.Add(aux);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Interactuable aux = collision.gameObject.GetComponent<Interactuable>();
        if (aux != null)
            objetoInteractuable.Remove(aux);
    }
    public void Muerto(bool m)
    {
        muerto = m;
    }

    public void Invisible(bool a)
    {
        punteroAnimacion.GetComponent<SpriteRenderer>().enabled = !a;
    }
}
