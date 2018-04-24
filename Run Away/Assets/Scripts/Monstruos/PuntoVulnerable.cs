using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monstruos;

public class PuntoVulnerable : MonoBehaviour {
    Vida vida;

    private void Start()
    {
        vida = GetComponentInParent<Vida>();
    }

    public void Danyar(int danyo)
    {
        vida.Danyar(danyo, TipoMonstruo.Ninguno);
    }
}
