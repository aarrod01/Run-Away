using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Colisiones;


public delegate void funcionInteractuado(Jugador a);
public delegate bool funcionPregunta(Jugador a);
public delegate float funcionDistancia();
[RequireComponent(typeof(Collider2D))]
public class Interactuable : MonoBehaviour {
    public funcionInteractuado Accion = (Jugador a)=> { };
    public funcionPregunta EsPosibleLaInteraccion = (Jugador a) => { return false; };
    public funcionDistancia DistanciaDeInteraccion = () => { return -1f; };

    //Tipos de Condiciones generales.
    public bool InteraccionPorLineaDeVision(Transform desde, Transform hasta, float distanciaInteraccion ,LayerMask capasInteraccion)
    {
        Vector3 pos = desde.position;
        RaycastHit2D hit = Physics2D.Raycast(pos, hasta.position - pos, distanciaInteraccion, capasInteraccion);
        return (hit.collider != null && hit.collider.tag == "Player")||hasta.GetComponent<Collider2D>().bounds.Contains(desde.position);
    }


}
