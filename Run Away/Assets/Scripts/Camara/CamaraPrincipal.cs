using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;


public class CamaraPrincipal : MonoBehaviour 
{
	public float smoothTime = 1;
    public float anchoInfinito = 1f;
    public float frecuenciaLemniscata = 1f;
    public float velocidadGiro = 1f;
    public float anguloGiroMaximo = 10f;
    public PostProcessingProfile perfil;

    Rigidbody2D rbJugador, cameraRb, punteroRb;
    Transform transformCamaraRelativo;
	Jugador jugador;
	Vector2 velocidadTraslacionSimple;
    ColorGradingModel a;

    // Use this for initialization
    void Start () {
        cameraRb = GetComponent<Rigidbody2D>();
        GameObject player = GameObject.FindWithTag("Player");
        if(player != null){
            rbJugador = player.GetComponent<Rigidbody2D>();
            jugador = player.GetComponent<Jugador>();
            cameraRb.position = rbJugador.position;
        }
        PunteroRetardo aux = GameObject.FindObjectOfType<PunteroRetardo>();
        if(aux!=null)
            punteroRb = aux.GetComponent<Rigidbody2D>();
       
        transformCamaraRelativo = transform.GetChild(0);
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
        if(rbJugador!=null)
			Vector2.SmoothDamp (cameraRb.position,
                rbJugador.position, ref velocidad, smoothTime, float.MaxValue, Time.deltaTime);
    }

    void CameraRoll()
    {
        if (rbJugador != null)
        {
            if (rbJugador.velocity.x > 0f)
            {
                cameraRb.rotation = Mathf.Min(cameraRb.rotation + Time.deltaTime * velocidadGiro, anguloGiroMaximo);
            }
            else if (rbJugador.velocity.x < 0f)
            {
                cameraRb.rotation = Mathf.Max(cameraRb.rotation - Time.deltaTime * velocidadGiro, -anguloGiroMaximo);
            }
        }
    }
    
    public void CambiarBrillo(float brillo)
    {
       ColorGradingModel.Settings aux = perfil.colorGrading.settings;
       aux.basic.postExposure = brillo;
       perfil.colorGrading.settings = aux;
    }
}
