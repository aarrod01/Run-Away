﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour {

	public Canvas cv;

	void Update(){
		if (Input.GetKeyDown ("p"))
			PausarPartida ();

	}

	public void PausarPartida(){
		if (Time.timeScale == 1.0f) {
			Time.timeScale = 0.0f;
			cv.gameObject.SetActive (true);
			Debug.Log (Application.persistentDataPath);
			Debug.Log (Application.dataPath);
		} else {
			Time.timeScale = 1.0f;
			cv.gameObject.SetActive (false);
		}
	}

	public void SalirAlMenu (){
		//Guardar
		Time.timeScale = 1.0f;
		cv.gameObject.SetActive (false);
		SaveLoadManager.SaveGame(GameManager.instance);
		GameManager.instance.CambiarEscena ("Inicio");
	}
}