﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monstruos;

namespace Monstruos
{
    public enum EstadosMonstruo { SiguiendoJugador, EnRuta, PensandoRuta, VolviendoARuta, Atacando ,Desorientado, Proyectado, BuscandoJugador, Huyendo, Quieto, Ninguno };
    public enum TipoMonstruo { Panzudo, Fantasma, Ninguno };
}
public delegate void funcionGameObject (GameObject gO);
public class Monstruo : MonoBehaviour
{
    TipoMonstruo tipo;
    EstadosMonstruo estadoMonstruo;
    public int prioridad;

    public const float MARGEN = 0.001f;

    public funcionVacia Comportamiento;
    void FixedUpdate()
    {
        Comportamiento();
    }

    public void CambiarEstadoMonstruo(EstadosMonstruo estado)
    {
        estadoMonstruo = estado;
    }
    public EstadosMonstruo EstadoMonstruoActual()
    {
        return estadoMonstruo;
    }

    public void Tipo(TipoMonstruo _tipo)
    {
        tipo = _tipo;
    }

    public TipoMonstruo Tipo()
    {
        return tipo;
    }

    public funcionInteractuado Atacado;

    public funcionVacia Morir;

    public funcionVacia Atacar;

    public funcionVacia FinalAtaque;

    public funcionGameObject entrandoLuz = (GameObject gO) => { };

    public void EntrandoLuz(GameObject gO)
    {
        Debug.Log("Entro");
        entrandoLuz(gO);
    }

    public funcionGameObject saliendoLuz = (GameObject gO) => { };

    public void SaliendoLuz(GameObject gO)
    {
        Debug.Log("Salgo");
        saliendoLuz(gO);
    }

}

