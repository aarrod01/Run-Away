using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Colores;

[CustomEditor(typeof(Puerta))]

public class EditorPuertas : Editor
{
    private void OnSceneGUI()
    {
        Puerta puerta = target as Puerta;
        puerta.GetComponent<Animator>().SetInteger("Color", (int)puerta.color);
        SpriteRenderer spritePuerta = puerta.GetComponent<SpriteRenderer>();

       switch (puerta.color)
        {
           case Colores.Colores.Amarillo:
                if (puerta.abierta)
                    spritePuerta.sprite = (Sprite)Resources.Load("Sprites/Tilesheets/PuerAmAb");
                else
                    spritePuerta.sprite = (Sprite)Resources.Load("Sprites/Tilesheets/PuerAmCer");
                break;
           case Colores.Colores.Azul:
                if (puerta.abierta)
                    spritePuerta.sprite = (Sprite)Resources.Load("Sprites/Tilesheets/PuerAzAb");
                else
                    spritePuerta.sprite = (Sprite)Resources.Load("Sprites/Tilesheets/PuerAzCer");
                break;
           case Colores.Colores.Rojo:
                if (puerta.abierta)
                    spritePuerta.sprite = (Sprite)Resources.Load("Sprites/Tilesheets/PuerRoAb");
                else
                    spritePuerta.sprite = (Sprite)Resources.Load("Sprites/Tilesheets/PuerRoCer");
                break;
           case Colores.Colores.Verde:
                if (puerta.abierta)
                    spritePuerta.sprite = (Sprite)Resources.Load("Sprites/Tilesheets/PuerVerAb");
                else
                    spritePuerta.sprite = (Sprite)Resources.Load("Sprites/Tilesheets/PuerVerCer");
                break;
           case Colores.Colores.Final:
                if (puerta.abierta)
                    spritePuerta.sprite = (Sprite)Resources.Load("Sprites/Tilesheets/PuerFin3");
                else
                    spritePuerta.sprite = (Sprite)Resources.Load("Sprites/Tilesheets/PuerFin1");
               break;
            }
    }
}
