using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
<<<<<<< HEAD:Run Away/Assets/Scripts/PJ/PunteroRetardo.cs
public class PunteroRetardo : MonoBehaviour {

    Interactuable objetoInteractuable = null, objetoInteractuable2 = null;
    Jugador jugador;
=======
public class Pointer : MonoBehaviour 
{
    Interactuable objetoInteractuable = null;
    PlayerMovement player;
>>>>>>> 5c036531624aa2b64f7ec7211d67ba52912b1db0:Run Away/Assets/Scripts/PJ/Pointer.cs
    Rigidbody2D puntero;
	Animator punteroAnimacion;

    public float lerp = 0.5f;

<<<<<<< HEAD:Run Away/Assets/Scripts/PJ/PunteroRetardo.cs
	// Use this for initialization
	void Start () {
        jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Jugador>();
=======
	void Start () 
	{
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
>>>>>>> 5c036531624aa2b64f7ec7211d67ba52912b1db0:Run Away/Assets/Scripts/PJ/Pointer.cs
        puntero = GetComponent<Rigidbody2D>();
		punteroAnimacion = GetComponent<Animator> ();
	}

	void Update () 
	{
        puntero.MovePosition(Vector2.Lerp(puntero.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), lerp));
<<<<<<< HEAD:Run Away/Assets/Scripts/PJ/PunteroRetardo.cs
        if (Input.GetMouseButtonDown(0) && objetoInteractuable != null && objetoInteractuable.EsPosibleLaInteraccion(jugador))
        {
            objetoInteractuable.Accion(jugador);
        }

=======
        if (Input.GetMouseButtonDown(0) && objetoInteractuable != null)
            objetoInteractuable.Click(player);
>>>>>>> 5c036531624aa2b64f7ec7211d67ba52912b1db0:Run Away/Assets/Scripts/PJ/Pointer.cs
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Interactuable aux = collision.gameObject.GetComponent<Interactuable>();
<<<<<<< HEAD:Run Away/Assets/Scripts/PJ/PunteroRetardo.cs
        if (aux != null)
            objetoInteractuable = aux;
=======
		if (aux != null) 
		{
			punteroAnimacion.SetTrigger ("Interactuar");
			objetoInteractuable = aux;
		}
>>>>>>> 5c036531624aa2b64f7ec7211d67ba52912b1db0:Run Away/Assets/Scripts/PJ/Pointer.cs
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
		if (objetoInteractuable != null && objetoInteractuable.gameObject == collision.gameObject) 
		{
			punteroAnimacion.SetTrigger ("Estatico");
			objetoInteractuable = null;
		}
    }
}
