using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactuable))]
public class Droga : MonoBehaviour {
    Interactuable master;
    public float distanciaDeInteraccion;
    LayerMask conQueColisiona;
    private void Start()
    {
        conQueColisiona = LayerMask.GetMask("Obstaculos", "Jugador");
        master = GetComponent<Interactuable>();
        master.Accion = (Jugador a) =>
        {
                DrogaConsumida();
        };
        master.EsPosibleLaInteraccion = (Jugador a) =>
        {
            return master.InteraccionPorLineaDeVision(a.transform, distanciaDeInteraccion, conQueColisiona);
        };
        master.DistanciaDeInteraccion = () => { return distanciaDeInteraccion; };
    }

    void DrogaConsumida()
    {
        GameManager.instance.ConsumirDroga();
        Destroy(gameObject);
    }
}
