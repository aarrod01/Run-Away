using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monster;

public class CampoVision : MonoBehaviour 
{
	public LayerMask queGolpear;

	MonsterMovement monstruo;
	Transform jugador;
    Vector2 ultimaPosicionJugador;
    

    void Start()
	{
        ultimaPosicionJugador = Vector2.negativeInfinity;
		monstruo = GetComponentInParent<MonsterMovement> ();
		jugador = GameObject.FindWithTag ("Player").GetComponent<Transform> ();
	}
    public Vector2 UltimaPosicionJugador()
    {
        return ultimaPosicionJugador;
    }
	void OnTriggerStay2D (Collider2D other)
	{
		if (other.gameObject.tag == "Player"&&!other.GetComponent<PlayerMovement>().Invisible())
		{
			RaycastHit2D hit = Physics2D.Raycast (transform.position, jugador.position - transform.position, 100f, queGolpear);
			if (hit.collider.gameObject.tag == "Player") 
			{
				monstruo.CambiarEstadoMonstruo (EstadosMonstruo.SiguiendoJugador);
                ultimaPosicionJugador=jugador.position;
			}
            /*else if (monstruo.EstadoMonstruoActual () == EstadosMonstruo.SiguiendoJugador) 
    {
        monstruo.CambiarEstadoMonstruo (EstadosMonstruo.PensandoRuta);
    }*/
        }
	}
	//QUE CUANDO EL RAYO SE CORTE, DEJE DE SEGUIRLO
	/*void OnTriggerExit2D (Collider2D other)
	{
		if (other.gameObject.tag == "Player" && monstruo.EstadoMonstruoActual() == EstadosMonstruo.SiguiendoJugador) 
			monstruo.CambiarEstadoMonstruo(EstadosMonstruo.PensandoRuta);
	}*/
}
