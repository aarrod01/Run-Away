using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorPalanca : MonoBehaviour {

	public static ControladorPalanca instance = null;
    Palanca[] todasLasPalancas;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(gameObject);
        instance.TodasLasPalancas();
    }

    void TodasLasPalancas()
    {
        todasLasPalancas = GameManager.FindObjectsOfType<Palanca>();
    }

    public void EncenderPalancas()
    {
        for(int i =0; i<todasLasPalancas.Length; i++)
        {
            todasLasPalancas[i].Iluminar();
        }
    }

    public void ApagarPalancas()
    {
        for (int i = 0; i < todasLasPalancas.Length; i++)
        {
            todasLasPalancas[i].Apagar();
        }
    }


}
