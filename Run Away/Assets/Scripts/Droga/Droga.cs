using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactuable))]
public class Droga : MonoBehaviour {
    Interactuable master;
    public float distanciaInteraccion;
    public LayerMask conQueColisiona;
    private void Start()
    {
        master = GetComponent<Interactuable>();
        master.Click = (PlayerMovement a) =>
        {
            RaycastHit2D hit = Physics2D.Raycast(a.transform.position, transform.position - a.transform.position, distanciaInteraccion, conQueColisiona);
            if (((Vector2)(transform.position - a.transform.position)).sqrMagnitude <= distanciaInteraccion
            && hit.collider != null && hit.collider.gameObject == gameObject)
            {
                GameManager.instance.ConsumirDroga();
                DrogaConsumida();
            }
        };
    }

    void DrogaConsumida()
    {
        Destroy(gameObject);
    }
}
