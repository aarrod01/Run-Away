using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monstruos;

public class Cabreo : MonoBehaviour {

    Rigidbody2D jugadorRB = null;

    float cabreoMaximo;
    float cabreoUmbral;
    float tasaAumentoDeCabreo;
    float tasaDescensoDeCabreo;
    float cabreo;

    const float MARGENESCUCHA = 0.005f;

    public void Iniciar(float _cabreoMaximo, float _cabreoUmbral, float _tasaAumentoDeCabreo, float _tasaDescensoDeCabreo)
    {
        cabreoMaximo = _cabreoMaximo;
        cabreoUmbral = _cabreoUmbral;
        tasaAumentoDeCabreo = _tasaAumentoDeCabreo;
        tasaDescensoDeCabreo = _tasaDescensoDeCabreo;
    }

    public EstadosMonstruo CambioCabreo()
    {
        if (jugadorRB != null && jugadorRB.velocity.sqrMagnitude > MARGENESCUCHA)
            cabreo = Mathf.Min(cabreo + tasaAumentoDeCabreo * Time.fixedDeltaTime, cabreoMaximo);
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

    public Rigidbody2D JugadorRB()
    {
        return jugadorRB;
    }

    public float Nivel()
    {
        return cabreo/cabreoMaximo;
    }

}
