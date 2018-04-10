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
        conQueColisiona = Colisiones.Colision.CapaGolpeMonstruo();

        master.Accion = (Jugador a) => {
            vida.Danyar(1);
            monstruo.Empujar(a.transform.position, velocidadDeProyeccion);
           
        };
        master.EsPosibleLaInteraccion = (Jugador a) =>
        {
            Vector2 pos = transform.position;
            Vector2 posa = a.transform.position;
            RaycastHit2D hit = Physics2D.Raycast(posa, pos - posa, distanciaInteraccion, conQueColisiona);
            Debug.DrawRay(posa, pos - posa, Color.red, 5f);
            return hit.collider != null && hit.collider.GetComponent<Golpeable>()==this;
        };
        
       
    }

}
