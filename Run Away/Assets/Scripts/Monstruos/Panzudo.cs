using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monstruos;

[RequireComponent(typeof(Monstruo))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Panzudo : MonoBehaviour {
    
    public AudioSource gritoCarga, pasos, respiracionBusqueda;
    bool cargando, andando;
    Monstruo este;

    public float velMovRuta, velMovPerseguir, velMovHuida, velGiro, aceleracionAngular, tiempoAturdimiento = 1f, periodoGiro = 1f;
    public float fuerza;
    public EstadosMonstruo estadoInicial;

    private void Start()
    {
        Rigidbody2D jugadorRB;
        Animator animador;
        float giroInicial = 0;
        DetectarRuta detectorRuta;
        CampoVision campoVision;
        DetectorParedes detectorParedes;
        Vida vida;
        Golpeable detectorGolpes;
        
        jugadorRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        animador = GetComponent<Animator>();
        detectorRuta = GetComponentInChildren<DetectarRuta>();
        campoVision = GetComponentInChildren<CampoVision>();
        detectorParedes = GetComponentInChildren<DetectorParedes>();
        vida = GetComponent<Vida>();
        detectorGolpes = GetComponentInChildren<Golpeable>();

        este = GetComponent<Monstruo>();
        este.Rb2D = GetComponent<Rigidbody2D>();
        este.Tipo(TipoMonstruo.Panzudo);

        este.CambiarEstadoMonstruo(estadoInicial);
        este.Comportamiento = () => {

            switch (este.EstadoMonstruoActual())
            {
                case EstadosMonstruo.EnRuta:
                    if (!andando) {
                        pasos.Play();
                        andando = true;
                        respiracionBusqueda.Stop();
                    }
                    detectorParedes.EvitarColision(detectorRuta.PosicionPuntoRuta());
                    MoverseHacia(detectorRuta.PosicionPuntoRuta(), velMovRuta);
                    break;
                case EstadosMonstruo.SiguiendoJugador:
                    if (!cargando)
                    {
                        cargando = true;
                        gritoCarga.Play();
                    }
                    if ((campoVision.UltimaPosicionJugador() - este.Rb2D.position).sqrMagnitude < Monstruo.MARGEN)
                    {
                        este.CambiarEstadoMonstruo(EstadosMonstruo.Desorientado);
                    }
                    detectorParedes.EvitarColision(campoVision.UltimaPosicionJugador());
                    MoverseHacia(campoVision.UltimaPosicionJugador(), velMovPerseguir);
                    break;
                case EstadosMonstruo.VolviendoARuta:
                    if (!andando)
                    {
                        pasos.Play();
                        andando = !andando;
                        respiracionBusqueda.Stop();
                    }
                    detectorParedes.EvitarColision(detectorRuta.PosicionPuntoRuta());
                    MoverseHacia(detectorRuta.PosicionPuntoRuta(), velMovRuta);
                    break;
                case EstadosMonstruo.Desorientado:
                    Pararse();
                    if (andando)
                    {
                        pasos.Stop();
                        andando = !andando;
                        cargando = false;
                        respiracionBusqueda.Play();
                    }
                    giroInicial = este.Rb2D.rotation;
                    este.Cronometro = Time.time;
                    este.CambiarEstadoMonstruo(EstadosMonstruo.BuscandoJugador);
                    break;
                case EstadosMonstruo.BuscandoJugador:
                    if (andando)
                    {
                        pasos.Stop();
                        andando = !andando;
                        cargando = false;
                        respiracionBusqueda.Play();
                    }
                    Pararse();
                    Vector2 aux = new Vector2(Mathf.Sin(((Time.time - este.Cronometro) / periodoGiro + giroInicial / 360f) * 2 * Mathf.PI), Mathf.Cos(((Time.time - este.Cronometro) / periodoGiro + giroInicial / 360f) * 2 * Mathf.PI));
                    GiroInstantaneo(aux);
                    if (Time.time - este.Cronometro > periodoGiro)
                    {
                        este.CambiarEstadoMonstruo(EstadosMonstruo.PensandoRuta);
                    }
                    break;
                case EstadosMonstruo.Proyectado:
                    if (Time.time - este.Cronometro > tiempoAturdimiento)
                    {
                        este.CambiarEstadoMonstruo(EstadosMonstruo.Desorientado);
                        vida.Invulnerable(false);
                    }
                    break;
                case EstadosMonstruo.Huyendo:
                    MoverseHacia((2 * este.Rb2D.position - jugadorRB.position), velMovHuida);
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
            GameManager.instance.MonstruoMuerto(TipoMonstruo.Panzudo);
            Destroy(detectorGolpes.gameObject);
            este.enabled = false;
            este.Rb2D.Sleep();
            GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject, 5f);
        };

        este.Atacar = () =>
        { 
            animador.SetTrigger("atacando");
        };

        este.Atacado = (Jugador a) =>
        {
            este.Empujar(a.transform.position, a.fuerzaEmpujon);
            vida.Danyar(a.Danyo(), TipoMonstruo.Panzudo);
            este.CambiarEstadoMonstruo(EstadosMonstruo.Proyectado);
            vida.Invulnerable(true);
        };

        este.FinalAtaque = () =>
        {
            este.CambiarEstadoMonstruo(EstadosMonstruo.Desorientado);
        };

    }

    //Seguir al jugador, moverse por la ruta y volver a la ruta.
    void MoverseHacia(Vector2 dir, float velocidadMaxima)
    {
        este.Rb2D.AddForce((dir-este.Rb2D.position).normalized * fuerza);

        if(este.Rb2D.velocity.sqrMagnitude > velocidadMaxima)
        {
            este.Rb2D.velocity = este.Rb2D.velocity.normalized * velocidadMaxima;
        }

        GiroInstantaneo(este.Rb2D.velocity);
    }

    void Giro(Vector2 dir)
    {
        if (dir != Vector2.zero)
            este.Rb2D.rotation = Mathf.LerpAngle(este.Rb2D.rotation, Mathf.Atan2(-dir.x, dir.y) * 180f / Mathf.PI, aceleracionAngular);
    }
    void GiroInstantaneo(Vector2 dir)
    {
        if (dir != Vector2.zero)
            este.Rb2D.rotation = Mathf.Atan2(-dir.x, dir.y) * 180f / Mathf.PI;
    }

    void Pararse()
    {
        este.Rb2D.velocity = Vector2.zero;
    }
}
