using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monstruos;

[RequireComponent(typeof(Monstruo))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Fantasma : MonoBehaviour
{
    const float MARGENESCUCHA = 0.005f;

    public EstadosMonstruo estadoInicial;
    public float velocidadPersecucion;
    public float velocidadHuida;
    public float cabreoMaximo;
    public float cabreoUmbral;
    public float tasaAumentoDeCabreo;
    public float tasaDescensoDeCabreo;

    Rigidbody2D fantasmaRB;
    float cabreo;
    Rigidbody2D jugadorRB;

    // Use this for initialization
    void Start ()
    {
        fantasmaRB = GetComponent<Rigidbody2D>();
        Animator animador = GetComponent<Animator>();
        Vida vida = GetComponent<Vida>();
        DetectarRuta detectorRuta = GetComponent<DetectarRuta>();

        Monstruo este = GetComponent<Monstruo>();
        este.Tipo(TipoMonstruo.Fantasma);

        este.CambiarEstadoMonstruo(estadoInicial);
        este.Comportamiento = () => {
            este.CambiarEstadoMonstruo(CambioCabreo());
            switch (este.EstadoMonstruoActual())
            {
                case EstadosMonstruo.SiguiendoJugador:
                    MoverseHacia(jugadorRB.position, velocidadPersecucion);
                    break;
                case EstadosMonstruo.Quieto:
                    Parar();
                    break;
                case EstadosMonstruo.Huyendo:
                    MoverseHacia((2 * fantasmaRB.position - jugadorRB.position), velocidadHuida);
                    GameManager.instance.MontruoHuye(TipoMonstruo.Fantasma);
                    GetComponent<Collider2D>().enabled = false;
                    Destroy(gameObject, 10f);
                    Destroy(this);
                    break;
            }
        };

        este.Morir = () =>
        {
            animador.SetTrigger("muriendo");
            GameManager.instance.MonstruoMuerto(TipoMonstruo.Fantasma);
            este.enabled = false;
            fantasmaRB.Sleep();
            GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject, 5f);
        };

        este.Atacar = () =>
        {
            animador.SetTrigger("atacando");
        };

        este.Atacado = (Jugador a) =>
        {
            vida.Danyar(a.Danyo(), TipoMonstruo.Panzudo);
            este.CambiarEstadoMonstruo(EstadosMonstruo.Proyectado);
        };
    }

    void MoverseHacia(Vector2 dir, float vel)
    {
        fantasmaRB.velocity = (dir - fantasmaRB.position).normalized * vel;
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

    EstadosMonstruo CambioCabreo()
    {
        if(jugadorRB != null&&jugadorRB.velocity.sqrMagnitude > MARGENESCUCHA)
            cabreo = Mathf.Min(cabreo+tasaAumentoDeCabreo*Time.fixedDeltaTime, cabreoMaximo);
        else
            cabreo = Mathf.Max(cabreo - tasaDescensoDeCabreo * Time.fixedDeltaTime, 0f);

        if (cabreo >= cabreoUmbral)
            return EstadosMonstruo.SiguiendoJugador;
        else
            return EstadosMonstruo.Quieto;
    }
    
    void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.tag == "Player")
        {
            jugadorRB = otro.GetComponent<Rigidbody2D>();
        }
    }

    private void OnTriggerExit2D(Collider2D otro)
    {
        if (otro.tag == "Jugador")
        {
            jugadorRB = null;
        }
    }
}
