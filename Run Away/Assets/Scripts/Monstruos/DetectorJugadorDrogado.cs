using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CircleCollider2D))]
public class DetectorJugadorDrogado : MonoBehaviour
{

    LayerMask conQueColisiona;

    private void Start()
    {
        conQueColisiona = LayerMask.GetMask("Obstaculos", "Jugador");
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, other.transform.position - transform.position, Mathf.Infinity, conQueColisiona);
            if (hit.collider.gameObject == other.gameObject && GameManager.instance.Drogado())
            {
                Monstruo aux = GetComponentInParent<Monstruo>();
                GameManager.instance.MontruoHuye(aux.tipo);
                aux.CambiarEstadoMonstruo(Monstruos.EstadosMonstruo.Huyendo);
            }
        }
    }


}
