using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour {

	public GameObject panelPausa;
	public GameObject panelOpciones;

    bool salirOpciones = true;

	void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
            PausarPartida();
	}

	public void PausarPartida()
    {
			Time.timeScale = 0.0f;
			panelPausa.SetActive (true);
			Debug.Log (Application.persistentDataPath);
			Debug.Log (Application.dataPath);
	}

	public void MenuOpciones ()
    {
		panelPausa.SetActive (false);
        salirOpciones = false;
		panelOpciones.SetActive (true);
	}

	public void SalirAlMenu ()
    {
		//Guardar
        SalirMenuPausa();
		SaveLoadManager.SaveGame(GameManager.instance);
        GameManager.instance.CambiarEscena("Inicio");
	}

    public void SalirOpciones()
    {
        panelOpciones.SetActive(false);
        salirOpciones = true;
        PausarPartida();
    }

    public void SalirMenuPausa()
    {
        Time.timeScale = 1.0f;
        panelPausa.SetActive(false);
    }

    public void ReiniciarNivel()
    {
        GameManager GM = GameManager.instance;
        GM.ResetarEscena();
    }

    public bool PausaActivada()
    {
        if (Time.timeScale == 0.0f)
            return true;
        else
            return false;
    }
}
