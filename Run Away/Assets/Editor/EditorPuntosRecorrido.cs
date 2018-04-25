using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;
using System.Linq;

[CustomEditor(typeof(PadrePuntosRecorrido))]
public class EditorPuntosRecorrido : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Crear Red"))
        {
            Puerta[] puertas = GameObject.FindObjectsOfType<Puerta>();
            for (int i = 0; i < puertas.Length; i++)
                puertas[i].gameObject.SetActive(false);
            PadrePuntosRecorrido puntosRecorrido = (PadrePuntosRecorrido)target;
            var tempList = puntosRecorrido.transform.Cast<Transform>().ToList();
            foreach (var child in tempList)
            {
                DestroyImmediate(child.gameObject);
            }
            puntosRecorrido.Inicializar();
            puntosRecorrido.CrearEnCentroPatron((GameObject)Resources.Load("Punto Recorrido"));
            for(int i = 0; i<PadrePuntosRecorrido.matrizDePuntos.GetLength(0);i++)
            {
                for (int j = 0; j < PadrePuntosRecorrido.matrizDePuntos.GetLength(1); j++)
                {
                    PuntoRecorrido aux;
                    if (PadrePuntosRecorrido.matrizDePuntos[i, j] != null)
                    {
                        aux = PadrePuntosRecorrido.matrizDePuntos[i, j].GetComponent<PuntoRecorrido>();
                        Undo.RecordObject(aux, "Add Point");
                        EditorUtility.SetDirty(aux);
                        aux.CrearPrimerosContactos();
                    }
                }
            }

            for (int i = 0; i < puertas.Length; i++)
                puertas[i].gameObject.SetActive(true);

        }
    }
    private void OnSceneGUI()
    {
        DetectarRuta punto = target as DetectarRuta;

    }
}
