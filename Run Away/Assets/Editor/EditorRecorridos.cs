﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PoligonoRecorrido))]
public class InstpectorDeRectas : Editor
{
    public GUIStyle estilo;
    PuntoRecorrido[] todosLosPuntos;

    void OnEnable()
    {
        estilo = new GUIStyle();
        estilo.fontSize = 20;
        estilo.richText = true;
        todosLosPuntos = GameObject.FindObjectsOfType<PuntoRecorrido>();
    }

    private void OnSceneGUI()
    {
        PoligonoRecorrido recorrido = target as PoligonoRecorrido;
        DetectarRuta detector = recorrido.GetComponent<DetectarRuta>();
         
        recorrido.transform.position = (Vector2)recorrido.transform.position;
        Transform agarraderaTransform = recorrido.transform;
        Quaternion agarraderaDeRotacion = Tools.pivotRotation == PivotRotation.Local ? agarraderaTransform.rotation : Quaternion.identity;
        Vector2[] puntos = new Vector2[recorrido.numeroDePuntos];
        if (recorrido.Puntos() == null || recorrido.numeroDePuntos != recorrido.Puntos().Length)
        {
            recorrido.CrearPuntos(recorrido.numeroDePuntos);

        }

        for (int i = 0; i < recorrido.numeroDePuntos; i++)
        {
            puntos[i] = agarraderaTransform.TransformPoint(recorrido.Puntos()[i]);
        }
        
        for (int i = 0; i < puntos.Length; i++)
        {
            Handles.color = Color.white;
            Handles.DrawLine(puntos[i], puntos[(i+1) % puntos.Length]);
            Vector2 distancia = puntos[(i + 1) % puntos.Length] - puntos[i];
            Handles.ArrowHandleCap(0, puntos[i], 
                Quaternion.LookRotation( distancia),
                3, 
                EventType.Repaint);
            
            string aux = "<color=red>" + i+ "</color>";
            Handles.Label(puntos[i], aux,estilo);
            EditorGUI.BeginChangeCheck();
            puntos[i]=Handles.PositionHandle(puntos[i], agarraderaDeRotacion);
            if (EditorGUI.EndChangeCheck())
            {

                Undo.RecordObject(recorrido, "Move Point");
                EditorUtility.SetDirty(recorrido);
                recorrido.Puntos()[i] = agarraderaTransform.InverseTransformPoint(puntos[i]);
            }

        }



    }
}

