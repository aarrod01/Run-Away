using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour {

	public int vida = 1;
    Monstruo monstruo;
	Jugador jugador;

    void Start()
    {
        monstruo = GetComponent<Monstruo>();
		jugador = GetComponent<Jugador> ();
    }


    public void Danyar(int danyo)
    {
        vida -= danyo;
        if (vida <= 0)
        {
            Muerte();
        }     
    }

    void Muerte()
    {
		if (monstruo != null)
			GameManager.instance.MonstruoMuerto (monstruo.tipo);
		else if (jugador != null)
			GameManager.instance.JugadorMuerto ();
		else
       		Destroy(gameObject);
    }

}
