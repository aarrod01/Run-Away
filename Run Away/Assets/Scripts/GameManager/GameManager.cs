using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Monstruos;
public delegate void funcionVacia();
public class GameManager : MonoBehaviour
{
    int[] monstruosMuertos;
    int[] monstruosHuidos;
    int[] monstruosIgnorados;
    int drogaConsumida = 0;

    Jugador jugador = null;
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
            int n = Enum.GetValues(typeof(TipoMonstruo)).Length;
            monstruosMuertos = new int[n];
            monstruosIgnorados = new int[n];
            monstruosHuidos = new int[n];
        }
        else
            Destroy(this.gameObject);
        SceneManager.activeSceneChanged += IniciarEscena;
    }

	public void IniciarEscena(Scene actual, Scene siguiente)
	{
        Monstruo[] monstruos = GameObject.FindObjectsOfType<Monstruo>();
        for(int i = 0; i<monstruos.Length; i++)
        {
            if(monstruos[i].prioridad>PrioridadMaxima())
                Destroy(monstruos[i].gameObject);
        }
		jugador = GameObject.FindObjectOfType<Jugador>();
	}

    public void MonstruoMuerto(TipoMonstruo tipo)
    {
        monstruosMuertos[(int)tipo]++;
    }

    public void MontruoHuye(TipoMonstruo tipo)
    {
        monstruosHuidos[(int)tipo]++;
    }

    void MonstruosIgnorados()
    {
        Monstruo[] aux = GameObject.FindObjectsOfType<Monstruo>();
        for(int i =0; i<aux.Length;i++)
        {
            MonstruoIgnorado(aux[i].tipo);
        }
    }
    void MonstruoIgnorado(TipoMonstruo tipo)
    {
        monstruosIgnorados[(int)tipo]++;
    }


	public void JugadorMuerto()
	{
		ResetarEscena ();
	}

	public void ResetarEscena ()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
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

    int PrioridadMaxima()
    {
        return 0;
    }

}
