using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monster;
public class GameManager : MonoBehaviour {




    int monstruosVivos = 0;
    int drogaConsumida = 0;
    PlayerMovement jugador;

    public float tiempoSubidon;

    public static GameManager instance = null;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);

        jugador = GameObject.FindObjectOfType<PlayerMovement>();
    }

    public void MonstruoMuerto(TipoMonstruo tipo)
    {

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
