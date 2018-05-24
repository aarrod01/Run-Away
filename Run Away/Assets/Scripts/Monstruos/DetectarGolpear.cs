using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectarGolpear : MonoBehaviour 
{
	Golpear golpear;

	void Start () 
	{
		golpear = GetComponentInParent<Golpear> ();
	}
	

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player" && !golpear.EstaGolpeando() && !other.GetComponent<Jugador>().Invisible()) 
		{
			golpear.Golpeando ();
		}
	}
}
