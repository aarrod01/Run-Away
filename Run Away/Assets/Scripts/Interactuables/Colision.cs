using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Colision : MonoBehaviour {

    Collider2D[] cajaColision;
    public bool posicion;
	// Use this for initialization
	void Start () {
        cajaColision = GetComponents<Collider2D>();
	}
	
    public void Activar(bool act)
    {
        gameObject.SetActive(act);
    }
}
