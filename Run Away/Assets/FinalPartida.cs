using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DynamicLight2D;

public class FinalPartida : MonoBehaviour {

    public string textoFinalBueno;
    public string textoFinalMalo;
    public float tiempoTransicion;
    public Color blanco, negro; 

    Text[] textos;
    DynamicLight luz;
    Button boton;
    float tiempo;
    bool bien;
    
	void Awake () {
        
        textos = GetComponentsInChildren<Text>();
        luz = GetComponentInChildren<DynamicLight>();
        boton = GetComponentInChildren<Button>();
        luz.gameObject.SetActive(false);
        foreach (Text t in textos)
            t.gameObject.SetActive(false);
        boton.gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            Fin(collision.transform);
        }
    }

    public void Fin(Transform tr)
    {
        GameObject.FindObjectOfType<Puntero>().PunteroMenu();
        luz.transform.position = tr.position;
        tiempo = Time.time;
        StartCoroutine(Final());
    }
    IEnumerator Final()
    {
        bien = GameObject.FindObjectsOfType<Monstruo>().Length == 0;
        ColorBlock aux0 = boton.colors;
        aux0.normalColor = bien ? negro : blanco;
        aux0.highlightedColor = (negro + blanco) / 2f;
        aux0.pressedColor = (negro + blanco) / 2f;
        boton.colors = aux0;
        boton.transform.GetChild(0).GetComponent<CambioColorBoton>().ColorAlEntrar = (negro + blanco) / 2f;
        boton.transform.GetChild(0).GetComponent<Text>().color = bien ? negro: blanco;
        luz.LightColor = bien ? blanco : negro;
        foreach (Text t in textos)
        {
            t.color = bien ? negro : blanco;
            if (t.GetComponent<CambioColorBoton>() == null)
                t.text = bien ? textoFinalBueno : textoFinalMalo;
        }
        foreach (Text t in textos)
            t.gameObject.SetActive(true);
        luz.gameObject.SetActive(true);
        GameObject.FindObjectOfType<Jugador>().MovimientoLibre(false);
        while (Time.time - tiempo < tiempoTransicion)
        {
            float por = (Time.time - tiempo) / tiempoTransicion;
            Color aux = textos[0].color;
            luz.LightRadius = por*100f;
            aux.a = por;
            foreach (Text t in textos)
            {
                t.color = aux;
            }
            yield return 0;
        }

        boton.gameObject.SetActive(true);
        yield return null;

    }
}
