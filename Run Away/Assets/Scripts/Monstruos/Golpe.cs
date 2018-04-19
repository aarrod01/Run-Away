using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golpe : MonoBehaviour 
{
	Jugador jugador;

	void Start () 
	{
		
	}

	void Update () 
	{
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		jugador = other.gameObject.GetComponent<Jugador> ();	
	}

	public bool LeGolpea()
	{
		return jugador != null;
	}
}
