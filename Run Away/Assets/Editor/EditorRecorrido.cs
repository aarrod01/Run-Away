using System.Collections;
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
            Undo.RecordObject(target, "Move Point");
            EditorUtility.SetDirty(target);
            puntos[i] = recorrido.Puntos()[i];
        }


        PuntoRecorrido[] puntosRecorrido = GameObject.FindObjectsOfType<PuntoRecorrido>();
        PuntoRecorrido[] puntosAuxiliares = new PuntoRecorrido[puntosRecorrido.Length];
        int j = 0;
        for (int i = 0; i < puntos.Length; i++)
        {
            Handles.color = Color.white;
            Handles.DrawLine(puntos[i], puntos[(i + 1) % puntos.Length]);
            Vector2 distancia = puntos[(i + 1) % puntos.Length] - puntos[i];
            Handles.ArrowHandleCap(0, puntos[i],
                Quaternion.LookRotation(distancia),
                3,
                EventType.Repaint);

            string aux = "<color=red>" + i + "</color>";
            Handles.Label(puntos[i], aux, estilo);
            EditorGUI.BeginChangeCheck();
            puntos[i] = Handles.FreeMoveHandle(i, puntos[i], agarraderaDeRotacion, 0.5f, Vector3.one*0.1f, DibujarAgarraderaPunto);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(detector, "Move Point");
                EditorUtility.SetDirty(detector);
                recorrido.Puntos()[i] = puntos[i];
            }
            else
            {
                Undo.RecordObject(detector, "Move Point");
                EditorUtility.SetDirty(detector);
                puntosAuxiliares[j] = AjustarseAPuntoRecorrido(puntos[i], puntosRecorrido);
                recorrido.Puntos()[i] = puntosAuxiliares[j].transform.position;
                j++;
                DibujarAgarraderaPunto(i, puntos[i], agarraderaDeRotacion, 0.5f, EventType.Repaint);
            }
        }

        PuntoRecorrido[] puntosFinales = new PuntoRecorrido[j];
        for (int i = 0; i < j; i++)
            puntosFinales[i] = puntosAuxiliares[i];

        detector.CrearPuntosRuta(puntosFinales);

    }

    void DibujarAgarraderaPunto(int controlID, Vector3 position, Quaternion rotation, float size, EventType eventType)
    {
        Handles.CircleHandleCap(controlID, position, rotation, size, eventType);
    }

    PuntoRecorrido AjustarseAPuntoRecorrido(Vector3 punto,PuntoRecorrido[] pR)
    {

        float distancia = float.PositiveInfinity;

        PuntoRecorrido v = null;
        for (int i=0; i<pR.Length; i++)
        {
            float aux= (punto - pR[i].transform.position).sqrMagnitude;
            if(aux<distancia)
            {
                distancia = aux;
                v = pR[i];
            }
        }
        return v;
    }
}