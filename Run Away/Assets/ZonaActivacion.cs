using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ZonaActivacion : MonoBehaviour {

    public GameObject objeto;

    void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.tag == "Player")
            objeto.SetActive(true);
    }
}
