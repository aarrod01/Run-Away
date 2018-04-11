using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Colores;

[RequireComponent(typeof(Interactuable))]
public class Palanca : MonoBehaviour 
{
	Interactuable master;
	bool puedeInteractuar;

	Puerta[] puertas; 

	public float distanciaInteraccion=1f;
	LayerMask conQueColisiona;
	public  Colores.Colores colorDelInterruptor;
	public float distanciaDeInteraccion=0.3f;

	void Start () {
        puertas = GameObject.FindObjectsOfType<Puerta>();

        conQueColisiona = LayerMask.GetMask("Obstaculos","Objetos");
        master = GetComponent<Interactuable>();
		master.Accion = (Jugador a) => {

                transform.Rotate(new Vector3(0f, 0f, 180f));
				for (int i = 0; i < puertas.Length; i++)
					puertas[i].abrir();
                ControladorRecorrido.instance.ReiniciarRed();
		};
        master.EsPosibleLaInteraccion = (Jugador a) =>
        {
            return master.InteraccionPorLineaDeVision(a.transform, transform, distanciaDeInteraccion, conQueColisiona);
        };
        master.DistanciaDeInteraccion = () => { return distanciaDeInteraccion; };
    }
}
