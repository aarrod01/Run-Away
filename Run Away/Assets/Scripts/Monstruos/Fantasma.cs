using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monstruos;

public delegate void funcionLuz(GameObject caster);

[RequireComponent(typeof(Monstruo))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class Fantasma : MonoBehaviour
{
    public EstadosMonstruo estadoInicial;
    public float tiempoAturdimiento = 1f;
    public float velocidadPersecucion;
    public float velocidadHuida;
    public float cabreoMaximo;
    public float cabreoUmbral;
    public float tasaAumentoDeCabreo;
    public float tasaDescensoDeCabreo;
    public AudioSource audioGrito;

    Monstruo este;

    // Use this for initialization
    void Start ()
    {
        Animator animador = GetComponent<Animator>();
        Vida vida = GetComponent<Vida>();
        DetectarRuta detectorRuta = GetComponent<DetectarRuta>();
        GeneradoOndas generadorOndas = GetComponentInChildren<GeneradoOndas>();
        este = GetComponent<Monstruo>();
        este.Rb2D = GetComponent<Rigidbody2D>();
        Cabreo cabreometro = GetComponentInChildren<Cabreo>();
        Rigidbody2D jugadorP = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        cabreometro.Iniciar(cabreoMaximo, cabreoUmbral, tasaAumentoDeCabreo, tasaDescensoDeCabreo);
        este.Tipo(TipoMonstruo.Fantasma);

        este.CambiarEstadoMonstruo(estadoInicial);
        este.Comportamiento = () => {
            audioGrito.volume = cabreometro.Nivel();
            
            este.Rb2D.isKinematic = !(este.EstadoMonstruoActual() == EstadosMonstruo.SiguiendoJugador || este.EstadoMonstruoActual() == EstadosMonstruo.Proyectado);

            switch (este.EstadoMonstruoActual())
            {
                case EstadosMonstruo.Huyendo:
                    vida.Invulnerable(true);
                    generadorOndas.GenerarOndas();
                    MoverseHacia((2 * este.Rb2D.position - cabreometro.JugadorRB().position), velocidadHuida);
                    GameManager.instance.MontruoHuye(TipoMonstruo.Fantasma);
                    GetComponent<Collider2D>().enabled = false;
                    audioGrito.Stop();
                    Destroy(gameObject, 10f);
                    break;
                case EstadosMonstruo.Proyectado:
                    vida.Invulnerable(true);
                    if (Time.time - este.Cronometro > tiempoAturdimiento)
                        este.CambiarEstadoMonstruo(EstadosMonstruo.Quieto);
                    break;
                default:
                    este.CambiarEstadoMonstruo(cabreometro.CambioCabreo());
                    switch (este.EstadoMonstruoActual())
                    {
                        case EstadosMonstruo.Quieto:
                            Parar();
                            vida.Invulnerable(true);
                            generadorOndas.PararOndas();
                            break;

                        case EstadosMonstruo.SiguiendoJugador:
                            vida.Invulnerable(false);
                            MoverseHacia(cabreometro.JugadorRB().position, velocidadPersecucion);
                            generadorOndas.GenerarOndas();
                            break;
                    }
                    break;
            }
        };

        audioGrito.Play();
        audioGrito.volume = 0f;
        este.Morir = () =>
        {
            generadorOndas.enabled = false;
            animador.SetTrigger("muriendo");
            GameManager.instance.MonstruoMuerto(TipoMonstruo.Fantasma);
            este.enabled = false;
            este.Rb2D.Sleep();
            este.Rb2D.simulated = false;
            GetComponent<Collider2D>().enabled = false;
            audioGrito.Stop();
            Destroy(gameObject, 5f);
        };

        este.Atacar = () =>
        {
            animador.SetTrigger("atacando");
            vida.Invulnerable(false);
        };

        este.Atacado = (Jugador a) =>
        {
            if (este.EstadoMonstruoActual() == EstadosMonstruo.SiguiendoJugador)
            {
                este.Empujar(a.transform.position, a.fuerzaEmpujon);
                vida.Danyar(a.Danyo(), TipoMonstruo.Panzudo);
                este.CambiarEstadoMonstruo(EstadosMonstruo.Proyectado);
                cabreometro.Tranquilizar();
            }
        };

        este.FinalAtaque = () =>
        {
            vida.Invulnerable(true);
        };

        este.EntrandoLuz = () =>
        {
            generadorOndas.SumarLuz();
        };

        este.SaliendoLuz = () =>
        {
            generadorOndas.RestarLuz();
        };
    }

    void MoverseHacia(Vector2 dir, float vel)
    {
        try
        {
            este.Rb2D.velocity = (dir - este.Rb2D.position).normalized * vel;
        }
        catch
        {
            este.Rb2D.velocity = Vector2.zero;
        }
        GiroInstantaneo(este.Rb2D.velocity);
    }

    void GiroInstantaneo(Vector2 dir)
    {
        if (dir != Vector2.zero)
            este.Rb2D.rotation = Mathf.Atan2(-dir.x, dir.y) * 180f / Mathf.PI;
    }

    void Parar()
    {
        este.Rb2D.velocity = Vector2.zero;
    }

    
}
