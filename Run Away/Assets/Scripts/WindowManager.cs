using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WindowManager : MonoBehaviour {

    public static WindowManager instance = null;

    VentanaEmergente[] ventanas;

	// Use this for initialization
	void Awake () {
        if (instance == null)
        {
            ventanas = GameObject.FindObjectsOfType<VentanaEmergente>();
            for (int i = 0; i < ventanas.Length; i++)
            {
                DontDestroyOnLoad(ventanas[i].gameObject.transform.root);
                ventanas[i].EscenaOrigen = SceneManager.GetActiveScene().name;
                ventanas[i].Numero = i;
            }
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
        instance.Iniciar();
	}

    void Iniciar()
    {
        if(ventanas.Length!=0&& ventanas[0].EscenaOrigen==SceneManager.GetActiveScene().name)
        {
            VentanaEmergente[] ventanasAux = GameObject.FindObjectsOfType<VentanaEmergente>();
            for(int i =0; i<ventanasAux.Length;i++)
            {
                int j = 0;
               
                while (j < ventanas.Length && ventanas[j].name!=ventanas[i].name)
                    j++;
                if (j != ventanas.Length && ventanas[j]!=ventanasAux[i])
                {
                    Destroy(ventanasAux[i].gameObject);
                }
                ventanas[j].gameObject.SetActive(ventanas[j].ActivadaInicialmente && !ventanas[j].Activada);
            }
        }
        else
        {
            if ( ventanas.Length!=0)
            {
                Destroy(ventanas[0].gameObject.transform.root.gameObject);
            }

            ventanas = GameObject.FindObjectsOfType<VentanaEmergente>();
            for (int i = 0; i < ventanas.Length; i++)
            {
                DontDestroyOnLoad(ventanas[i].gameObject.transform.root);
                ventanas[i].EscenaOrigen = SceneManager.GetActiveScene().name;
                ventanas[i].Numero = i;
                ventanas[i].gameObject.SetActive(ventanas[i].ActivadaInicialmente && !ventanas[i].Activada);
            }
        }
        Time.timeScale = 1.0f;
    }
	
}
