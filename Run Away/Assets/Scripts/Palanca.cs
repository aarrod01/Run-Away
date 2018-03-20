using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Interactuable))]
public class Palanca : MonoBehaviour {

	public float distanciaInteraccion=1f;
	public LayerMask conQueColisiona;
	public string colorDelInterruptor;
	public float distanciaDeInteraccion=0.3f;

	Interactuable master;

	GameObject[] doors; 

	void Start () {
		master = GetComponent<Interactuable>();
		master.Click = (PlayerMovement a) => {
			GetComponent<Collider2D>().enabled = false;
			RaycastHit2D hit = Physics2D.Raycast(transform.position,a.transform.position- transform.position, distanciaInteraccion, conQueColisiona);
			if(hit.collider.tag=="Player"&&((Vector2)(transform.position-a.transform.position)).sqrMagnitude<distanciaInteraccion)
			{
				Debug.Log("Se ha detectado la colisión");
				doors = GameObject.FindGameObjectsWithTag(colorDelInterruptor);
				for (int i = 0; i < doors.Length; i++)
					doors[i].GetComponent<AbrirPuerta>().abrir();
				Debug.Log("Se abre la puerta");
			}
			GetComponent<Collider2D>().enabled = true;
		};
	}
}
