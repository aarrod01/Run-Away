using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Monstruos;
public class GameManager : MonoBehaviour 
{
    int monstruosVivos = 0;
    int drogaConsumida = 0;

    Jugador jugador = null;
	Scene escena;

    public float tiempoSubidon;
    public static GameManager instance = null;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
			escena = SceneManager.GetActiveScene ();
			if (jugador == null)
				jugador = GameObject.FindObjectOfType<Jugador>();
        }
        else
            Destroy(this.gameObject);
    }

    public void MonstruoMuerto(TipoMonstruo tipo)
    {

    }

	public void ResetarEscena ()
	{
		SceneManager.LoadScene (escena.name);
	}

    public void ConsumirDroga()
    {
        drogaConsumida++;
        jugador.Luz().IntensidadLuz(1.5f);
        jugador.AumentoVelocidad(1.5f);
        Invoke("Bajon", TiempoSubidon());
    }

    float TiempoSubidon()
    {
        return tiempoSubidon;
    }

    void Bajon()
    {
        jugador.Luz().IntensidadLuz(0.9f);
        jugador.AumentoVelocidad(0.9f);
    }

}
