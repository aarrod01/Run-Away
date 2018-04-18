using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorPalanca : MonoBehaviour {

	public static ControladorPalanca instante = null;
    Palanca[] todasLasPalancas;

    void Start()
    {
        if (instante == null)
        {
            instante = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(gameObject);
        SceneManager.activeSceneChanged += TodasLasPalancas;
    }

    void TodasLasPalancas(Scene actual, Scene posterior)
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
