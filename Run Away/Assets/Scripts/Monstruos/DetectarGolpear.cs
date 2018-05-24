using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monstruos;

public class DetectarGolpear : MonoBehaviour 
{
    Monstruo este;
	Golpear golpear;

	void Start () 
	{
        este = GetComponentInParent<Monstruo>();
		golpear = GetComponentInParent<Golpear> ();
	}
	

	void OnTriggerStay2D(Collider2D other)
	{
		if (este.EstadoMonstruoActual()!=EstadosMonstruo.Quieto && other.gameObject.tag == "Player" && !golpear.EstaGolpeando() && !other.GetComponent<Jugador>().Invisible()) 
		{
			golpear.Golpeando ();
		}
	}
}
