using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Rigidbody2D))]
public class Jugador : MonoBehaviour 
{
	float velMaxima;
	bool invisible = false,
		movimientoLibre = true;

	Rigidbody2D jugador, puntero;
    Vector2 direccionMirada,
            direccionMovimiento;
    Luz luz;

	public float velocidadaxima = 1f,
				factorAceleracion = 0.5f,
				velocidadAngularMaxima = 1f,
				factorAceleracionAngular = 0.5f,
				factorGiro = 0.5f,
				fraccionMinimaVelocidadHaciaDetras = 0.5f,
				vidas = 1;

    void Start () {
        AumentoVelocidad(1f);
        luz = GetComponentInChildren<Luz>();
        jugador = GetComponent<Rigidbody2D>();
        puntero = GameObject.FindObjectOfType<PunteroRetardo>().GetComponent<Rigidbody2D>();
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
        if (anguloPredicho > 180f)
            anguloPredicho -= 360f;
        else if (anguloPredicho < -180f)
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

    public void Esconderse(bool a)
    {
        Parar();
        Invisible(a);
        movimientoLibre=!a;
        GetComponent<Rigidbody2D>().Sleep();
        GetComponent<Collider2D>().enabled = !a;
        GetComponent<SpriteRenderer>().enabled = !a;
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
        velMaxima = porcentaje * velocidadaxima;
    }

    public Luz Luz()
    {
        return luz;
    }

	public void Vida(int danyo)
	{
		vidas = vidas - danyo;
	}
}
