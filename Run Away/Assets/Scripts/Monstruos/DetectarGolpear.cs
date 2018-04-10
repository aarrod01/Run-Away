using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectarGolpear : MonoBehaviour 
{
	Golpear golpear;

	void Start () 
	{
		golpear = GetComponentInParent<Golpear> ();
	}
	

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player") 
		{
			golpear.Golpeando ();
		}
	}
}
