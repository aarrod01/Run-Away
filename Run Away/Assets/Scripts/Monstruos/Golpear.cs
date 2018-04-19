using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golpear : MonoBehaviour 
{
	Vida vidaJugador;
	Jugador jugador;
	Golpe golpe;
	//Animator animacionMonstruo;

	public int danyoMonstruo = 1;
	public float golpeRetardo = 0.2f;

	void Start () 
	{
		vidaJugador = GameObject.FindWithTag ("Player").GetComponent<Vida> ();
		jugador = GameObject.FindWithTag ("Player").GetComponent<Jugador> ();
		//animacionMonstruo = GetComponentInParent <Animator>();
		golpe = GetComponentInChildren<Golpe>();
	}
	

	void Update () 
	{
		
	}

	public void Golpeando ()
	{
		Invoke ("Impacto", golpeRetardo);
	}

	void Impacto ()
	{
		if (golpe.LeGolpea ()) 
		{
			jugador.MovimientoLibre (false);
			//animacionMonstruo.SetTrigger ("Golpear");
			vidaJugador.Danyar(danyoMonstruo);
		}
	}

	public void Ejemplo()
	{
		
	}
}
