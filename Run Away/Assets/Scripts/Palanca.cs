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
            Vector3 pos = transform.position;
			RaycastHit2D hit = Physics2D.Raycast(pos,a.transform.position- pos, distanciaInteraccion, conQueColisiona);
			if(hit.collider.tag=="Player")
			{
				doors = GameObject.FindGameObjectsWithTag(colorDelInterruptor);
				for (int i = 0; i < doors.Length; i++)
					doors[i].GetComponent<AbrirPuerta>().abrir();
			}
			GetComponent<Collider2D>().enabled = true;
		};
	}
}
