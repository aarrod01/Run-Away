using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monstruos;

public class CampoVision : MonoBehaviour
{
    LayerMask queGolpear;

    Monstruo monstruo;
    Transform jugador;
    Vector2 ultimaPosicionJugador;


    void Start()
    {
        queGolpear = LayerMask.GetMask("Obstaculos", "Jugador");
        ultimaPosicionJugador = Vector2.negativeInfinity;
        monstruo = GetComponentInParent<Monstruo>();
        jugador = GameObject.FindObjectOfType<Jugador>().GetComponent<Transform>();
    }
    public Vector2 UltimaPosicionJugador()
    {
        return ultimaPosicionJugador;
    }
    void OnTriggerStay2D(Collider2D other)
    {
        Jugador aux;
        if ((aux = other.GetComponent<Jugador>()) != null && !aux.Invisible())
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, jugador.position - transform.position, 100f, queGolpear);
            if (monstruo.EstadoMonstruoActual() != EstadosMonstruo.Proyectado && hit.collider.gameObject.tag == "Player")
            {
                monstruo.CambiarEstadoMonstruo(EstadosMonstruo.SiguiendoJugador);
                ultimaPosicionJugador = jugador.position;
            }

        }
    }

}