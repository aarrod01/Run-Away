using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golpe : MonoBehaviour 
{
	Jugador jugador;

	void OnTriggerEnter2D(Collider2D other)
	{
        if(other.tag =="Player")
		    jugador = other.gameObject.GetComponent<Jugador> ();	
	}

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
            jugador = null;
    }

    public bool LeGolpea()
	{
		return jugador != null;
	}
}
