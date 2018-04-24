using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class GolpeJugador : MonoBehaviour {

    float retardo, duracion;
    int danyo;

    public void Iniciar()
    {
        gameObject.SetActive(false);
    }

    public void Golpear(float _retardo, float _duracion, int _danyo)
    {
        danyo = _danyo;
        retardo = _retardo;
        duracion = _duracion;
        Invoke("Inicio", retardo);
    }

    void Inicio()
    {
        gameObject.SetActive(true);
        Invoke("Fin", duracion);
    }

    void Fin()
    {
        gameObject.SetActive(false);
    }

	void OnTriggerEnter2D(Collider2D otro)
    {
        if(otro.GetComponent<PuntoVulnerable>()!=null)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, otro.transform.position - transform.position, Mathf.Infinity, LayerMask.GetMask("Obstaculos","PuntoVulnerable", "PuntoInvulnerable"));
            if(hit.collider==otro)
            {
                otro.GetComponent<PuntoVulnerable>().Danyar(danyo);
                otro.GetComponentInParent<Monstruo>().Empujar(transform.position, GetComponentInParent<Jugador>().fuerzaEmpujon);
                Fin();
            }
        }
    }
}
