using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactuable))]

public class Baul : MonoBehaviour {
    Interactuable master;
    public float distanciaInteraccion=1f;
    public LayerMask conQueColisiona;
    // Use this for initialization
    void Start () {
        master = GetComponent<Interactuable>();
        master.Click = (PlayerMovement a) => {
            GetComponent<Collider2D>().enabled = false;
            RaycastHit2D hit = Physics2D.Raycast(transform.position,a.transform.position- transform.position, distanciaInteraccion, conQueColisiona);
            if(hit.collider.tag=="Player")
                if (!a.Invisible())
                {
                    a.Parar();
                    a.Invisible(true);
                    a.MovimientoLibre(false);
                    a.GetComponent<Rigidbody2D>().position = transform.position;
                    a.GetComponent<SpriteRenderer>().enabled=false;
                }
                else
                {
                    a.Invisible(false);
                    a.MovimientoLibre(true);
                    a.GetComponent<Rigidbody2D>().position = (Vector2)transform.position+Vector2.down;
                    a.GetComponent<SpriteRenderer>().enabled = true;
                }
                GetComponent<Collider2D>().enabled = true;
        };
	}
	
}
