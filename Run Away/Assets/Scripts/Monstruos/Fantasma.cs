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
    public AudioSource grito;
    public EstadosMonstruo estadoInicial;
    public float velocidadPersecucion;
    public float velocidadHuida;
    public float cabreoMaximo;
    public float cabreoUmbral;
    public float tasaAumentoDeCabreo;
    public float tasaDescensoDeCabreo;


    Rigidbody2D fantasmaRB;

    // Use this for initialization
    void Start ()
    {
        fantasmaRB = GetComponent<Rigidbody2D>();
        Animator animador = GetComponent<Animator>();
        Vida vida = GetComponent<Vida>();
        DetectarRuta detectorRuta = GetComponent<DetectarRuta>();
        GeneradoOndas generadorOndas = GetComponentInChildren<GeneradoOndas>();
        Monstruo este = GetComponent<Monstruo>();
        Cabreo cabreometro = GetComponentInChildren<Cabreo>();
        Rigidbody2D jugadorP = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        cabreometro.Iniciar(cabreoMaximo, cabreoUmbral, tasaAumentoDeCabreo, tasaDescensoDeCabreo);
        este.Tipo(TipoMonstruo.Fantasma);

        este.CambiarEstadoMonstruo(estadoInicial);
        este.Comportamiento = () => {
            grito.volume = cabreometro.Nivel() / (fantasmaRB.position - jugadorP.position).sqrMagnitude;
            switch (este.EstadoMonstruoActual())
            {
                case EstadosMonstruo.Huyendo:
                    generadorOndas.GenerarOndas();
                    MoverseHacia((2 * fantasmaRB.position - cabreometro.JugadorRB().position), velocidadHuida);
                    GameManager.instance.MontruoHuye(TipoMonstruo.Fantasma);
                    GetComponent<Collider2D>().enabled = false;
                    Destroy(gameObject, 10f);
                    break;
                default:
                    este.CambiarEstadoMonstruo(cabreometro.CambioCabreo());
                    switch (este.EstadoMonstruoActual())
                    {
                        case EstadosMonstruo.Quieto:
                            Parar();
                            generadorOndas.PararOndas();
                            break;

                        case EstadosMonstruo.SiguiendoJugador:
                            MoverseHacia(cabreometro.JugadorRB().position, velocidadPersecucion);
                            generadorOndas.GenerarOndas();
                            break;

                    }
                    break;
            }
        };

        grito.Play();

        este.Morir = () =>
        {
            generadorOndas.enabled = false;
            animador.SetTrigger("muriendo");
            GameManager.instance.MonstruoMuerto(TipoMonstruo.Fantasma);
            este.enabled = false;
            fantasmaRB.Sleep();
            fantasmaRB.simulated = false;
            GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject, 5f);
        };

        este.Atacar = () =>
        {
            animador.SetTrigger("atacando");
            vida.Invulnerable(false);
        };

        este.Atacado = (Jugador a) =>
        {
            vida.Danyar(a.Danyo(), TipoMonstruo.Panzudo);
            este.CambiarEstadoMonstruo(EstadosMonstruo.Proyectado);
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
            fantasmaRB.velocity = (dir - fantasmaRB.position).normalized * vel;
        }
        catch
        {
            fantasmaRB.velocity = Vector2.zero;
        }
        GiroInstantaneo(fantasmaRB.velocity);
    }

    void GiroInstantaneo(Vector2 dir)
    {
        if (dir != Vector2.zero)
            fantasmaRB.rotation = Mathf.Atan2(-dir.x, dir.y) * 180f / Mathf.PI;
    }

    void Parar()
    {
        fantasmaRB.velocity = Vector2.zero;
    }

    
}
