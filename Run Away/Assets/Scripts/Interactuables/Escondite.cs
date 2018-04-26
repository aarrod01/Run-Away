using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Interactuable))]

public class Escondite: MonoBehaviour {
    ColisionArmario armario;
    Interactuable master;
    public float distanciaDeInteraccion=1f;
    LayerMask conQueColisiona;
	public Transform posicionSalida;
    public Transform posicionDentro;
    // Use this for initialization
    void Start () {

        armario = GetComponentInChildren<ColisionArmario>();
        conQueColisiona = LayerMask.GetMask("Obstaculos", "Jugador");
        master = GetComponent<Interactuable>();
        master.Accion = (Jugador a) => {
            
            if (!a.Invisible())
            {
                armario.Contiene(true);
                a.Esconderse(true,posicionDentro.position);
                a.Interactuar();
                
            }
            
            else
            {
                armario.Contiene(false);
                a.Esconderse(false, posicionSalida.position);
            }
        };
        master.EsPosibleLaInteraccion = (Jugador a) =>
        {
            return master.InteraccionPorLineaDeVision(a.transform, distanciaDeInteraccion, conQueColisiona);
        };
        master.DistanciaDeInteraccion = () => { return distanciaDeInteraccion; };
    }
	
}
