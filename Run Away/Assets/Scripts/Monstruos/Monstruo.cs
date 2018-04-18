using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monstruos;

namespace Monstruos
{
	public enum EstadosMonstruo{SiguiendoJugador, EnRuta, PensandoRuta ,VolviendoARuta, Desorientado,Proyectado,BuscandoJugador, Ninguno};
    public enum TipoMonstruo { Basico, Ninguno};
}

public class Monstruo : MonoBehaviour 
{
	public float velMovRuta, velMovPerseguir, velGiro,aceleracionAngular, tiempoAturdimiento=1f, periodoGiro=1f;
    public EstadosMonstruo estadoMonstruo;
    public TipoMonstruo tipo;

    LayerMask conQueColisiona;
    Rigidbody2D rb2D;
	Transform jugadorTrans;
    DetectorParedes detectorParedes;
    float giroInicial;
    float cronometro;

    const float MARGEN = 0.001f;
    const float MARGENANGULO = 5f;

    void Start () 
	{
        conQueColisiona = LayerMask.GetMask("Obstaculos", "Recorrido");
		rb2D = GetComponent <Rigidbody2D> ();
		jugadorTrans = GameObject.FindWithTag ("Player").GetComponent<Transform> ();
        detectorParedes = GetComponentInChildren<DetectorParedes>();
		estadoMonstruo = EstadosMonstruo.EnRuta;
	}

	void FixedUpdate ()
	{	
		Vector2 posPlayer = GetComponentInChildren<CampoVision>().UltimaPosicionJugador();
		Vector2 posPuntoRuta = GetComponentInChildren<DetectarRuta> ().PosicionPuntoRuta ();

		switch (estadoMonstruo) 
		{
			case EstadosMonstruo.EnRuta:
				MoverseHacia (detectorParedes.EvitarColision(posPuntoRuta), velMovRuta);
				break;
			case EstadosMonstruo.SiguiendoJugador:
                if ((GetComponentInChildren<CampoVision>().UltimaPosicionJugador() - rb2D.position).sqrMagnitude < MARGEN)
                {
                    CambiarEstadoMonstruo(EstadosMonstruo.Desorientado);
                }
                MoverseHacia(detectorParedes.EvitarColision(posPlayer), velMovPerseguir);
                break;
			case EstadosMonstruo.VolviendoARuta:
				MoverseHacia (posPuntoRuta, velMovRuta);
				break;
            case EstadosMonstruo.Desorientado:
                Pararse();
                giroInicial = rb2D.rotation;
                cronometro = Time.time;
                
                CambiarEstadoMonstruo(EstadosMonstruo.BuscandoJugador);
                break;
            case EstadosMonstruo.BuscandoJugador:
                Pararse();
                Vector2 aux = new Vector2(Mathf.Sin(((Time.time - cronometro) / periodoGiro + giroInicial/360f) * 2 * Mathf.PI ), Mathf.Cos(((Time.time - cronometro) / periodoGiro + giroInicial / 360f) * 2 * Mathf.PI));
                GiroInstantaneo(aux);
                if (Time.time-cronometro>periodoGiro)
                {
                    CambiarEstadoMonstruo(EstadosMonstruo.PensandoRuta);
                }
                break;
            case EstadosMonstruo.Proyectado:
                if (Time.time - cronometro > tiempoAturdimiento)
                    CambiarEstadoMonstruo(EstadosMonstruo.Desorientado);
                break;
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		/*if (collision.gameObject.tag == "Player")
			SceneManager.LoadScene ("Tutorial");*/
	}

	public void CambiarEstadoMonstruo (EstadosMonstruo estado)
	{
		estadoMonstruo = estado;
	}

	public EstadosMonstruo EstadoMonstruoActual ()
	{
		return estadoMonstruo;
	}

    //Seguir al jugador, moverse por la ruta y volver a la ruta.
    void MoverseHacia(Vector2 dir, float vel)
    {
        rb2D.velocity = (dir-(Vector2)transform.position).normalized * vel;
        Giro(rb2D.velocity);
    }

    void Giro(Vector2 dir)
    {
        if (dir != Vector2.zero)
            rb2D.rotation = Mathf.LerpAngle(rb2D.rotation,Mathf.Atan2(-dir.x, dir.y)*180f/Mathf.PI, aceleracionAngular);
    }

    void GiroInstantaneo(Vector2 dir)
    {
        if (dir != Vector2.zero)
            rb2D.rotation = Mathf.Atan2(-dir.x, dir.y) * 180f / Mathf.PI;
    }

    public void Empujar(Vector2 origen, float velocidadProyeccion)
    {
        rb2D.velocity = (rb2D.position - origen).normalized * velocidadProyeccion;
        cronometro = Time.time;
        CambiarEstadoMonstruo(EstadosMonstruo.Proyectado);
    }
    
    void Pararse()
    {
        rb2D.velocity = Vector2.zero;
    }

	void Stop ()
	{
		rb2D.velocity = Vector2.zero;
	}

}		

