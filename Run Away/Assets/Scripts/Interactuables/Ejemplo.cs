using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactuable))]

public class Ejemplo : MonoBehaviour 
{
    Interactuable master;

	void Start () 
	{
        master = GetComponent<Interactuable>();
        master.Click = (PlayerMovement a) => { /*Cosa que hace al interactuar*/ };
	}
	
}
