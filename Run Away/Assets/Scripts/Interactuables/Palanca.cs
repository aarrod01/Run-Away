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
    //static Sonidosss sonidoPalanca = null;
    float tiempo;
    
	public  Colores.Colores color;
	public float distanciaDeInteraccion=0.3f;
    public bool posicionInicial;
    public AudioClip sonido;
    [Range(0,1)]
    public float volumen;
    public float tiempoDeReactivacion;

	void Start () {
       /* if (sonidoPalanca == null)
        {
            sonidoPalanca = new Sonidosss(sonido, false, true, volumen, 1f, SoundManager.instance.VolumenMusica);
            DontDestroyOnLoad(sonidoPalanca.gO);
        }*/
        luz = GetComponentInChildren<DynamicLight2D.DynamicLight>().gameObject;
        Apagar();
        puertas = GameObject.FindObjectsOfType<Puerta>();
        palancaAnimacion = GetComponent<Animator>();
        palancaAnimacion.SetBool("activada", posicionInicial);
        palancaAnimacion.SetInteger("color",(int)color);
        conQueColisiona = LayerMask.GetMask("Obstaculos", "Jugador");
        master = GetComponent<Interactuable>();
		master.Accion = (Jugador a) => {
            tiempo = Time.time;
            //sonidoPalanca.Activar();
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
            return (Time.time - tiempo)>tiempoDeReactivacion && master.InteraccionPorLineaDeVision(a.transform, distanciaDeInteraccion, conQueColisiona);
        };
        master.DistanciaDeInteraccion = () => { return distanciaDeInteraccion; };
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
