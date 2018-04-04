using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour {

	GameObject luz;

	void Start()
	{
		luz = GameObject.FindGameObjectWithTag ("foco");
	}

	public void ApagarLuz(){
		luz.SetActive (false);
	}
	public void EncenderLuz()
	{
		luz.SetActive (true);
	}
}
