using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Colisiones;

[RequireComponent(typeof(Interactuable))]
public class Palanca : MonoBehaviour {

	public float distanciaInteraccion=1f;
	LayerMask conQueColisiona;
	public string colorDelInterruptor;
	public float distanciaDeInteraccion=0.3f;

	Interactuable master;

	GameObject[] doors; 

	void Start () {
        conQueColisiona = Colision.CapasInteraccion();

        master = GetComponent<Interactuable>();
		master.Accion = (Jugador a) => {
			
                transform.Rotate(new Vector3(0f, 0f, 180f));
                doors = GameObject.FindGameObjectsWithTag(colorDelInterruptor);
				for (int i = 0; i < doors.Length; i++)
					doors[i].GetComponent<AbrirPuerta>().abrir();
                ControladorRecorrido.instance.ReiniciarRed();

		};
        master.EsPosibleLaInteraccion = (Jugador a) =>
        {
            return master.InteraccionPorLineaDeVision(a.transform, transform, distanciaDeInteraccion, conQueColisiona);
        };
        master.DistanciaDeInteraccion = () => { return distanciaDeInteraccion; };
    }
}
