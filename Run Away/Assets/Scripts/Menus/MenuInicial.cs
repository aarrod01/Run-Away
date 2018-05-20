using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    public GameObject menuPrincipal, menuOpciones;

    void Start()
    {
        menuPrincipal.SetActive(true);
    }

	public void NuevaPartida()
    {
		GameManager.instance.CambiarEscena("Nivel1");
	}

	public void CargarPartida(){
		int[] loadedStats = SaveLoadManager.LoadGame ();

		switch (loadedStats [0]) {
		case 0:
			break;
		case 1:
			GameManager.instance.CambiarEscena ("Nivel1");
			break;
		case 2:
			GameManager.instance.CambiarEscena ("Nivel2");
			break;
		}
		//SceneManager.LoadScene (nivel.ToString());
	}

    public void EntrarOpciones()
    {
        menuPrincipal.SetActive(false);
        menuOpciones.SetActive(true);
    }

    public void SalirOpciones()
    {
        menuPrincipal.SetActive(true);
        menuOpciones.SetActive(false);
    }

	public void SalirDelJuego(){
		Application.Quit ();
	}

}
