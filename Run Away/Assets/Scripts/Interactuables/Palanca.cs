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
    GameObject luz;
    
	public  Colores.Colores color;
	public float distanciaDeInteraccion=0.3f;
    public bool posicionInicial;
    public AudioSource sonido;

	void Start () {
        luz = GetComponentInChildren<DynamicLight2D.DynamicLight>().gameObject;
        Apagar();
        puertas = GameObject.FindObjectsOfType<Puerta>();
        palancaAnimacion = GetComponent<Animator>();
        palancaAnimacion.SetBool("activada", posicionInicial);
        palancaAnimacion.SetInteger("color",(int)color);
        conQueColisiona = LayerMask.GetMask("Obstaculos", "Jugador");
        master = GetComponent<Interactuable>();
		master.Accion = (Jugador a) => {
            sonido.Play();
            a.Interactuar();
            posicionInicial = !posicionInicial;
                palancaAnimacion.SetBool("activada", posicionInicial);
            Vector2 pos = a.GetComponent<Rigidbody2D>().position;
            for (int i = 0; i < puertas.Length; i++)
            {
                if(puertas[i].color == color)
                    puertas[i].Abrir(pos);
            }
                ControladorRecorrido.instance.ReiniciarRed();
		};
        master.EsPosibleLaInteraccion = (Jugador a) =>
        {
            return master.InteraccionPorLineaDeVision(a.transform, distanciaDeInteraccion, conQueColisiona);
        };
        master.DistanciaDeInteraccion = () => { return distanciaDeInteraccion; };
        sonido.Stop();
        Apagar();
    }

    public void Iluminar()
    {
        luz.SetActive(true);
    }

    public void Apagar()
    {
        luz.SetActive(false);
    }
}
