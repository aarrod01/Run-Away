using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour{

	public GameManager gm;

	public void NuevaPartida(){
		gm.CambiarEscena("Nivel1");
	}

	public void CargarPartida(){
		int[] loadedStats = SaveLoadManager.LoadGame ();

		string nivel = loadedStats [0].ToString ();
		gm.CambiarEscena (nivel);
		//SceneManager.LoadScene (nivel.ToString());
	}

	public void SalirDelJuego(){
		Application.Quit ();
	}

}
