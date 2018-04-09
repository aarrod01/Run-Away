using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Interactuable))]
public class Palanca : MonoBehaviour 
{
	Interactuable master;
	bool puedeInteractuar;

	GameObject[] doors; 

	public float distanciaInteraccion=1f;
	public LayerMask conQueColisiona;
	public string colorDelInterruptor;
	public float distanciaDeInteraccion=0.3f;

	void Start () 
	{
		master = GetComponent<Interactuable>();
		master.Click = (PlayerMovement a) => 
		{
			GetComponent<Collider2D>().enabled = false;
            
            Vector3 pos = transform.position;
			RaycastHit2D hit = Physics2D.Raycast(pos,a.transform.position- pos, distanciaInteraccion, conQueColisiona);

			if(hit.collider!=null&&hit.collider.tag=="Player")
			{
                transform.Rotate(new Vector3(0f, 0f, 180f));
                doors = GameObject.FindGameObjectsWithTag(colorDelInterruptor);
				for (int i = 0; i < doors.Length; i++)
					doors[i].GetComponent<AbrirPuerta>().abrir();
                PathManager.instance.ReiniciarRed();
			}

			GetComponent<Collider2D>().enabled = true;
		};
	}
}
