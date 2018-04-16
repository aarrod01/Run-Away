using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PuntoRecorrido))]
public class EditorPuntosRecorrido : Editor {

    private void OnSceneGUI()
    {
        PuntoRecorrido punto = target as PuntoRecorrido;
        punto.caja = punto.GetComponent<Collider2D>();
    }
}
