using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monstruos;

namespace Monstruos
{
    public enum EstadosMonstruo { SiguiendoJugador, EnRuta, PensandoRuta, VolviendoARuta, Desorientado, Proyectado, BuscandoJugador, Huyendo, Ninguno };
    public enum TipoMonstruo { Basico, Ninguno };
}

public class Monstruo : MonoBehaviour
{
    public LayerMask conQueColisiona;
    public float velMovRuta, velMovPerseguir, velMovHuida, velGiro, aceleracionAngular, tiempoAturdimiento = 1f, periodoGiro = 1f;
    public EstadosMonstruo estadoMonstruo;
    public TipoMonstruo tipo;
    public int prioridad;

    Rigidbody2D rb2D;
    Rigidbody2D jugadorRB;
    float giroInicial;
    float cronometro;
    DetectarRuta detectorRuta;
    CampoVision campoVision;
    DetectorParedes detectorParedes;

    const float MARGEN = 0.001f;

    void Start()
    {
        detectorRuta = GetComponentInChildren<DetectarRuta>();
        campoVision = GetComponentInChildren<CampoVision>();
        detectorParedes = GetComponentInChildren<DetectorParedes>();
        rb2D = GetComponent<Rigidbody2D>();
        jugadorRB = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        estadoMonstruo = EstadosMonstruo.EnRuta;
    }

    void FixedUpdate()
    {
        switch (estadoMonstruo)
        {
            case EstadosMonstruo.EnRuta:
                MoverseHacia(detectorParedes.EvitarColision(detectorRuta.PosicionPuntoRuta()), velMovRuta);
                break;
            case EstadosMonstruo.SiguiendoJugador:
                if ((campoVision.UltimaPosicionJugador() - rb2D.position).sqrMagnitude < MARGEN)
                {
                    CambiarEstadoMonstruo(EstadosMonstruo.Desorientado);
                }
                MoverseHacia(detectorParedes.EvitarColision(campoVision.UltimaPosicionJugador()), velMovPerseguir);
                break;
            case EstadosMonstruo.VolviendoARuta:
                MoverseHacia(detectorParedes.EvitarColision(detectorRuta.PosicionPuntoRuta()), velMovRuta);
                break;
            case EstadosMonstruo.Desorientado:
                Pararse();
                giroInicial = rb2D.rotation;
                cronometro = Time.time;

                CambiarEstadoMonstruo(EstadosMonstruo.BuscandoJugador);
                break;
            case EstadosMonstruo.BuscandoJugador:
                Pararse();
                Vector2 aux = new Vector2(Mathf.Sin(((Time.time - cronometro) / periodoGiro + giroInicial / 360f) * 2 * Mathf.PI), Mathf.Cos(((Time.time - cronometro) / periodoGiro + giroInicial / 360f) * 2 * Mathf.PI));
                GiroInstantaneo(aux);
                if (Time.time - cronometro > periodoGiro)
                {
                    CambiarEstadoMonstruo(EstadosMonstruo.PensandoRuta);
                }
                break;
            case EstadosMonstruo.Proyectado:
                if (Time.time - cronometro > tiempoAturdimiento)
                    CambiarEstadoMonstruo(EstadosMonstruo.Desorientado);
                break;
            case EstadosMonstruo.Huyendo:
                MoverseHacia((2 * rb2D.position - jugadorRB.position), velMovHuida);
                GameManager.instance.MontruoHuye(tipo);
                GetComponent<Collider2D>().enabled = false;
                Destroy(gameObject, 10f);
                Destroy(this);
                break;
        }
    }

    public void CambiarEstadoMonstruo(EstadosMonstruo estado)
    {
        estadoMonstruo = estado;
    }
    public EstadosMonstruo EstadoMonstruoActual()
    {
        return estadoMonstruo;
    }

    //Seguir al jugador, moverse por la ruta y volver a la ruta.
    void MoverseHacia(Vector2 dir, float vel)
    {
        rb2D.velocity = (dir - (Vector2)transform.position).normalized * vel;
        GiroInstantaneo(rb2D.velocity);
    }

    void Giro(Vector2 dir)
    {
        if (dir != Vector2.zero)
            rb2D.rotation = Mathf.LerpAngle(rb2D.rotation, Mathf.Atan2(-dir.x, dir.y) * 180f / Mathf.PI, aceleracionAngular);
    }
    void GiroInstantaneo(Vector2 dir)
    {
        if (dir != Vector2.zero)
            rb2D.rotation = Mathf.Atan2(-dir.x, dir.y) * 180f / Mathf.PI;
    }

    public void Empujar(Vector2 origen, float velocidadProyeccion)
    {
        rb2D.velocity = (rb2D.position - origen).normalized * velocidadProyeccion;
        cronometro = Time.time;
        CambiarEstadoMonstruo(EstadosMonstruo.Proyectado);
    }

    void Pararse()
    {
        rb2D.velocity = Vector2.zero;
    }

    void Stop()
    {
        rb2D.velocity = Vector2.zero;
    }

}

