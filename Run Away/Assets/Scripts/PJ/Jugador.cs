using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monstruos;

[RequireComponent(typeof (Rigidbody2D))]
public class Jugador : MonoBehaviour 
{
	float velMaxima;
	public bool invisible = false,
		movimientoLibre = true;
    Animator animador;
	Rigidbody2D jugador, puntero;
    Vector2 direccionMirada,
            direccionMovimiento;
    Luz luz;
    GolpeJugador golpe;
    Piernas piernas;
    Vida vida;

    public float velocidadMaxima = 1f,
                factorAceleracion = 0.5f,
                velocidadAngularMaxima = 1f,
                factorAceleracionAngular = 0.5f,
                factorGiro = 0.5f,
                fraccionMinimaVelocidadHaciaDetras = 0.5f,
                vidas = 1, fuerzaEmpujon = 200f;
    public int danyo;
    public float retardoAtaque, duracionAtaque;

    void Start () {
        vida = GetComponent<Vida>();
        AumentoVelocidad(1f);
        luz = GetComponentInChildren<Luz>();
        jugador = GetComponent<Rigidbody2D>();
        animador = GetComponent<Animator>();
        puntero = PunteroRetardo.instance.GetComponent<Rigidbody2D>();
        golpe = GetComponentInChildren<GolpeJugador>();
        piernas = GetComponentInChildren<Piernas>();
        golpe.Iniciar();
        GameManager.instance.RestaurarJugador();
    }
		
	void FixedUpdate () 
	{
		if (vidas > 0) 
		{
			if (movimientoLibre) 
			{
				Giro ();
				Movimiento ();
			}
		} 
		else
			GameManager.instance.ResetarEscena();
	}

    void Giro()
	{
        float velocidadAngularPredicha;
        Vector2 direccionMovimientoObjetivo = puntero.position - jugador.position;
        float anguloPredicho = Vector2.SignedAngle(Vector2.up, direccionMovimientoObjetivo)- jugador.rotation;
        while (anguloPredicho > 180f)
            anguloPredicho -= 360f;
        while (anguloPredicho < -180f)
            anguloPredicho += 360f;
        velocidadAngularPredicha = (anguloPredicho)/Time.fixedDeltaTime;
        jugador.angularVelocity = Mathf.Lerp(jugador.angularVelocity,Mathf.Max(Mathf.Min(velocidadAngularMaxima, velocidadAngularPredicha),-velocidadAngularMaxima),factorAceleracionAngular);

        direccionMirada = new Vector2(-Mathf.Sin(jugador.rotation * Mathf.PI/180), Mathf.Cos(jugador.rotation*Mathf.PI/180));
    }

    void Movimiento()
    {
        //Obtenemos el vector de movimiento
        direccionMovimiento=(new Vector2 (1,0))*Input.GetAxis("Horizontal") 
            + (new Vector2(0, 1)) * Input.GetAxis("Vertical");

        //Por si se juega con mando, para no tener un vector mayor que 1.
        if (direccionMovimiento.sqrMagnitude>1)
            direccionMovimiento.Normalize();

        Vector2 velocidad = jugador.velocity;

        velocidad = Vector2.Lerp(velocidad, direccionMovimiento * velMaxima
                * ((direccionMovimiento + direccionMirada).sqrMagnitude * (1 - fraccionMinimaVelocidadHaciaDetras) + fraccionMinimaVelocidadHaciaDetras)
                , factorAceleracion);


        jugador.velocity = velocidad;
        direccionMovimiento.Normalize();
    }
    
    public void MovimientoLibre(bool variable)
    {
        movimientoLibre = variable;
    }

    public void Invisible(bool a) 
	{        
        invisible = a;
        
    }

    public void Esconderse(bool a, Vector2 en)
    {
        transform.position = en;
        Invisible(a);
        vida.Invulnerable(a);
        jugador.isKinematic = a;
        movimientoLibre =!a;
        jugador.Sleep();
        GetComponent<SpriteRenderer>().enabled = !a;
        piernas.Invisible(a);
        LuzConica(!a);
    }

    public bool Invisible()
    {
        return invisible;
    }

    public void Parar()
    {
        jugador.velocity = Vector2.zero;
    }

	public void LuzConica(bool a){
		luz.LuzConica (a);
	}

    public void AumentoVelocidad(float porcentaje)
    {
        velMaxima = porcentaje * velocidadMaxima;
    }

    public Luz Luz()
    {
        return luz;
    }

    public void Atacar()
    {
        if (movimientoLibre)
        {
            animador.SetTrigger("atacando");
            golpe.Golpear(retardoAtaque, duracionAtaque, danyo);
        }
    }

    public void Andar()
    {

    }

    public void Interactuar()
    {
        animador.SetTrigger("interactuando");
    }

    public void Morir(TipoMonstruo tipo)
    {
        animador.SetTrigger("muriendo");
        animador.SetInteger("tipoMonstruo", (int)tipo);
        jugador.Sleep();
        GetComponent<Collider2D>().enabled = false;
        puntero.GetComponent<PunteroRetardo>().Muerto(true);
        Destroy(this);
        GameManager.instance.JugadorMuerto();
        piernas.Invisible(true);
    }

    public float AnguloMovimiento()
    {
        return Mathf.Atan2(direccionMirada.y, direccionMirada.x);
    }

    public float Velocidad()
    {
        return jugador.velocity.sqrMagnitude;
    }

    public float FuerzaEmpujon()
    {
        return fuerzaEmpujon;
    }
    public int Danyo()
    {
        return danyo;
    }
}
