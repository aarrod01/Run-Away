using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactuable))]

public class Droga : MonoBehaviour 
{
    public AudioClip cogerDroga;
    [Range (0, 1)]
    public float volumenCogerDroga;
    public float distanciaDeInteraccion;


    Interactuable master;
    LayerMask conQueColisiona;

    private void Start()
    {
       /* if (cogerDrogaSonido == null) { 
            cogerDrogaSonido = new Sonidosss(cogerDroga, false, true, volumenCogerDroga, 1f, SoundManager.instance.VolumenSonidos);
            DontDestroyOnLoad(cogerDrogaSonido.gO);
        }*/
        conQueColisiona = LayerMask.GetMask("Obstaculos", "Jugador");
        master = GetComponent<Interactuable>();
        master.Accion = (Jugador a) =>
        {
            //cogerDrogaSonido.Activar();
            DrogaConsumida();
        };
        master.EsPosibleLaInteraccion = (Jugador a) =>
        {
            return master.InteraccionPorLineaDeVision(a.transform, distanciaDeInteraccion, conQueColisiona);
        };
        master.DistanciaDeInteraccion = () => { return distanciaDeInteraccion; };
    }

    void DrogaConsumida()
    {
        GameManager.instance.ConsumirDroga();
        Destroy(gameObject);
    }
}
