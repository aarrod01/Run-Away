using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof (Collider2D))]
public class PuntoRecorrido : MonoBehaviour {

	public PuntoRecorrido[] posicionesConectadas;
    public PuntoRecorrido[] posiblesPosicionesConectadas;

    Vector2[] cuatroPosiciones;
    Vector2[] cuatroCardinales;
    Vector2 estaPosicion;
    LayerMask conQueColisiona;
    int puntosDentro=0;
    float unCuarto;

    [HideInInspector]
    public Collider2D caja;

    public void CrearPrimerosContactos()
    {
        conQueColisiona = LayerMask.GetMask("Obstaculos", "Recorrido");
        //Desactiva su collider para que el rayo a proyectar no interfiera.
        unCuarto = GetComponent<Collider2D>().bounds.size.x / 8f;
        gameObject.GetComponent<Collider2D>().enabled = false;
        estaPosicion = transform.position;
        //Busca todos los puntos del mapa.
        PuntoRecorrido[] puntosMapa = GameObject.FindObjectsOfType<PuntoRecorrido>();

        //Creamos un vector auxiliar que guardara los puntos en contacto
        PuntoRecorrido[] aux = new PuntoRecorrido[puntosMapa.Length];
        int j = 0;
        PuntoRecorrido[] puntosTotales = GameObject.FindObjectsOfType<PuntoRecorrido>();

        //

        cuatroPosiciones = new Vector2[4];
        cuatroCardinales = new Vector2[4];
        for (int i=0; i<4; i++)
        {
            cuatroPosiciones[i] = new Vector2(SucesionZeroUnoZeroMenosUno(i), SucesionZeroUnoZeroMenosUno(i + 1))*unCuarto;
            cuatroCardinales[i] = cuatroPosiciones[i];
        }

        //Comprobamos que el punto no este dentro de uno de los puntos de la red.
        for(int i =0; i< puntosTotales.Length; i++)
        {
            if (puntosTotales[i].GetComponent<Collider2D>().bounds.Contains(estaPosicion)&&puntosTotales[i]!=this)
            {
                aux[j] = puntosTotales[i];
                aux[j].GetComponent<Collider2D>().enabled = false;
                j++;
                puntosDentro++;
            }
        }
        
        for (int i = 0; i < j; i++)
        {
            Vector2 esaPosicion = aux[i].transform.position;
            if (esaPosicion.y - estaPosicion.y < 0f)
            {
                cuatroPosiciones[2] = Vector2.negativeInfinity;
            }
            else if (esaPosicion.x - estaPosicion.x < 0f)
            {
                cuatroPosiciones[3] = Vector2.negativeInfinity;
            }
            else if (esaPosicion.y - estaPosicion.y > 0f)
            {
                cuatroPosiciones[0] = Vector2.negativeInfinity;
            }
            else if (esaPosicion.x - estaPosicion.x > 0f)
            {
                cuatroPosiciones[1] = Vector2.negativeInfinity;
            }
        }

        PuntoRecorrido aux1;
        for (int i = 0; i < 4; i++)
        {
            if (!Vector2.Equals(cuatroPosiciones[i], Vector2.negativeInfinity))
            {
                RaycastHit2D[] hit = new RaycastHit2D[2]{Physics2D.Raycast(estaPosicion+cuatroCardinales[(i+1)%4], cuatroCardinales[i], Mathf.Infinity, conQueColisiona),
                    Physics2D.Raycast(estaPosicion+cuatroCardinales[(i+3)%4], cuatroCardinales[i], Mathf.Infinity, conQueColisiona)
                };
                if (hit[0].collider != null && hit[1].collider != null 
                    && (aux[j]=hit[0].collider.gameObject.GetComponent<PuntoRecorrido>()) != null 
                    && (aux1 = hit[1].collider.gameObject.GetComponent<PuntoRecorrido>()) != null)
                {
                    if (!(Mathf.Approximately(aux[j].transform.position.x - estaPosicion.x, 0f) || Mathf.Approximately(aux[j].transform.position.y - estaPosicion.y, 0f)))
                        aux[j] = aux1;
                    j++;
                }
            }
        }

        //Actualizamos las posiciones conectadas
        posiblesPosicionesConectadas = new PuntoRecorrido[j];
        for (int i = 0; i < j; i++)
            posiblesPosicionesConectadas[i] = aux[i];
        //Volvemos a activar su collider.
        gameObject.GetComponent<Collider2D>().enabled = true;
        for (int i = 0; i < j; i++)
            posiblesPosicionesConectadas[i].GetComponent<Collider2D>().enabled = true;
    }

    int SucesionZeroUnoZeroMenosUno(int x)
    {
        return (x % 2) * (1 - ((x % 4) / 2) * 2);
    }

    //Metodo que pone como posiciones conectadas a aquellas con las que se puede unir el punto en linea recta sin que choque contra ningun obstáculo.
    [ExecuteInEditMode]
    public void ReiniciarContactos()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        int j = puntosDentro;
        for(int i =0; i<puntosDentro;i++)
        {
            posiblesPosicionesConectadas[i].GetComponent<Collider2D>().enabled = false;
        }

        PuntoRecorrido aux1;
        for (int i = 0; i < 4; i++)
        {
            if (!Vector2.Equals(cuatroPosiciones[i], Vector2.negativeInfinity))
            {
                RaycastHit2D[] hit = new RaycastHit2D[2]{Physics2D.Raycast(estaPosicion+cuatroCardinales[(i+1)%4], cuatroCardinales[i], Mathf.Infinity, conQueColisiona),
                    Physics2D.Raycast(estaPosicion+cuatroCardinales[(i+3)%4], cuatroCardinales[i], Mathf.Infinity, conQueColisiona)
                };
                if (hit[0].collider != null && hit[1].collider != null
                    && (posiblesPosicionesConectadas[j] = hit[0].collider.gameObject.GetComponent<PuntoRecorrido>()) != null
                    && (aux1 = hit[1].collider.gameObject.GetComponent<PuntoRecorrido>()) != null)
                {
                    if (!(Mathf.Approximately(posiblesPosicionesConectadas[j].transform.position.x - estaPosicion.x, 0f) 
                        || Mathf.Approximately(posiblesPosicionesConectadas[j].transform.position.y - estaPosicion.y, 0f)))
                        posiblesPosicionesConectadas[j] = aux1;
                    j++;
                }
            }
        }

        //Actualizamos las posiciones conectadas
        posiblesPosicionesConectadas = new PuntoRecorrido[j];
        for (int i = 0; i < j; i++)
            posicionesConectadas[i] = posiblesPosicionesConectadas[i];
        //Volvemos a activar su collider.
        gameObject.GetComponent<Collider2D>().enabled = true;
        for (int i = 0; i < j; i++)
            posiblesPosicionesConectadas[i].GetComponent<Collider2D>().enabled = true;

    }

    public float DistanciaHasta(PuntoRecorrido objetivo)
    {
        return (estaPosicion - objetivo.estaPosicion).sqrMagnitude;
    }
    public float DistanciaHasta(Vector2 objetivo)
    {
        return (estaPosicion - objetivo).sqrMagnitude;
    }
    public PuntoRecorrido[] PuntosConectados()
    {
        return posicionesConectadas;
    }
    public Vector2 EstaPosicion()
    {
        return estaPosicion;
    }
    public void CambiarPosicion(Vector2 a)
    {
        estaPosicion = a;
        transform.position = a;
    }
    public bool EstaEstePuntoEn(PuntoRecorrido[] aBuscar)
    {
        int i = 0;
        while (i < aBuscar.Length && this != aBuscar[i])
            i++;
        return i != aBuscar.Length;
    }
    public bool EstaDentro()
    {
        return true;
    }

}
