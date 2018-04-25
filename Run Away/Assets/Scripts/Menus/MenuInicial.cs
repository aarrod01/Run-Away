using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour{

	public void NuevaPartida(){
		GameManager.instance.CambiarEscena("Nivel1");
	}

	public void CargarPartida(){
		int[] loadedStats = SaveLoadManager.LoadGame ();

		switch (loadedStats[0])
        {
            case 0:
				Debug.LogError ("Se ha cargado la pantalla de inicio");
                break;
            case 1:
                GameManager.instance.CambiarEscena("Nivel1");
                break;
            case 2:
                GameManager.instance.CambiarEscena("Nivel2");
                break;
        }
		
		//SceneManager.LoadScene (nivel.ToString());
	}

	public void SalirDelJuego(){
		Application.Quit ();
	}

}
