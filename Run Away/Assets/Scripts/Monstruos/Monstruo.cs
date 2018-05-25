using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monstruos;

namespace Monstruos
{
    public enum EstadosMonstruo { SiguiendoJugador, EnRuta, PensandoRuta, VolviendoARuta, Atacando ,Desorientado, Proyectado, BuscandoJugador, Huyendo, Quieto, Ninguno };
    public enum TipoMonstruo { Panzudo, Fantasma, Ninguno };
}

public class Monstruo : MonoBehaviour
{
    TipoMonstruo tipo;
    EstadosMonstruo estadoMonstruo;
    Rigidbody2D rb2D;
    float cronometro;
    public int prioridad;
    

    public const float MARGEN = 0.1f;

    public funcionVacia Comportamiento;
    public Rigidbody2D Rb2D { get { return rb2D; }set { rb2D = value; } }
    public float Cronometro { get { return cronometro; } set { cronometro = value; } }
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

    public void Empujar(Vector2 origen, float fuerzaProyeccion)
    {
        rb2D.AddForce((rb2D.position - origen).normalized * fuerzaProyeccion,ForceMode2D.Impulse);
        cronometro = Time.time;
    }

    public funcionInteractuado Atacado;

    public funcionVacia Morir;

    public funcionVacia Atacar;

    public funcionVacia FinalAtaque;

    public funcionVacia EntrandoLuz = () => { };

    public funcionVacia SaliendoLuz = () => { };

}

