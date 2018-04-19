using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorParedes : MonoBehaviour {

    public float longitudRayo = 1f;
    LayerMask conQueColisiona;
    float distanciaAlCentro;

	// Use this for initialization
	void Start ()
    {
        conQueColisiona = LayerMask.GetMask("Obstaculos");
        Collider2D aux = GetComponentInParent<Collider2D>();
        distanciaAlCentro = (aux.bounds.max.x - aux.bounds.min.x)/2f;
	}

    public Vector2 EvitarColision(Vector2 destino)
    {
        Vector2 origen = transform.position;
        Vector2 direccionAPunto = destino-origen;
        Vector2 perpendicular = (new Vector2(-direccionAPunto.y,direccionAPunto.x)).normalized*distanciaAlCentro;

        RaycastHit2D[] hit = new RaycastHit2D[] { Physics2D.Raycast(origen, GirarVector(direccionAPunto,30f), longitudRayo, conQueColisiona),
            Physics2D.Raycast(origen, GirarVector(direccionAPunto,-30f), longitudRayo, conQueColisiona) , Physics2D.Raycast(origen, direccionAPunto, longitudRayo, conQueColisiona)};
        Debug.DrawRay(origen, GirarVector(direccionAPunto, 30f));
        Debug.DrawRay(origen, GirarVector(direccionAPunto, -30f));
        Debug.DrawRay(origen, direccionAPunto);
        Vector2 distanciaMasLejana = origen, estaPosicion=transform.position;
        int indiceMasCercano=-1;
        for (int i =0; i<hit.Length;i++)
        {
            if (hit[i].collider!=null && 
                Vector2.Distance(distanciaMasLejana, estaPosicion) < Vector2.Distance(hit[i].point, estaPosicion))
                indiceMasCercano = i;
        }
        if (indiceMasCercano != -1) {
            Vector2 normalMuro = hit[indiceMasCercano].normal, puntoMuro = hit[indiceMasCercano].point;
            Debug.DrawRay(new Vector2((perpendicular.y * Vector2.Dot(normalMuro, puntoMuro) - normalMuro.y * Vector2.Dot(perpendicular, origen)) / (perpendicular.y * normalMuro.x - normalMuro.y * perpendicular.x),
                (perpendicular.x * Vector2.Dot(normalMuro, puntoMuro) - normalMuro.x * Vector2.Dot(perpendicular, origen)) / (perpendicular.x * normalMuro.y - normalMuro.x * perpendicular.y)), normalMuro, Color.blue);

            return new Vector2((perpendicular.y * Vector2.Dot(normalMuro, puntoMuro) - normalMuro.y * Vector2.Dot(perpendicular, origen)) / (perpendicular.y * normalMuro.x - normalMuro.y * perpendicular.x),
                (perpendicular.x *Vector2.Dot(normalMuro,puntoMuro)-normalMuro.x*Vector2.Dot(perpendicular,origen))/(perpendicular.x*normalMuro.y-normalMuro.x*perpendicular.y))+normalMuro.normalized*distanciaAlCentro;

        } else
            return destino;
    }

    public Vector2 GirarVector(Vector2 v, float giro)
    {
        Vector2 aux;
        aux.x = v.x * Mathf.Cos(giro * Mathf.PI / 180f) - v.y * Mathf.Sin(giro * Mathf.PI / 180f);
        aux.y = v.x * Mathf.Sin(giro * Mathf.PI / 180f) + v.y * Mathf.Cos(giro * Mathf.PI / 180f);
        return aux;
    }
}
