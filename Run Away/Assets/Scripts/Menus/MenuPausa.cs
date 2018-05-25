using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour {

	public GameObject panelPausa;
	public GameObject panelOpciones;

	void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
            PausarPartida();
	}

	public void PausarPartida()
    {
        GameObject.FindObjectOfType<Puntero>().PunteroMenu();
        Time.timeScale = 0.0f;
			panelPausa.SetActive (true);
			Debug.Log (Application.persistentDataPath);
			Debug.Log (Application.dataPath);
	}

	public void MenuOpciones ()
    {
        GameObject.FindObjectOfType<Puntero>().PunteroMenu();
        panelPausa.SetActive (false);
		panelOpciones.SetActive (true);
	}

	public void SalirAlMenu ()
    {
        GameObject.FindObjectOfType<Puntero>().PunteroMenu();
        //SalirMenuPausa();
		SaveLoadManager.SaveGame(GameManager.instance);
        GameManager.instance.IrAEscena("Inicio");
	}

    public void SalirOpciones()
    {
        GameObject.FindObjectOfType<Puntero>().PunteroMenu();
        panelOpciones.SetActive(false);
        PausarPartida();
    }

    public void SalirMenuPausa()
    {
        GameObject.FindObjectOfType<Puntero>().PunteroJuego();
        Time.timeScale = 1.0f;
        panelPausa.SetActive(false);
    }

    public void ReiniciarNivel()
    {
        //Cambiar musica si estas con la droga
        SalirMenuPausa();
        GameManager.instance.ResetarEscena();
    }

    public bool PausaActivada()
    {
        if (Time.timeScale == 0.0f)
            return true;
        else
            return false;
    }
}
