using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DynamicLight2D;

public class LuzDroga : MonoBehaviour {

    public float rMin,rMax,gMin,gMax,bMin,bMax, velocidadCambio;
    bool drogado = false;
    DynamicLight luz;

    void Start()
    {
        luz = GetComponent<DynamicLight>();
        StartCoroutine(Luz());
    }

    public void Luces(bool drug)
    {
        luz.LightColor = new Color(Random.Range(rMin, rMax), Random.Range(rMin, rMax), Random.Range(bMin, bMax), 1.0f);
        drogado = drug;
    }
    IEnumerator Luz()
    {
        while(true)
        {
            if (drogado)
            {
                Color aux = luz.LightColor;
                aux.r = Mathf.Min(rMax, Mathf.Max(rMin, aux.r + Random.Range(-1f, 1f) * Time.deltaTime * velocidadCambio));
                aux.g = Mathf.Min(gMax, Mathf.Max(gMin, aux.r + Random.Range(-1f, 1f) * Time.deltaTime * velocidadCambio));
                aux.b = Mathf.Min(bMax, Mathf.Max(bMin, aux.b + Random.Range(-1f, 1f) * Time.deltaTime * velocidadCambio));
                luz.LightColor = aux;
            }  
            else
                luz.LightColor = Color.black;
            yield return 0;
        }
    }
}
