using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Puntero : MonoBehaviour 
{
	public static Puntero instance = null;
	public Texture2D punterojuego, punteroFueraJuego;

	void Awake () 
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
		else
			Destroy(this.gameObject);
	}

	void Start () 
	{
		Scene scene = SceneManager.GetActiveScene ();

		//Hacer modificacion en un futuro cuando esté el menu de juego.
		if (scene.name == "Nivel1" || scene.name == "Nivel2" || scene.name == "NivelFinal" || scene.name == "Nivel3") 
		{
			Cursor.visible = true;
			Cursor.SetCursor (punterojuego, Vector2.one * (((float)punterojuego.width) / 2f), CursorMode.Auto);
		} 
		else if (scene.name == "Inicio") 
		{
			Cursor.visible = true;
			Cursor.SetCursor (punteroFueraJuego, Vector2.one * (((float)punteroFueraJuego.width) / 2f), CursorMode.Auto);
		}
		else
			Cursor.visible = false;
	}
}
