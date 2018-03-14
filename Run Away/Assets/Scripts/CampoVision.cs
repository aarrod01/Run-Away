using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monster;

public class CampoVision : MonoBehaviour 
{
	public LayerMask queGolpear;

	MonsterMovement monstruo;
	Transform jugador;

	void Start()
	{
		monstruo = GetComponentInParent<MonsterMovement> ();
		jugador = GameObject.FindWithTag ("Player").GetComponent<Transform> ();
	}
		
	void OnTriggerStay2D (Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			RaycastHit2D hit = Physics2D.Raycast (transform.position, jugador.position - transform.position, 100f, queGolpear);
			if (hit.collider.gameObject.tag == "Player") 
			{
				monstruo.CambiarEstadoMonstruo (EstadosMonstruo.SiguiendoJugador);
				Debug.DrawRay (transform.position, (jugador.position - transform.position).normalized, Color.green);
			} 
			else if (monstruo.EstadoMonstruoActual () == EstadosMonstruo.SiguiendoJugador) 
			{
				monstruo.CambiarEstadoMonstruo (EstadosMonstruo.VolviendoARuta);
				Debug.DrawRay (transform.position, (jugador.position - transform.position).normalized, Color.red);
			}
		}
	}
	//QUE CUANDO EL RAYO SE CORTE, DEJE DE SEGUIRLO
	void OnTriggerExit2D (Collider2D other)
	{
		if (other.gameObject.tag == "Player" && monstruo.EstadoMonstruoActual() == EstadosMonstruo.SiguiendoJugador) 
			monstruo.CambiarEstadoMonstruo(EstadosMonstruo.VolviendoARuta);
	}
}
