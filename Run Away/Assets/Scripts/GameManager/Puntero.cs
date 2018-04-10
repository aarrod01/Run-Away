using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Puntero : MonoBehaviour 
{
	public static Puntero instance = null;
	public Texture2D punterojuego, punteroFueraJuego;

	void Start () 
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
		else
			Destroy(this.gameObject);
		
	}

	void Update () 
	{
		Scene scene = SceneManager.GetActiveScene ();

		//Hacer modificacion en un futuro cuando esté el menu de juego.
		if (scene.name == "Nivel1" || scene.name == "Nivel2" || scene.name == "NivelFinal" || scene.name == "Demo") 
		{
			Cursor.visible = true;
			Cursor.SetCursor (punterojuego, Vector2.zero, CursorMode.Auto);
		} 
		else if (scene.name == "MenuPrincipal") 
		{
			Cursor.visible = true;
			Cursor.SetCursor (punteroFueraJuego, Vector2.zero, CursorMode.Auto);
		}
		else
			Cursor.visible = false;
	}
}
