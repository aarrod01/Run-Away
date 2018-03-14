using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternMovement : MonoBehaviour {

	public GameObject parameterGameObject;
	public float incrementoX;
	public float incrementoY;

	Transform transformLantern;	//Transform de Lantern
	Transform transformParameterGameObject;	//Transform del objeto recibido como parámetro

	float posX;
	float posY;

	// Use this for initialization
	void Start () {
		transformLantern = this.GetComponent<Transform>();
		transformParameterGameObject = parameterGameObject.GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		transformLantern.position = transformParameterGameObject.position;
		transformLantern.rotation = transformParameterGameObject.rotation;

	}
}
