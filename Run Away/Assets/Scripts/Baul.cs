using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactuable))]

public class Baul : MonoBehaviour {
    Interactuable master;
    public float distanciaInteraccion=1f;
    public LayerMask conQueColisiona;
	public Transform posicionSalida;
    // Use this for initialization
    void Start () {
        master = GetComponent<Interactuable>();
        master.Click = (PlayerMovement a) => {
            GetComponent<Collider2D>().enabled = false;
            Vector3 pos = transform.position;
            RaycastHit2D hit = Physics2D.Raycast(pos,a.transform.position- pos, distanciaInteraccion, conQueColisiona);
            if (hit.collider != null && hit.collider.tag == "Player")
            {
                if (!a.Invisible())
                {
                    a.Parar();
                    a.Invisible(true);
                    a.MovimientoLibre(false);
                    a.transform.position = transform.position;
                    a.GetComponent<Rigidbody2D>().Sleep();
                    a.GetComponent<Collider2D>().enabled = false;
                    a.GetComponent<SpriteRenderer>().enabled = false;
                    a.ApagarLuz();
                }
            }
            else if (a.Invisible())
            {
                a.Invisible(false);
                a.MovimientoLibre(true);
                a.GetComponent<Rigidbody2D>().position = posicionSalida.position;
                a.GetComponent<Collider2D>().enabled = true;
                a.GetComponent<SpriteRenderer>().enabled = true;
                a.EncenderLuz();
            }
            GetComponent<Collider2D>().enabled = true;
        };
	}
	
}
