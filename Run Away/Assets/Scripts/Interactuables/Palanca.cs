using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Colores;

[RequireComponent(typeof(Interactuable))]
public class Palanca : MonoBehaviour 
{
	Interactuable master;
    LayerMask conQueColisiona;
    Puerta[] puertas;
    Animator palancaAnimacion;

    public float distanciaInteraccion = 1f;
	public  Colores.Colores color;
	public float distanciaDeInteraccion=0.3f;
    public bool posicionInicial;

	void Start () {

        conQueColisiona = LayerMask.GetMask("Obstaculos","Objetos");
        puertas = GameObject.FindObjectsOfType<Puerta>();
        palancaAnimacion = GetComponent<Animator>();
        palancaAnimacion.SetBool("activada", posicionInicial);
        palancaAnimacion.SetInteger("color",(int)color);
        conQueColisiona = Colision.CapasInteraccion();
        master = GetComponent<Interactuable>();
		master.Accion = (Jugador a) => {
                posicionInicial = !posicionInicial;
                palancaAnimacion.SetBool("activada", posicionInicial);

            for (int i = 0; i < puertas.Length; i++)
            {
                if(puertas[i].color == color)
                    puertas[i].abrir();
            }
                ControladorRecorrido.instance.ReiniciarRed();
		};
        master.EsPosibleLaInteraccion = (Jugador a) =>
        {
            return master.InteraccionPorLineaDeVision(a.transform, transform, distanciaDeInteraccion, conQueColisiona);
        };
        master.DistanciaDeInteraccion = () => { return distanciaDeInteraccion; };
    }

    public void Iluminar()
    {

    }

    public void Apagar()
    {

    }
}
