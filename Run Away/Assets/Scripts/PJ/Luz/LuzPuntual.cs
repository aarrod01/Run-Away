using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DynamicLight2D;

public class LuzPuntual : MonoBehaviour {
    
    DynamicLight[] luces;
    CircleCollider2D circuloColision;
    float radio;

    private void Start()
    {
        luces = GetComponentsInChildren<DynamicLight>();
        circuloColision = GetComponent<CircleCollider2D>();
        radio = luces[0].LightRadius;
        for(int i =1; i<luces.Length; i++)
        {
            luces[i].LightRadius = radio;
        }
        circuloColision.radius = radio;
    }

    public void Activa(bool a)
    {
        gameObject.SetActive(a);
    }


    public void Radio(float porcentaje)
    {
        float _radio = radio * porcentaje;
        circuloColision.radius = _radio;
        for (int i = 0; i < luces.Length; i++)
        {
            luces[i].LightRadius = _radio;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Monstruo aux;
        if((aux=collision.GetComponent<Monstruo>())!=null)
        {
            aux.EntrandoLuz();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Monstruo aux;
        if ((aux = collision.GetComponent<Monstruo>()) != null)
        {
            aux.SaliendoLuz();
        }
    }
}
