using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Pointer : MonoBehaviour {

    Interactuable objetoInteractuable = null;
    PlayerMovement player;
    Rigidbody2D puntero;
    public float lerp = 0.5f;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        puntero = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        puntero.MovePosition(Vector2.Lerp(puntero.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), lerp));
        if (Input.GetMouseButtonDown(0) && objetoInteractuable != null)
            objetoInteractuable.Click(player);

	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Interactuable aux = collision.gameObject.GetComponent<Interactuable>();
        if (aux != null)
            objetoInteractuable = aux; ;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (objetoInteractuable != null && objetoInteractuable.gameObject == collision.gameObject)
            objetoInteractuable = null;
    }
}
