using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Colisiones;

[RequireComponent(typeof(Interactuable))]
public class Droga : MonoBehaviour {
    Interactuable master;
    public float distanciaDeInteraccion;
    LayerMask conQueColisiona;
    private void Start()
    {
        conQueColisiona = Colision.CapasInteraccion();
        master = GetComponent<Interactuable>();
        master.Accion = (Jugador a) =>
        {

                GameManager.instance.ConsumirDroga();
                DrogaConsumida();
        };
        master.EsPosibleLaInteraccion = (Jugador a) =>
        {
            return master.InteraccionPorLineaDeVision(a.transform, transform, distanciaDeInteraccion, conQueColisiona);
        };
        master.DistanciaDeInteraccion = () => { return distanciaDeInteraccion; };
    }

    void DrogaConsumida()
    {
        Destroy(gameObject);
    }
}
