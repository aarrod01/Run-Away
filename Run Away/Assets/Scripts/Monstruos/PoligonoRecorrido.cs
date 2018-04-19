using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(DetectarRuta))]
[System.Serializable]
public class PoligonoRecorrido : MonoBehaviour {

    [SerializeField]
    public uint numeroDePuntos;

    [SerializeField]
    public Vector2[] puntos;
    DetectarRuta detector;

    public void Detector(DetectarRuta de)
    {
        detector = de;
    }

    public DetectarRuta Detector()
    {
        return detector;
    }
    void Start()
    {
        Destroy(this);
    }

    public void CrearPuntos(uint n)
    {
        if (puntos == null)
        {
            puntos = new Vector2[n];
            for (int i = 0; i < n; i++)
                puntos[i] = Vector2.right;
        }
        else
        {
            Vector2[] aux = new Vector2[n];
            for (int i = 0; i < puntos.Length && i<n; i++)
                aux[i] = puntos[i];
            for (int i = puntos.Length; i < n; i++)
                aux[i] = Vector2.right;
            puntos = new Vector2[n];
            for (int i = 0; i < n; i++)
                puntos[i] = aux[i];
        }
    }

    public Vector2[] Puntos()
    {
        return puntos;
    }
}
