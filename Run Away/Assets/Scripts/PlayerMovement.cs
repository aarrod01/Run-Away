using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Rigidbody2D))]
public class PlayerMovement : MonoBehaviour 
{
	public float velocidadMaxima = 1f,
				factorAceleracion = 0.5f,
				factorGiro = 0.5f,
				fraccionMinimaVelocidadHaciaDetras = 0.5f;
	public LayerMask conQueColisiona;

	Rigidbody2D player, puntero;
    Vector2 direccionMirada,
			direccionMovimiento;
    Lantern luz;

    bool invisible = false;
    bool movimientoLibre = true;

    // Use this for initialization
    void Start () {
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
	/*
    private void OnTriggerStay2D(Collider2D collision)
    {
        if((collision.tag=="Obstaculo"|| collision.tag == "MedioObstaculo" )&& movimientoLibre)
        {
            RaycastHit2D[] resultados= new RaycastHit2D[1];
            GetComponent<Collider2D>().Raycast(player.position, resultados,conQueColisiona);
            player.velocity = player.velocity - resultados[0].normal * 5;
        }
    }
*/
    public void MovimientoLibre(bool variable)
    {
        movimientoLibre = variable;
    }
    public void Invisible(bool a) 
	{

        luz.gameObject.SetActive(!a);
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
}
