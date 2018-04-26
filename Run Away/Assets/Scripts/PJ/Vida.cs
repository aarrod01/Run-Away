using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monstruos;

public class Vida : MonoBehaviour {

	public int vida = 1;
    Monstruo monstruo;
	Jugador jugador;
    bool invulnerable = false;

    void Start()
    {
        monstruo = GetComponent<Monstruo>();
		jugador = GetComponent<Jugador> ();
    }


    public void Danyar(int danyo, TipoMonstruo tipo)
    {
        if (!invulnerable)
        {
            vida -= danyo;
            if (vida <= 0)
            {
                Muerte(tipo);
            }
        }
    }

    void Muerte(TipoMonstruo tipo)
    {
        if (monstruo != null)
        {
            monstruo.Morir();
        }
        else if (jugador != null)
        {
            jugador.Morir(tipo);
        }
        else
            Destroy(gameObject);
    }
    public void Invulnerable(bool inv)
    {
        invulnerable = inv;
    }

}
