using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void funcionInteractuado(PlayerMovement a);
public class Interactuable : MonoBehaviour 
{
    public funcionInteractuado Click = (PlayerMovement a)=> { };
}
