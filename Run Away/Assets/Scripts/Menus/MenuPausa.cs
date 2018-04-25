using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour {

	public Canvas cv;
	public GameManager gm;

	void Update(){
		if (Input.GetKeyDown ("p"))
			PausarPartida ();
	}

	public void PausarPartida(){
		if (Time.timeScale == 1.0f) {
			Time.timeScale = 0.0f;
			cv.gameObject.SetActive (true);
		} else {
			Time.timeScale = 1.0f;
			cv.gameObject.SetActive (false);
		}
	}

	public void SalirAlMenu (){
		//Guardar
		SaveLoadManager.SaveGame(gm);
		SceneManager.LoadScene ("Inicio");
	}
}
