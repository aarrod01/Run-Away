using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorParedes : MonoBehaviour {
    public float longitudRayo = 1f;
    Transform[] detector;
    LayerMask conQueColisiona;
    float distanciaAlCentro;
	// Use this for initialization
	void Start () {
        conQueColisiona = LayerMask.GetMask("Obstaculos");
        GameObject[] auxO = new GameObject[] { new GameObject(), new GameObject()};
        
        detector = new Transform[2];
        detector[0] = auxO[0].transform;
        detector[1] = auxO[1].transform;
        detector[0].parent = transform.parent;
        detector[1].parent = transform.parent;
        Collider2D aux = GetComponentInParent<Collider2D>();
        detector[0].position = new Vector2(aux.bounds.min.x,0f);
        detector[1].position = new Vector2(aux.bounds.max.x,0f);
        distanciaAlCentro = (aux.bounds.max.x - aux.bounds.min.x)*2f;

	}

    public Vector2 EvitarColision(Vector2 destino)
    {
        RaycastHit2D[] hit = new RaycastHit2D[] { Physics2D.Raycast(detector[0].position, transform.up, longitudRayo, conQueColisiona),
            Physics2D.Raycast(detector[1].position, transform.up, longitudRayo, conQueColisiona) };
        Debug.DrawRay(detector[0].position, transform.up*longitudRayo,Color.red);
        Debug.DrawRay(detector[1].position, transform.up*longitudRayo, Color.red);
        Debug.DrawRay(transform.parent.position, transform.up*10f, Color.red);
        Vector2 distanciaMasLejana = transform.position, estaPosicion=transform.position;
        int indiceMasCercano=-1;
        for (int i =0; i<hit.Length;i++)
        {
            if (hit[i].collider!=null && 
                Vector2.Distance(distanciaMasLejana, estaPosicion) < Vector2.Distance(hit[i].point, estaPosicion))
                indiceMasCercano = i;
        }
        if (indiceMasCercano != -1)
            return (hit[indiceMasCercano].normal.normalized * distanciaAlCentro+hit[indiceMasCercano].point);
        else
            return destino;
    }
}
