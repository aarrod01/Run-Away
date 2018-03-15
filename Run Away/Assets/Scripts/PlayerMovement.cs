using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {

    Rigidbody2D player;
    public float velocidadMaxima = 1f;
    Vector2 direccionMirada;
    Vector2 direccionMovimiento;
    public float factorAceleracion = 0.5f;
    public float factorGiro = 0.5f;
    public float fraccionMinimaVelocidadHaciaDetras = 0.5f;
    Rigidbody2D puntero;

    

    bool movimientoLibre = true;

    // Use this for initialization
    void Start () {
        player = GetComponent<Rigidbody2D>();
        puntero = GameObject.FindWithTag("Pointer").GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        Giro();
        if(movimientoLibre)
            Movimiento();
	}

    void Giro()
    {

        Vector2 direccionMovimientoObjetivo = puntero.position - player.position;

        player.rotation = (Mathf.LerpAngle(player.rotation, Vector2.SignedAngle(Vector2.up, direccionMovimientoObjetivo), factorGiro));

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

        velocidad = Vector2.Lerp(velocidad, direccionMovimiento * velocidadMaxima
                * ((direccionMovimiento + direccionMirada).sqrMagnitude * (1 - fraccionMinimaVelocidadHaciaDetras) + fraccionMinimaVelocidadHaciaDetras)
                , factorAceleracion);


        player.velocity = velocidad;
    }

    public void MovimientoLibre(bool variable)
    {
        movimientoLibre = variable;
    }
}
