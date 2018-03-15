using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void funcionInteractuado();
[RequireComponent(typeof(Rigidbody2D))]
public class Interactuable : MonoBehaviour {
    
    public funcionInteractuado Click = ()=> { };

}
