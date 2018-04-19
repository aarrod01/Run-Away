using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DetectarRuta))]
public class EditorDetectarRuta : Editor {

    PuntoRecorrido todosLosPuntos;

    private void OnEnable()
    {
        todosLosPuntos = GameObject.FindObjectOfType<PuntoRecorrido>();
    }

    private void OnSceneGUI()
    {
        DetectarRuta punto = target as DetectarRuta;

    }
}
