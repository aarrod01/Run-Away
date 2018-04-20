using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monstruos;

[RequireComponent(typeof(Rigidbody2D))]

public class Fantasma : MonoBehaviour
{
    const float MARGENESCUCHA = 0.005f;

    public EstadosMonstruo estadoMonstruo;
    public TipoMonstruo tipo;
    public float velocidadPersecucion;
    public float velocidadHuida;
    public float cabreoMaximo;
    public float cabreoUmbral;
    public float tasaAumentoDeCabreo;
    public float tasaDescensoDeCabreo;

    Rigidbody2D fantasmaRB;
    public float cabreo;
    Rigidbody2D jugadorRB;

    // Use this for initialization
    void Start () {
        fantasmaRB = GetComponent<Rigidbody2D>();
        cabreo = 0f;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        CambioCabreo();
        switch(estadoMonstruo)
        {
            case EstadosMonstruo.SiguiendoJugador:
                MoverseHacia(jugadorRB.position, velocidadPersecucion);
                break;
            case EstadosMonstruo.Quieto:
                Parar();
                break;
            case EstadosMonstruo.Huyendo:
                MoverseHacia((2 * fantasmaRB.position - jugadorRB.position), velocidadHuida);
                GameManager.instance.MontruoHuye(tipo);
                GetComponent<Collider2D>().enabled = false;
                Destroy(gameObject, 10f);
                Destroy(this);
                break;
        }
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

    void CambioCabreo()
    {
        if(jugadorRB != null&&jugadorRB.velocity.sqrMagnitude>MARGENESCUCHA)
            cabreo = Mathf.Min(cabreo+tasaAumentoDeCabreo*Time.fixedDeltaTime, cabreoMaximo);
        else
            cabreo = Mathf.Max(cabreo - tasaDescensoDeCabreo * Time.fixedDeltaTime, 0f);

        if (cabreo >= cabreoUmbral)
            estadoMonstruo = EstadosMonstruo.SiguiendoJugador;
        else
            estadoMonstruo = EstadosMonstruo.Quieto;
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
