using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Interactuable))]

public class Escondite: MonoBehaviour {
    Interactuable master;
    public float distanciaDeInteraccion=1f;
    LayerMask conQueColisiona;
	public Transform posicionSalida;
    // Use this for initialization
    void Start () {
        conQueColisiona = LayerMask.GetMask("Obstaculos", "Jugador");
        master = GetComponent<Interactuable>();
        master.Accion = (Jugador a) => {
            
            if (!a.Invisible())
            {
                a.Esconderse(true);
                a.Interactuar();
                a.transform.position = transform.position;
            }
            
            else
            {
                a.Esconderse(false);
                a.GetComponent<Rigidbody2D>().position = posicionSalida.position;
            }
        };
        master.EsPosibleLaInteraccion = (Jugador a) =>
        {
            return master.InteraccionPorLineaDeVision(a.transform, distanciaDeInteraccion, conQueColisiona);
        };
        master.DistanciaDeInteraccion = () => { return distanciaDeInteraccion; };
    }
	
}
