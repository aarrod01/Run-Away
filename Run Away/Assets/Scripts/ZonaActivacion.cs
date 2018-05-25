using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ZonaActivacion : MonoBehaviour {

    public VentanaEmergente objeto;

    void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.tag == "Player"&& !objeto.Activada&&objeto!=null)
            objeto.gameObject.SetActive(true);
    }
}
