using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof (Collider2D))]
public class PuntoRecorrido : MonoBehaviour {

	Vector2 estaPosicion;
	PuntoRecorrido[] posicionesConectadas ;
	LayerMask conQueColisiona;

    //Metodo que pone como posiciones conectadas a aquellas con las que se puede unir el punto en linea recta sin que choque contra ningun obstáculo.
    public void ReiniciarContactos()
    {
        conQueColisiona = LayerMask.GetMask("Obstaculos", "Recorrido");
        //Desactiva su collider para que el rayo a proyectar no interfiera.
        gameObject.GetComponent<Collider2D>().enabled = false;
        estaPosicion = transform.position;
        //Busca todos los puntos del mapa.
        PuntoRecorrido[] puntosMapa = GameObject.FindObjectsOfType<PuntoRecorrido>();

        //Creamos un vector auxiliar que guardara los puntos en contacto
        PuntoRecorrido[] aux = new PuntoRecorrido[puntosMapa.Length];

        int j = 0;
        for (int i = 0; i < puntosMapa.Length; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(estaPosicion, (Vector2)puntosMapa[i].transform.position - estaPosicion, Mathf.Infinity, conQueColisiona);


            if (hit.collider != null && hit.collider.gameObject == puntosMapa[i].gameObject)
            {
                aux[j] = puntosMapa[i].GetComponent<PuntoRecorrido>();
                j++;
            }
        }

        //Actualizamos las posiciones conectadas
        posicionesConectadas = new PuntoRecorrido[j];
        for (int i = 0; i < j; i++)
            posicionesConectadas[i] = aux[i];
        //Volvemos a activar su collider.
        gameObject.GetComponent<Collider2D>().enabled = true;
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
}
