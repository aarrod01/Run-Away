using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DynamicLight2D;

public class LuzConica : MonoBehaviour {

    DynamicLight luz;
    CircleCollider2D circuloColision;
    float radio, angulo;

    private void Start()
    {
        luz = GetComponentInChildren<DynamicLight>();
        circuloColision = GetComponent<CircleCollider2D>();
        radio = luz.LightRadius;
        angulo = luz.RangeAngle;
        circuloColision.radius = radio;
    }

    public void Activa(bool a)
    {
        gameObject.SetActive(a);
    }


    public void Largo(float porcentaje)
    {
        float _radio = radio * porcentaje;
        circuloColision.radius = _radio;
        luz.LightRadius = _radio;
    }

    public void Ancho(float porcentaje)
    {
        float _angulo = angulo * porcentaje;
        luz.RangeAngle = _angulo;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Monstruo aux;
        if ((aux = collision.GetComponent<Monstruo>()) != null)
        {
            Vector2 posMon = aux.GetComponent<Rigidbody2D>().position;
            if(Mathf.Abs(Vector2.Angle(transform.up, posMon-(Vector2)transform.position)) < angulo/2f)
                aux.EntrandoLuz();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Monstruo aux;
        if ((aux = collision.GetComponent<Monstruo>()) != null)
        {
            Vector2 posMon = aux.GetComponent<Rigidbody2D>().position;
            if (Mathf.Abs(Vector2.Angle(Vector2.up, posMon - (Vector2)transform.position)) < angulo / 2f)
                aux.SaliendoLuz();
        }
    }
}
