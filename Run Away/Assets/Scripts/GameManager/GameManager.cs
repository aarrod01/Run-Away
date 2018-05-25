using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Monstruos;
using UnityEngine.SceneManagement;

public delegate void funcionVacia();
public class GameManager : MonoBehaviour
{
    public funcionVacia Bajon = () => { };
    public static GameManager instance = null;

    public int drogaConsumida = 0;
    public float tiempoSubidon, 
                velocidadMinimaDroga, 
                velocidadMaximaDroga,
                numeroDrogaMaxima,
                intensidadLuzMaxima,
                intensidadLuzMinima;
    public int[] monstruosMuertos,
        monstruosMuertosTemporales,
        monstruosHuidos,
        monstruosHuidosTemporales,
        monstruosIgnorados;
    public int NumeroDeMuertes = 0;
    public string Nivel;

    Jugador jugador = null;

    bool drogado = false;
    float cronometro;
    int drogaConsumidaTemporal = 0;

    void Awake()
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
    private void Update()
    {
        Bajonazo().MoveNext();
    }
    public void IniciarEscena()
	{
        Nivel = SceneManager.GetActiveScene().name;
        cronometro = Time.time;
        if(SoundManager.instance!=null)
            SoundManager.instance.CambiarTonoMusica(SoundManager.instance.tonoPredeterminado);
        Bajon = () => { };
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
    
    public void TerminarExitosamenteEscena()
    {
        if (SceneManager.GetActiveScene().name != "NivelTutorial")
        {
            int n = Enum.GetValues(typeof(TipoMonstruo)).Length;
            for (int i = 0; i < n; i++)
            {
                monstruosMuertos[i] += monstruosMuertosTemporales[i];
                monstruosHuidos[i] += monstruosHuidosTemporales[i];
            }
            Monstruo[] monstruos = GameObject.FindObjectsOfType<Monstruo>();
            for (int i = 0; i < monstruos.Length; i++)
            {
                monstruosIgnorados[(int)monstruos[i].Tipo()]++;
            }
            drogaConsumida += drogaConsumidaTemporal;
        }
        switch (SceneManager.GetActiveScene().name)
        {
            case "NivelTutorial":
                SceneManager.LoadScene("Nivel1");
                break;
            case "Nivel1":
                SceneManager.LoadScene("Nivel2");
                break;
            case "Nivel2":
                SceneManager.LoadScene("NivelFinal");
                break;
            case "NivelFinal":
                SceneManager.LoadScene("Creditos");
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
        if (SceneManager.GetActiveScene().name != "NivelFinal")
        {
            Invoke("ResetarEscena", 5f);
            NumeroDeMuertes++;
        }
        else
        {
            GameObject.FindObjectOfType<FinalPartida>().Fin(jugador.transform);
        }
    }

	public void ResetarEscena ()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}

    public void ConsumirDroga()
    {
        drogado = true;
        drogaConsumidaTemporal++;
        jugador.Luz().IntensidadLuz(1f - (1f - intensidadLuzMaxima) * atenuacionDroga());
        jugador.AumentoVelocidad(1f - (1f - velocidadMaximaDroga) * atenuacionDroga());
        SoundManager.instance.CambiarTonoMusica(SoundManager.instance.tonoDrogado);
        Invoke("Bajonazo",TiempoSubidon());
        ControladorPalanca.instance.EncenderPalancas();
        cronometro = Time.time;
        GameObject.FindGameObjectWithTag("LuzDroga").GetComponent<LuzDroga>().Luces(drogado);
        Bajon += () =>
        {
            ControladorPalanca.instance.ApagarPalancas();
            drogado = false;
            jugador.Luz().IntensidadLuz(1f - (1f - intensidadLuzMinima) * atenuacionDroga());
            jugador.AumentoVelocidad(1f - (1f - velocidadMinimaDroga) * atenuacionDroga());
            SoundManager.instance.CambiarTonoMusica(SoundManager.instance.tonoPredeterminado);
            GameObject.FindGameObjectWithTag("LuzDroga").GetComponent<LuzDroga>().Luces(drogado);
            Bajon = () => { };
        };
    }

    float atenuacionDroga()
    {
        return (drogaConsumida +drogaConsumidaTemporal)/ numeroDrogaMaxima;
    }

    IEnumerator Bajonazo()
    {
        while (Time.time - cronometro < tiempoSubidon)
            yield return 0;
        Bajon();
        yield return null;
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
        return 10;// 2*monstruosHuidos[(int)tipo] + monstruosIgnorados[(int)tipo];
    }

    public void IrAEscena(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }

}
