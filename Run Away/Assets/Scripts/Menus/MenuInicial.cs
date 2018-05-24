using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Guardado;

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
		SceneData loadedStats = SaveLoadManager.LoadGame ();
        GameManager.instance.CambiarEscena (loadedStats.nivel);
        GameManager.instance.NumeroDeMuertes = loadedStats.numeroDeMuertes;
        GameManager.instance.drogaConsumida = loadedStats.numeroDeDroga;
        GameManager.instance.monstruosHuidos = loadedStats.monstruosHuidos;
        GameManager.instance.monstruosIgnorados = loadedStats.monstruosIgnorados;
        GameManager.instance.monstruosMuertos = loadedStats.monstruosMuertos;
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
