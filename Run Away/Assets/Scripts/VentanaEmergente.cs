using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VentanaEmergente: MonoBehaviour {

    public GameObject siguiente;
    public bool ActivadaInicialmente;
    [SerializeField]
    public bool Activada = false;
    public int Numero;
    public string EscenaOrigen;

    void Start()
    {
        Time.timeScale = 0.0f;
        Activada = true;
    }

    public void Siguiente()
    {
        Time.timeScale = 1.0f;
        if (siguiente != null)
            siguiente.SetActive(true);
        gameObject.SetActive(false);
    }
}
