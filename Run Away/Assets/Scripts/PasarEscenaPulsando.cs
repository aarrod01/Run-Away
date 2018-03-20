using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PasarEscenaPulsando : MonoBehaviour {
	public string escena;
		
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("space"))
			SceneManager.LoadScene (escena);
	}
}
