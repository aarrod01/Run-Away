using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monster;
using UnityEngine.SceneManagement;
namespace Monster
{
	public enum EstadosMonstruo{SiguiendoJugador, EnRuta, PensandoRuta ,VolviendoARuta, Ninguno};
}

public class MonsterMovement : MonoBehaviour 
{
    public LayerMask conQueColisiona;

	public float velMovRuta, velMovPerseguir, velGiro;

	Rigidbody2D rb2D;
	Transform jugadorTrans;

	public EstadosMonstruo estadoMonstruo;

	void Start () 
	{
		rb2D = GetComponent <Rigidbody2D> ();
		jugadorTrans = GameObject.FindWithTag ("Player").GetComponent<Transform> ();
		estadoMonstruo = EstadosMonstruo.EnRuta;
	}

	void FixedUpdate ()
	{	
		Vector2 posPlayer = jugadorTrans.position - transform.position;
		Vector2 posPuntoRuta = GetComponentInChildren<CuerpoContacto> ().PosicionPuntoRuta () - (Vector2)transform.position;

		switch (estadoMonstruo) 
		{
			case EstadosMonstruo.EnRuta:
				MoverseHacia (posPuntoRuta, velMovRuta);
				break;
			case EstadosMonstruo.SiguiendoJugador:
				MoverseHacia (posPlayer, velMovPerseguir);
				break;
			case EstadosMonstruo.VolviendoARuta:
				MoverseHacia (posPuntoRuta, velMovRuta);
				break;
		}
	}
	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player")
			SceneManager.LoadScene ("Tutorial");
	}

	public void CambiarEstadoMonstruo (EstadosMonstruo estado)
	{
		estadoMonstruo = estado;
	}
	public EstadosMonstruo EstadoMonstruoActual ()
	{
		return estadoMonstruo;
	}

	//Seguir al jugador, moverse por la ruta y volver a la ruta.
	void MoverseHacia (Vector2 dir, float vel)
	{
		rb2D.velocity = dir.normalized * vel;
		rb2D.MoveRotation (Mathf.LerpAngle (rb2D.rotation, Vector2.SignedAngle (Vector2.up, dir), velGiro));
	}

	void Stop ()
	{
		rb2D.velocity = Vector2.zero;
	}

}		

