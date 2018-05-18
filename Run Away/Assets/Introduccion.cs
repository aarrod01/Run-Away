using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Introduccion : MonoBehaviour {

    void Awake()
    {
        if (GameManager.instance.NumeroDeMuertes > 0)
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);
    }

    public void Desactivar()
    {
        gameObject.SetActive(false);
    }
}
