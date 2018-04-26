using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monstruos;

public class Golpear : MonoBehaviour 
{
	Vida vidaJugador;
	Jugador jugador;
    Monstruo monstruo;
	Golpe golpe;
    bool golpeando;
	//Animator animacionMonstruo;

	public int danyo = 1;
	public float golpeRetardo = 1f;

	void Start () 
	{
        monstruo = GetComponentInParent<Monstruo>();
		vidaJugador = GameObject.FindWithTag ("Player").GetComponent<Vida> ();
		jugador = GameObject.FindWithTag ("Player").GetComponent<Jugador> ();
		//animacionMonstruo = GetComponentInParent <Animator>();
		golpe = GetComponentInChildren<Golpe>();
        golpeando = false;
	}

	public void Golpeando ()
	{
        monstruo.CambiarEstadoMonstruo(EstadosMonstruo.Atacando);
        golpeando = true;
        monstruo.Atacar();
		Invoke ("Impacto", golpeRetardo);
	}

	void Impacto ()
	{
		if (golpe.LeGolpea () && monstruo.isActiveAndEnabled) 
		{
			vidaJugador.Danyar(danyo, GetComponentInParent<Monstruo>().Tipo());
		}
        golpeando = false;
        monstruo.FinalAtaque();
    }

    public bool EstaGolpeando()
    {
        return golpeando;
    }
}
