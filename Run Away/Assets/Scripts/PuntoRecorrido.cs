using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntoRecorrido : MonoBehaviour {

	Vector2 estaPosicion;
	public PuntoRecorrido[] posicionesConectadas;
	public LayerMask conQueColisiona;

    //Metodo que pone como posiciones conectadas a aquellas con las que se puede unir el punto en linea recta sin que choque contra ningun obstáculo.
    public void ReiniciarContactos()
    {
        //Desactiva su collider para que el rayo a proyectar no interfiera.
        gameObject.GetComponent<Collider2D>().enabled = false;
        estaPosicion = transform.position;
        //Busca todos los puntos del mapa.
        GameObject[] puntosMapa = GameObject.FindGameObjectsWithTag("Path");

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
}
