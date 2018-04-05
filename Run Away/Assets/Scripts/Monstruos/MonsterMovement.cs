using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monster;
using UnityEngine.SceneManagement;
namespace Monster
{
	public enum EstadosMonstruo{SiguiendoJugador, EnRuta, PensandoRuta ,VolviendoARuta, Desorientado,Proyectado,BuscandoJugador, Ninguno};
    public enum TipoMonstruo { Basico, Ninguno};
}

public class MonsterMovement : MonoBehaviour 
{
    public LayerMask conQueColisiona;

	public float velMovRuta, velMovPerseguir, velGiro,aceleracionAngular, tiempoAturdimiento=1f;
    public EstadosMonstruo estadoMonstruo;
    public TipoMonstruo tipo;

    Rigidbody2D rb2D;
	Transform jugadorTrans;
    float giroInicial,giroFinal, sentidoGiro;
    float cronometro;

    const float MARGEN = 0.001f;
    const float MARGENANGULO = 5f;

    void Start () 
	{
		rb2D = GetComponent <Rigidbody2D> ();
		jugadorTrans = GameObject.FindWithTag ("Player").GetComponent<Transform> ();
		estadoMonstruo = EstadosMonstruo.EnRuta;
	}

	void FixedUpdate ()
	{	
		Vector2 posPlayer = GetComponentInChildren<CampoVision>().UltimaPosicionJugador() - (Vector2)transform.position;
		Vector2 posPuntoRuta = GetComponentInChildren<CuerpoContacto> ().PosicionPuntoRuta () - (Vector2)transform.position;

		switch (estadoMonstruo) 
		{
			case EstadosMonstruo.EnRuta:
				MoverseHacia (posPuntoRuta, velMovRuta);
				break;
			case EstadosMonstruo.SiguiendoJugador:
                if ((GetComponentInChildren<CampoVision>().UltimaPosicionJugador() - rb2D.position).sqrMagnitude < MARGEN)
                {
                    CambiarEstadoMonstruo(EstadosMonstruo.Desorientado);
                }
                MoverseHacia (posPlayer, velMovPerseguir);
				break;
			case EstadosMonstruo.VolviendoARuta:
				MoverseHacia (posPuntoRuta, velMovRuta);
				break;
            case EstadosMonstruo.Desorientado:
                Pararse();
                giroInicial = rb2D.rotation;
                sentidoGiro = Mathf.Pow(-1, Random.Range(0, 1));
                giroFinal = giroInicial + sentidoGiro*360f;
                
                CambiarEstadoMonstruo(EstadosMonstruo.BuscandoJugador);
                break;
            case EstadosMonstruo.BuscandoJugador:
                Pararse();
                rb2D.MoveRotation(Mathf.Lerp(rb2D.rotation, giroFinal, velGiro));
                if (Mathf.Abs(rb2D.rotation- giroFinal)<MARGENANGULO)
                {
                    rb2D.rotation = giroInicial;
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

        //rb2D.velocity = Vector2.Lerp(rb2D.velocity,dir.normalized * vel,factorAceleracion);
        //rb2D.rotation=Mathf.Atan2(dir.y, dir.x)*180f/Mathf.PI-90f;
        rb2D.velocity = dir.normalized * vel;
        Giro(rb2D.velocity);
    }

    void Giro(Vector2 dir)
    {
        float velocidadAngularPredicha;
        Vector2 direccionMovimientoObjetivo = dir;
        float anguloPredicho = Vector2.SignedAngle(Vector2.up, direccionMovimientoObjetivo) - rb2D.rotation;
        if (anguloPredicho > 180f)
            anguloPredicho -= 360f;
        else if (anguloPredicho < -180f)
            anguloPredicho += 360f;
        velocidadAngularPredicha = (anguloPredicho) / Time.fixedDeltaTime;
        rb2D.angularVelocity = Mathf.Lerp(rb2D.angularVelocity, Mathf.Max(Mathf.Min(velGiro, velocidadAngularPredicha), -velGiro), aceleracionAngular);
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

