using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Colisiones;

[RequireComponent(typeof(Interactuable))]
public class Palanca : MonoBehaviour 
{
	Interactuable master;
	bool puedeInteractuar;

	GameObject[] doors; 

	public float distanciaInteraccion=1f;
	LayerMask conQueColisiona;
	public string colorDelInterruptor;
	public float distanciaDeInteraccion=0.3f;

<<<<<<< HEAD
	Interactuable master;

	GameObject[] doors; 

	void Start () {
        conQueColisiona = Colision.CapasInteraccion();

        master = GetComponent<Interactuable>();
		master.Accion = (Jugador a) => {
			
=======
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
>>>>>>> 5c036531624aa2b64f7ec7211d67ba52912b1db0
                transform.Rotate(new Vector3(0f, 0f, 180f));
                doors = GameObject.FindGameObjectsWithTag(colorDelInterruptor);
				for (int i = 0; i < doors.Length; i++)
					doors[i].GetComponent<AbrirPuerta>().abrir();
<<<<<<< HEAD
                ControladorRecorrido.instance.ReiniciarRed();

=======
                PathManager.instance.ReiniciarRed();
			}

			GetComponent<Collider2D>().enabled = true;
>>>>>>> 5c036531624aa2b64f7ec7211d67ba52912b1db0
		};
        master.EsPosibleLaInteraccion = (Jugador a) =>
        {
            return master.InteraccionPorLineaDeVision(a.transform, transform, distanciaDeInteraccion, conQueColisiona);
        };
        master.DistanciaDeInteraccion = () => { return distanciaDeInteraccion; };
    }
}
