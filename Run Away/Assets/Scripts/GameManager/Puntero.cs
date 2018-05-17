using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Puntero : MonoBehaviour 
{
	public static Puntero instance = null;
	public Texture2D punterojuego, punteroFueraJuego;

    MenuPausa menuPausa;
    bool enJuego;

	void Awake () 
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
		else
			Destroy(this.gameObject);
        instance.Inicio();
	}

	void Inicio () 
	{
		Scene scene = SceneManager.GetActiveScene ();

		//Hacer modificacion en un futuro cuando esté el menu de juego.
		if (scene.name == "Nivel1" || scene.name == "Nivel2" || scene.name == "NivelFinal") 
		{
			Cursor.visible = true;
			Cursor.SetCursor (punterojuego, Vector2.one * (((float)punterojuego.width) / 2f), CursorMode.Auto);
            menuPausa = GameObject.Find("Camara").GetComponent<MenuPausa>();
            enJuego = true;
		}
        else if (scene.name == "Inicio")
        {
            enJuego = false;
            Cursor.visible = true;
            Cursor.SetCursor(punteroFueraJuego, Vector2.zero, CursorMode.Auto);
        }
        else
        {
            enJuego = false;
            Cursor.visible = false;
        }
	}

    void Update()
    {
        if (enJuego)
            if (menuPausa.PausaActivada() == true)
                Cursor.SetCursor(punteroFueraJuego, Vector2.zero, CursorMode.Auto);
            else
                Cursor.SetCursor(punterojuego, Vector2.one * (((float)punterojuego.width) / 2f), CursorMode.Auto);
    }
}
