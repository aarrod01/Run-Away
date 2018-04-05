using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Interactuable))]
public class Golpeable : MonoBehaviour {
    Interactuable master;
    Rigidbody2D monstruoRB;
    MonsterMovement monstruo;
    Health vida;
    public float distanciaInteraccion = 1f;
    public float velocidadDeProyeccion;
    public LayerMask conQueColisiona;
    // Use this for initialization
    void Start()
    {
        master = GetComponent<Interactuable>();
        monstruoRB = GetComponentInParent<Rigidbody2D>();
        monstruo = GetComponentInParent<MonsterMovement>();
        vida= GetComponentInParent<Health>();
        master.Click = (PlayerMovement a) => {
            Vector2 pos = transform.position;
            Vector2 posa = a.transform.position;
            RaycastHit2D hit = Physics2D.Raycast(posa, pos-posa, distanciaInteraccion, conQueColisiona);
            Debug.DrawRay(posa, pos-posa,Color.red,5f);
            if (hit.collider != null && hit.collider.tag == "PuntoVulnerable")
            {
                vida.Danyar(1);
                monstruo.Empujar(posa, velocidadDeProyeccion);
            }
        };
        
       
    }

}
