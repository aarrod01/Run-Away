using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

[CustomEditor(typeof(PoligonoRecorrido))]
public class EditorRecorrido : Editor
{
    public GUIStyle estilo;
    PuntoRecorrido[] todosLosPuntos;

    void OnEnable()
    {
        estilo = new GUIStyle();
        estilo.fontSize = 20;
        estilo.richText = true;
        todosLosPuntos = GameObject.FindObjectsOfType<PuntoRecorrido>();
        ((PoligonoRecorrido)target).Detector(((PoligonoRecorrido)target).GetComponent<DetectarRuta>());
    }

    private void OnSceneGUI()
    {
        PoligonoRecorrido recorrido = target as PoligonoRecorrido;
        DetectarRuta detector = recorrido.GetComponent<DetectarRuta>();
        Tilemap mapa = GameObject.FindGameObjectWithTag("Pared").GetComponent<Tilemap>();
        PadrePuntosRecorrido padrePuntos = GameObject.FindObjectOfType<PadrePuntosRecorrido>().GetComponent<PadrePuntosRecorrido>();
        //Determinar la gradilla
        BoundsInt limite = mapa.cellBounds;
        Vector2 posicionSuperiorIzquierda = mapa.LocalToWorld(new Vector2(mapa.localBounds.min.x, mapa.localBounds.min.y));
        float ancho = mapa.LocalToWorld(mapa.cellSize).x;
        Vector2 vectorDesdeEsquinaSuperiorIzquierdaACentro = mapa.LocalToWorld((Vector2.right + Vector2.up) * ancho);
        TileBase[] casillas = mapa.GetTilesBlock(limite);

        //Supresion movimiento en z.
        recorrido.transform.position = (Vector2)recorrido.transform.position;

        Transform agarraderaTransform = recorrido.transform;
        Quaternion agarraderaDeRotacion = Tools.pivotRotation == PivotRotation.Local ? agarraderaTransform.rotation : Quaternion.identity;

        //Creaccion de los puntos
        Vector2[] puntos = new Vector2[recorrido.numeroDePuntos];
        if (recorrido.Puntos() == null || recorrido.numeroDePuntos != recorrido.Puntos().Length)
        {
            recorrido.CrearPuntos(recorrido.numeroDePuntos);
        }
        
        for (int i = 0; i < recorrido.numeroDePuntos; i++)
        {
            //posicionar puntos con respecto w
            Vector3 aux1 = recorrido.puntos[i];
            Vector3Int aux = mapa.WorldToCell(agarraderaTransform.TransformPoint(aux1));
            aux = buscarPuntoRutaEnCeldaMasCercana(aux, PadrePuntosRecorrido.matrizDePuntos);
            puntos[i] = mapa.CellToWorld(aux);
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
        
        for (int i = 0; i < recorrido.numeroDePuntos; i++)
        {
            Vector2Int aux = Vector2Int.FloorToInt((puntos[i] - posicionSuperiorIzquierda) / ancho);

        }
    }
    Vector2Int Vector3IntToVector2Int(Vector3Int a)
    {
        return new Vector2Int(a.x, a.y);
    }
    Vector3Int buscarPuntoRutaEnCeldaMasCercana(Vector3Int a, GameObject[,] padre)
    {
        int anchoSubmatriz = 0;//0 igual a matriz de 1 elemento
        a=new Vector3Int(Mathf.Max(0, a.x), Mathf.Max(0, a.y),0);
        while (padre[a.x, a.y] == null)
        {
            for (int i = Mathf.Max(a.x - anchoSubmatriz, 0); i < Mathf.Min(a.x + anchoSubmatriz, padre.GetLength(0)); i++)
            {
                for (int j = Mathf.Max(a.y - anchoSubmatriz, 0); j < Mathf.Min(a.y + anchoSubmatriz, padre.GetLength(1)); j++)
                {
                    if (padre[i, j] != null)
                        return new Vector3Int(i, j, 0);
                }
            }
            anchoSubmatriz++;
        }
        return a;
    }


}

