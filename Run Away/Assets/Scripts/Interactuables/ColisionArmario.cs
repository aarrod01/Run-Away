using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionArmario : MonoBehaviour {

    Collider2D cajaColision;

	// Use this for initialization
	void Start () {
        cajaColision = GetComponent<Collider2D>();
	}
	
	public void Contiene(bool a)
    {
        cajaColision.enabled = !a;
    }
}
