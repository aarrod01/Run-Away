using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CamaraPrincipal : MonoBehaviour 
{
	public float smoothTime = 1;
    public float anchoInfinito = 1f;
    public float frecuenciaLemniscata = 1f;
    public float velocidadGiro = 1f;
    public float anguloGiroMaximo = 10f;

    Rigidbody2D rbJugador, cameraRb, punteroRb;
    Transform transformCamaraRelativo;
	Jugador jugador;
	Vector2 velocidadTraslacionSimple;

    // Use this for initialization
    void Start () {
        GameObject player = GameObject.FindWithTag("Player");
        rbJugador = player.GetComponent<Rigidbody2D>();
        punteroRb = GameObject.FindWithTag("Pointer").GetComponent<Rigidbody2D>();
        jugador = player.GetComponent<Jugador>();
        cameraRb = GetComponent<Rigidbody2D>();
        transformCamaraRelativo = transform.GetChild(0);
        cameraRb.position = rbJugador.position;
        velocidadTraslacionSimple = Vector2.zero;
    }
		
    void LateUpdate()
    {
        TraslacionSimple(ref velocidadTraslacionSimple);
        TraslacionLemniscata();
        CameraRoll();
        cameraRb.velocity = velocidadTraslacionSimple;
    }
    void TraslacionLemniscata()
    {
        Vector3 pos = transformCamaraRelativo.localPosition;
        float time = Time.time * Mathf.PI * 2f * frecuenciaLemniscata;
        pos.x = anchoInfinito * Mathf.Sqrt(2f) * Mathf.Cos(time) / (1f + Mathf.Pow(Mathf.Sin(time), 2f));
        pos.y = anchoInfinito * Mathf.Sqrt(2f) * Mathf.Sin(time) * Mathf.Cos(time * Mathf.PI * 2f * frecuenciaLemniscata) / (1f + Mathf.Pow(Mathf.Sin(time), 2f));
        transformCamaraRelativo.localPosition = pos;

    }
    void TraslacionSimple(ref Vector2 velocidad)
	{
			Vector2.SmoothDamp (cameraRb.position,
                rbJugador.position, ref velocidad, smoothTime, float.MaxValue, Time.deltaTime);
    }

    void CameraRoll()
    {
        if(rbJugador.velocity.x>0f)
        {
            cameraRb.rotation = Mathf.Min(cameraRb.rotation + Time.deltaTime * velocidadGiro,anguloGiroMaximo);
        }else if(rbJugador.velocity.x<0f)
        {
            cameraRb.rotation = Mathf.Max(cameraRb.rotation - Time.deltaTime * velocidadGiro, -anguloGiroMaximo);
        }
    }
}
