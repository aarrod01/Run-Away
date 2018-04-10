﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monstruos;
using Recorrido;
public class CuerpoContacto : MonoBehaviour 
{
	public int numeroMonstruo;

	public int puntoRutaActual = 0;
	Monstruo monstruo;
    Rigidbody2D monstruoRB;

	public PuntoRecorrido[] ruta;
    PuntoRecorrido[] puntosRuta;
    PuntoRecorrido ultimopunto;
    const float MARGEN = 0.001f;
    public listaNodos caminoDeVuelta;

	void Start(){

		monstruo = GetComponentInParent<Monstruo> ();
        monstruoRB = monstruo.GetComponent<Rigidbody2D>();
        PuntoRecorrido[] aux = new PuntoRecorrido[ruta.Length];
        int j = 0;
        for (int i = 0; i < ruta.Length; i++)
        {
            int k = 0;
            while (k < j && ruta[i] != aux[k])
                k++;
            if (k == j)
            {
                aux[j] = ruta[i];
                j++;
            }
        }

        puntosRuta = new PuntoRecorrido[j];
        for (int i = 0; i < j; i++)
            puntosRuta[i] = aux[i];

    }


    private void Update()
    {
        switch (monstruo.EstadoMonstruoActual())
        {
            case EstadosMonstruo.EnRuta:
                if (((Vector2)transform.position - ruta[puntoRutaActual].EstaPosicion()).sqrMagnitude < MARGEN)
                    puntoRutaActual = (puntoRutaActual + 1) % ruta.Length;
                break;
            case EstadosMonstruo.PensandoRuta:
				caminoDeVuelta = ControladorRecorrido.instance.EncontarCamino(monstruoRB.position,ruta);
                if(caminoDeVuelta!=null)
                    monstruo.CambiarEstadoMonstruo(EstadosMonstruo.VolviendoARuta);
                break;
            case EstadosMonstruo.VolviendoARuta:
                if(caminoDeVuelta.Fin())
                    monstruo.CambiarEstadoMonstruo(EstadosMonstruo.EnRuta);
                else
                    caminoDeVuelta.PosicionObjetivo(transform.position);
                break;
        }
    }

    void OnTriggerStay2D (Collider2D other)
	{
        if (other.gameObject.tag == "Path") 
        {
            if (monstruo.EstadoMonstruoActual() != EstadosMonstruo.EnRuta)
            {
                PuntoRecorrido punto = other.GetComponent<PuntoRecorrido>();

                int i = IndicePuntoRuta(punto);
                if (i != -1)
                {
                    puntoRutaActual = i;
                }
            }
        }
	}
		
    int IndicePuntoRuta(PuntoRecorrido punto)
    {
        int i = 0;
        while (i < ruta.Length && punto != ruta[i])
            i++;
        return i;
    }
    public Vector2 PosicionPuntoRuta()
    {
        switch (monstruo.EstadoMonstruoActual())
        {
            case EstadosMonstruo.EnRuta:
	            return ruta[puntoRutaActual].EstaPosicion();
            case EstadosMonstruo.VolviendoARuta:
                return caminoDeVuelta.Posicion();
            default:
                return Vector2.positiveInfinity;
        }
	}
		
}
