using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monstruos;
using Recorrido;

public class DetectarRuta : MonoBehaviour 
{
	int puntoRutaActual = 1;
	Monstruo monstruo;
    Rigidbody2D monstruoRB;
    listaNodos caminoDeVuelta;

    [HideInInspector]
    public PuntoRecorrido[] ruta;

    const float MARGEN = 0.001f;
    
	void Start(){

		monstruo = GetComponentInParent<Monstruo> ();
        monstruoRB = monstruo.GetComponent<Rigidbody2D>();

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

                puntoRutaActual = IndicePuntoRuta(punto);
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

    public void CrearPuntosRuta(PuntoRecorrido[] puntosReco)
    {
        ruta = puntosReco;
    }

    public void AnyadirPuntoRuta(PuntoRecorrido nuevo)
    {
        if (ruta == null)
        {
            ruta = new PuntoRecorrido[1];
            ruta[0] = nuevo;
        }
        else
        {
            if(ruta[ruta.Length-1]!=nuevo)
            {
                PuntoRecorrido[] aux = new PuntoRecorrido[ruta.Length + 1];
                for (int i = 0; i < ruta.Length; i++)
                    aux[i] = ruta[i];
                aux[ruta.Length] = nuevo;
                ruta = new PuntoRecorrido[aux.Length];
                for (int i = 0; i < ruta.Length; i++)
                    ruta[i] = aux[i];
            }
        }
    }

    public void QuitarUltimoPuntoRuta()
    {
        if(ruta!=null)
        {
            if (ruta.Length == 1)
                ruta = null;
            else
            {
                PuntoRecorrido[] aux = new PuntoRecorrido[ruta.Length - 1];
                for (int i = 0; i < aux.Length; i++)
                    aux[i] = ruta[i];
                ruta = new PuntoRecorrido[aux.Length];
                for (int i = 0; i < ruta.Length; i++)
                    ruta[i] = aux[i];
            }
        }
    }

}
