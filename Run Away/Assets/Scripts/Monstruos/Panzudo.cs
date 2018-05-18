using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monstruos;

[RequireComponent(typeof(Monstruo))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Panzudo : MonoBehaviour {
    
    public AudioClip gritoCarga;
    [Range(0, 1)]
    public float volumenGritoCarga;
    public AudioClip pasos;
    [Range(0, 1)]
    public float volumenPasos;
    public AudioClip respiracionBusqueda;
    [Range(0, 1)]
    public float volumenRespiracionBusqueda;
    bool cargando, andando;
    Sonidosss sonidoCarga, sonidoPasos, sonidoRespiracion;

    public float intensidadSonido = 10f, velMovRuta, velMovPerseguir, velMovHuida, velGiro, aceleracionAngular, tiempoAturdimiento = 1f, periodoGiro = 1f;
    public EstadosMonstruo estadoInicial;
    
    Rigidbody2D rb2D;
    float cronometro = 0;

    private void Start()
    {
        sonidoCarga = new Sonidosss(gritoCarga, false, false, volumenGritoCarga, 1f, SoundManager.instance.VolumenSonidos);
        sonidoPasos = new Sonidosss(pasos, true, false, volumenPasos, 1f, SoundManager.instance.VolumenSonidos);
        sonidoRespiracion = new Sonidosss(respiracionBusqueda, false, false, volumenRespiracionBusqueda, 1f, SoundManager.instance.VolumenSonidos);
        SoundManager.instance.IntroducirGeneradorSonidos(transform, sonidoCarga, sonidoPasos, sonidoRespiracion);

        Rigidbody2D jugadorRB;
        Animator animador;
        float giroInicial = 0;
        DetectarRuta detectorRuta;
        CampoVision campoVision;
        DetectorParedes detectorParedes;
        Vida vida;
        Golpeable detectorGolpes;

        rb2D = GetComponent<Rigidbody2D>();
        jugadorRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        animador = GetComponent<Animator>();
        detectorRuta = GetComponentInChildren<DetectarRuta>();
        campoVision = GetComponentInChildren<CampoVision>();
        detectorParedes = GetComponentInChildren<DetectorParedes>();
        vida = GetComponent<Vida>();
        detectorGolpes = GetComponentInChildren<Golpeable>();

        Monstruo este = GetComponent<Monstruo>();
        este.Tipo(TipoMonstruo.Panzudo);

        este.CambiarEstadoMonstruo(estadoInicial);
        este.Comportamiento = () => {
            float volumen = Mathf.Min(5f / (jugadorRB.position - rb2D.position).sqrMagnitude, 1f);

            switch (este.EstadoMonstruoActual())
            {
                case EstadosMonstruo.EnRuta:
                    if (!andando)
                    {
                        sonidoPasos.Activar();
                        andando = !andando;
                        sonidoRespiracion.Desactivar();
                    }
                    MoverseHacia(detectorParedes.EvitarColision(detectorRuta.PosicionPuntoRuta()), velMovRuta);
                    break;
                case EstadosMonstruo.SiguiendoJugador:
                    if (!cargando)
                    {
                        sonidoPasos.Desactivar();
                        cargando = true;
                        sonidoCarga.Activar();
                    }
                    if ((campoVision.UltimaPosicionJugador() - rb2D.position).sqrMagnitude < Monstruo.MARGEN)
                    {
                        este.CambiarEstadoMonstruo(EstadosMonstruo.Desorientado);
                    }
                    MoverseHacia(detectorParedes.EvitarColision(campoVision.UltimaPosicionJugador()), velMovPerseguir);
                    break;
                case EstadosMonstruo.VolviendoARuta:
                    if (!andando)
                    {
                        sonidoPasos.Activar();
                        andando = !andando;
                        sonidoRespiracion.Desactivar();
                    }
                    MoverseHacia(detectorParedes.EvitarColision(detectorRuta.PosicionPuntoRuta()), velMovRuta);
                    break;
                case EstadosMonstruo.Desorientado:
                    Pararse();
                    if (andando)
                    {
                        sonidoPasos.Desactivar();
                        andando = !andando;
                        cargando = false;
                        sonidoRespiracion.Activar();
                    }
                    giroInicial = rb2D.rotation;
                    cronometro = Time.time;
                    este.CambiarEstadoMonstruo(EstadosMonstruo.BuscandoJugador);
                    break;
                case EstadosMonstruo.BuscandoJugador:
                    if (andando)
                    {
                        sonidoPasos.Desactivar();
                        andando = !andando;
                        cargando = false;
                        sonidoRespiracion.Activar();
                    }
                    Pararse();
                    Vector2 aux = new Vector2(Mathf.Sin(((Time.time - cronometro) / periodoGiro + giroInicial / 360f) * 2 * Mathf.PI), Mathf.Cos(((Time.time - cronometro) / periodoGiro + giroInicial / 360f) * 2 * Mathf.PI));
                    GiroInstantaneo(aux);
                    if (Time.time - cronometro > periodoGiro)
                    {
                        este.CambiarEstadoMonstruo(EstadosMonstruo.PensandoRuta);
                    }
                    break;
                case EstadosMonstruo.Proyectado:
                    if (Time.time - cronometro > tiempoAturdimiento)
                        este.CambiarEstadoMonstruo(EstadosMonstruo.Desorientado);
                    break;
                case EstadosMonstruo.Huyendo:
                    MoverseHacia((2 * rb2D.position - jugadorRB.position), velMovHuida);
                    GameManager.instance.MontruoHuye(TipoMonstruo.Panzudo);
                    GetComponent<Collider2D>().enabled = false;
                    Destroy(gameObject, 10f);
                    break;
                case EstadosMonstruo.Atacando:
                    MoverseHacia(jugadorRB.position, velMovPerseguir);
                    break;
            }
        };

        este.Morir = () =>
        {
            animador.SetTrigger("muriendo");
            sonidoPasos.Destruir();
            sonidoCarga.Destruir();
            sonidoRespiracion.Destruir();
            GameManager.instance.MonstruoMuerto(TipoMonstruo.Panzudo);
            Destroy(detectorGolpes.gameObject);
            este.enabled = false;
            rb2D.Sleep();
            GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject, 5f);
        };

        este.Atacar = () =>
        { 
            animador.SetTrigger("atacando");
        };

        este.Atacado = (Jugador a) =>
        {
            Empujar(a.transform.position, a.fuerzaEmpujon);
            vida.Danyar(a.Danyo(), TipoMonstruo.Panzudo);
            este.CambiarEstadoMonstruo(EstadosMonstruo.Proyectado);
        };

        este.FinalAtaque = () =>
        {
            este.CambiarEstadoMonstruo(EstadosMonstruo.Desorientado);
        };

    }
    void Empujar(Vector2 origen, float velocidadProyeccion)
    {
        rb2D.velocity = (rb2D.position - origen).normalized * velocidadProyeccion;
        cronometro = Time.time;
        
    }

    //Seguir al jugador, moverse por la ruta y volver a la ruta.
    void MoverseHacia(Vector2 dir, float vel)
    {
        try
        {
            rb2D.velocity = (dir - (Vector2)transform.position).normalized * vel;
        }
        catch
        {
            rb2D.velocity = Vector2.zero;
        }

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

    void Pararse()
    {
        rb2D.velocity = Vector2.zero;
    }
}
