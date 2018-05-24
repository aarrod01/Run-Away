using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactuable))]

public class Droga : MonoBehaviour 
{
    public AudioSource cogerDroga;
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
        master.Accion += (Jugador a) =>
        {
            cogerDroga.Play();
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
        Destroy(this);
        Destroy(GetComponent<SpriteRenderer>());
        Destroy(gameObject, cogerDroga.clip.length);
    }
}
