﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Monstruos;
public delegate void funcionVacia();
public class GameManager : MonoBehaviour
{
    int[] monstruosMuertos, monstruosMuertosTemporales;
    int[] monstruosHuidos, monstruosHuidosTemporales;
    int[] monstruosIgnorados;
    public int drogaConsumida = 0;
	public int nivel = 0;

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
        instance.IniciarEscena();
    }

	public void IniciarEscena()
	{

        int n = Enum.GetValues(typeof(TipoMonstruo)).Length;
        monstruosMuertosTemporales = new int[n];
        monstruosHuidosTemporales = new int[n];
        Monstruo[] monstruos = GameObject.FindObjectsOfType<Monstruo>();
        for(int i = 0; i<monstruos.Length; i++)
        {
            if(monstruos[i].prioridad>PrioridadMaxima(monstruos[i].Tipo()))
                Destroy(monstruos[i].gameObject);
        }
		jugador = GameObject.FindObjectOfType<Jugador>();
	}
public void CambiarEscena(string nombreEscenaActual)
    {
        switch (nombreEscenaActual)
        {
            case "Inicio":
                nivel = 0;
                Debug.Log("Se ha cambiado a la escena: " + nivel);
                SceneManager.LoadScene("Inicio");
                break;
            case "Nivel1":
                nivel = 1;
                Debug.Log("Se ha cambiado a la escena: " + nivel);
                SceneManager.LoadScene("Nivel1");
                break;
            case "Nivel2":
                nivel = 2;
                Debug.Log("Se ha cambiado a la escena: " + nivel);
                SceneManager.LoadScene("Nivel2");
                break;
        }
    }
    
    public void TerminarExitosamenteEscena()
    {

        int n = Enum.GetValues(typeof(TipoMonstruo)).Length;
        for(int i =0; i<n; i++)
        {
            monstruosMuertos[i] += monstruosMuertosTemporales[i];
            monstruosHuidos[i] += monstruosHuidosTemporales[i];
        }
        Monstruo[] monstruos = GameObject.FindObjectsOfType<Monstruo>();
        for(int i = 0; i<monstruos.Length; i++)
        {
            monstruosIgnorados[(int)monstruos[i].Tipo()]++;
        }

        switch (SceneManager.GetActiveScene().name)
        {
            
            case "Nivel1":
                nivel = 2;
                SceneManager.LoadScene("Nivel2");
                break;
            case "Nivel2":
                nivel = 0;
                SceneManager.LoadScene("Inicio");
                break;
        }
    }

    public void MonstruoMuerto(TipoMonstruo tipo)
    {
        monstruosMuertosTemporales[(int)tipo]++;
    }

    public void MontruoHuye(TipoMonstruo tipo)
    {
        monstruosHuidosTemporales[(int)tipo]++;
    }

    void MonstruosIgnorados()
    {
        Monstruo[] aux = GameObject.FindObjectsOfType<Monstruo>();
        for(int i =0; i<aux.Length;i++)
        {
            MonstruoIgnorado(aux[i].Tipo());
        }
    }
    void MonstruoIgnorado(TipoMonstruo tipo)
    {
        monstruosIgnorados[(int)tipo]++;
    }


	public void JugadorMuerto()
	{
		Invoke("ResetarEscena", 5f);
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
        Invoke("Bajonazo",TiempoSubidon());
        ControladorPalanca.instance.EncenderPalancas();
        Bajon = () =>
        {
            ControladorPalanca.instance.ApagarPalancas();
            drogado = false;
            jugador.Luz().IntensidadLuz(0.9f);
            jugador.AumentoVelocidad(0.9f);
        };
    }

    void Bajonazo()
    {
        Bajon();
    }

    float TiempoSubidon()
    {
        return tiempoSubidon;
    }

    public bool Drogado()
    {
        return drogado;
    }

    int PrioridadMaxima(TipoMonstruo tipo)
    {
        return 2*monstruosHuidos[(int)tipo] + monstruosIgnorados[(int)tipo];
    }

}
