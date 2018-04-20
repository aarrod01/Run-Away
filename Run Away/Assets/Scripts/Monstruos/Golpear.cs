using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golpear : MonoBehaviour 
{
	Vida vidaJugador;
	Jugador jugador;
	Golpe golpe;
    bool golpeando;
	//Animator animacionMonstruo;

	public int danyoMonstruo = 1;
	public float golpeRetardo = 0.2f;

	void Start () 
	{
		vidaJugador = GameObject.FindWithTag ("Player").GetComponent<Vida> ();
		jugador = GameObject.FindWithTag ("Player").GetComponent<Jugador> ();
		//animacionMonstruo = GetComponentInParent <Animator>();
		golpe = GetComponentInChildren<Golpe>();
        golpeando = false;
	}
	

	void Update () 
	{
		
	}

	public void Golpeando ()
	{
        golpeando = true;
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
        golpeando = false;
	}

    public bool EstaGolpeando()
    {
        return golpeando;
    }
	public void Ejemplo()
	{
		
	}
}
