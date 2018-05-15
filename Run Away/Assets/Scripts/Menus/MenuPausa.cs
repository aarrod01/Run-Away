using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour {

	public GameObject panelPausa;
	public GameObject panelOpciones;

	void Update(){
		if (Input.GetKeyDown("p")  || Input.GetKeyDown(KeyCode.Escape))
            PausarPartida();
        else if (Input.GetKeyDown(KeyCode.Escape)&&Time.timeScale==0.0f)
            SalirAlMenu();

	}

	public void PausarPartida(){
		if (Time.timeScale == 1.0f) {
			Time.timeScale = 0.0f;
			panelPausa.SetActive (true);
			Debug.Log (Application.persistentDataPath);
			Debug.Log (Application.dataPath);
		} else {
			Time.timeScale = 1.0f;
			panelPausa.SetActive (false);
		}
	}

	public void MenuOpciones (){
		panelPausa.SetActive (false);
		panelOpciones.SetActive (true);
	}

	public void SalirAlMenu (){
		//Guardar
		Time.timeScale = 1.0f;
		panelPausa.SetActive (false);
		SaveLoadManager.SaveGame(GameManager.instance);
        GameManager.instance.CambiarEscena("Inicio");
	}
}
