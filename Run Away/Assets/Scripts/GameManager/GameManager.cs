using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Monstruos;
public delegate void funcionVacia();
public class GameManager : MonoBehaviour
{
    int monstruosVivos = 0;
    int drogaConsumida = 0;

    Jugador jugador = null;
    Scene escena;
    bool drogado = false;

    public funcionVacia Bajon = () => {};
    public float tiempoSubidon;
    public static GameManager instance = null;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
			escena = SceneManager.GetActiveScene ();
			//SceneManager.activeSceneChanged += 
        }
        else
            Destroy(this.gameObject);
    }

	public void IniciarEscena()
	{
		jugador = GameObject.FindObjectOfType<Jugador>();
	}

    public void MonstruoMuerto(TipoMonstruo tipo)
    {

    }

	public void JugadorMuerto()
	{
		ResetarEscena ();
	}

	public void ResetarEscena ()
	{
		SceneManager.LoadScene (escena.name);
	}

    public void ConsumirDroga()
    {
        drogado = true;
        drogaConsumida++;
        jugador.Luz().IntensidadLuz(1.5f);
        jugador.AumentoVelocidad(1.5f);
        Invoke("Bajon", TiempoSubidon());
        ControladorPalanca.instante.EncenderPalancas();
        Bajon = () =>
        {
            ControladorPalanca.instante.ApagarPalancas();
            drogado = false;
            jugador.Luz().IntensidadLuz(0.9f);
            jugador.AumentoVelocidad(0.9f);
        };
    }

    float TiempoSubidon()
    {
        return tiempoSubidon;
    }

    public bool Drogado()
    {
        return drogado;
    }

}
