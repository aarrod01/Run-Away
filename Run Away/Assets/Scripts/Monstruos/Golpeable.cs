using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Interactuable))]
public class Golpeable : MonoBehaviour {

    Interactuable master;
    Rigidbody2D monstruoRB;
    Monstruo monstruo;
    Vida vida;
    public float distanciaInteraccion = 1f;
    public float velocidadDeProyeccion;
    LayerMask conQueColisiona;
    // Use this for initialization
    void Start()
    {
        master = GetComponent<Interactuable>();
        monstruoRB = GetComponentInParent<Rigidbody2D>();
        monstruo = GetComponentInParent<Monstruo>();
        vida = GetComponentInParent<Vida>();
        conQueColisiona = LayerMask.GetMask("Obstaculos", "Jugador");

        master.Accion = (Jugador a) => {
            a.Atacar();
           
        };
        master.EsPosibleLaInteraccion = (Jugador a) =>
        {
            return master.InteraccionPorLineaDeVision(a.transform, distanciaInteraccion, conQueColisiona);
        };
        
       
    }

}
