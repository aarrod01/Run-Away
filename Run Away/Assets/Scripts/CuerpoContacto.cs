using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monster;

public class CuerpoContacto : MonoBehaviour 
{
	public int numeroMonstruo;

	public int puntoRutaActual = 0;
	MonsterMovement monstruo;

	Vector2[] ruta; 

	void Start(){
		//Creación de un array compuesto de las posiciones de los puntos de ruta.
		Vector3 posRutas = GameObject.Find("Rutas").GetComponent <Transform> ().position;
		Transform[] transformAuxiliar = GameObject.Find("Ruta" + numeroMonstruo).GetComponentsInChildren <Transform> ();
		ruta = new Vector2[transformAuxiliar.Length-1];

		for (int i = 1; i < transformAuxiliar.Length; i++)
			ruta [i-1] = (Vector2) (transformAuxiliar [i].position + transformAuxiliar [0].position + posRutas);
		monstruo = GetComponentInParent<MonsterMovement> ();
	}


	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag == "Path") 
		{
			if (monstruo.EstadoMonstruoActual () == EstadosMonstruo.EnRuta)
				puntoRutaActual = (puntoRutaActual + 1) % ruta.Length;
			else if (monstruo.EstadoMonstruoActual () == EstadosMonstruo.SiguiendoJugador) 
				puntoRutaActual = int.Parse (other.name);
			else if (monstruo.EstadoMonstruoActual () == EstadosMonstruo.VolviendoARuta) 
			{
				puntoRutaActual = (int.Parse (other.name) + 1) % ruta.Length;
				monstruo.CambiarEstadoMonstruo (EstadosMonstruo.EnRuta);
			}
		}
	}
		
	public Vector2 PosicionPuntoRuta()
	{
		return ruta[puntoRutaActual];
	}
		
}
