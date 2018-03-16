using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void funcionInteractuado(PlayerMovement a);
[RequireComponent(typeof(Rigidbody2D))]
public class Interactuable : MonoBehaviour {
    public funcionInteractuado Click = (PlayerMovement a)=> { };
}
