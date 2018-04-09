using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Rigidbody2D))]
public class PlayerMovement : MonoBehaviour 
{
	float velMaxima;
	bool invisible = false;
	bool movimientoLibre = true;

	Rigidbody2D player, puntero;
    Vector2 direccionMirada,
            direccionMovimiento;
    Lantern luz;

	public float velocidadaxima = 1f,
				factorAceleracion = 0.5f,
				velocidadAngularMaxima = 1f,
				factorAceleracionAngular = 0.5f,
				factorGiro = 0.5f,
				fraccionMinimaVelocidadHaciaDetras = 0.5f;

    void Start () {
        AumentoVelocidad(1f);
        luz = GetComponentInChildren<Lantern>();
        player = GetComponent<Rigidbody2D>();
        puntero = GameObject.FindWithTag("Pointer").GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if (movimientoLibre)
        {
            Giro();
            Movimiento();
        }
	}

    void Giro()
    {
        float velocidadAngularPredicha;
        Vector2 direccionMovimientoObjetivo = puntero.position - player.position;
        float anguloPredicho = Vector2.SignedAngle(Vector2.up, direccionMovimientoObjetivo)- player.rotation;
        if (anguloPredicho > 180f)
            anguloPredicho -= 360f;
        else if (anguloPredicho < -180f)
            anguloPredicho += 360f;
        velocidadAngularPredicha = (anguloPredicho)/Time.fixedDeltaTime;
        player.angularVelocity = Mathf.Lerp(player.angularVelocity,Mathf.Max(Mathf.Min(velocidadAngularMaxima, velocidadAngularPredicha),-velocidadAngularMaxima),factorAceleracionAngular);

        direccionMirada = new Vector2(-Mathf.Sin(player.rotation * Mathf.PI/180), Mathf.Cos(player.rotation*Mathf.PI/180));
    }

    void Movimiento()
    {
        //Obtenemos el vector de movimiento
        direccionMovimiento=(new Vector2 (1,0))*Input.GetAxis("Horizontal") 
            + (new Vector2(0, 1)) * Input.GetAxis("Vertical");

        //Por si se juega con mando, para no tener un vector mayor que 1.
        if (direccionMovimiento.sqrMagnitude>1)
            direccionMovimiento.Normalize();

        Vector2 velocidad = player.velocity;

        velocidad = Vector2.Lerp(velocidad, direccionMovimiento * velMaxima
                * ((direccionMovimiento + direccionMirada).sqrMagnitude * (1 - fraccionMinimaVelocidadHaciaDetras) + fraccionMinimaVelocidadHaciaDetras)
                , factorAceleracion);


        player.velocity = velocidad;
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
    public bool Invisible()
    {
        return invisible;
    }
    public void Parar()
    {
        player.velocity = Vector2.zero;
    }
	public void ApagarLuzConica(){
		luz.ApagarLuzConica ();
	}
	public void EncenderLuzConica()
	{
		luz.EncenderLuzConica ();
	}

    public void AumentoVelocidad(float porcentaje)
    {
        velMaxima = porcentaje * velocidadaxima;
    }

    public Lantern Luz()
    {
        return luz;
    }
}
