using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Colores;

[CustomEditor(typeof(Puerta))]

public class EditorPuertas : Editor
{
    static Sprite[] sprites;

    private void OnEnable()
    {
        sprites = Resources.LoadAll<Sprite>("Sprites/Tilesheets/Spritesheet");
    }
    private void OnSceneGUI()
    {
        Puerta puerta = target as Puerta;

        puerta.GetComponent<Animator>().SetInteger("Color", (int)puerta.color);
        SpriteRenderer spritePuerta = puerta.GetComponent<SpriteRenderer>();

        string nombre;

        switch (puerta.color)
        {
            case Colores.Colores.Amarillo:
                if (puerta.abierta)
                    nombre = "PuerAmAb";
                else
                    nombre = "PuerAmCer";
                break;
            case Colores.Colores.Azul:
                if (puerta.abierta)
                    nombre = "PuerAzAb";
                else
                    nombre = "PuerAzCer";
                break;
            case Colores.Colores.Rojo:
                if (puerta.abierta)
                    nombre = "PuerRoAb";
                else
                    nombre = "PuerRoCer";
                break;
            case Colores.Colores.Verde:
                if (puerta.abierta)
                    nombre = "PuerVerAb";
                else
                    nombre = "PuerVerCer";
                break;
            case Colores.Colores.Final:
                if (puerta.abierta)
                    nombre = "PuerFin3";
                else
                    nombre = "PuerFin1";
                break;
            default:
                if (puerta.abierta)
                    nombre = "PuerBlaAb";
                else
                    nombre = "PuerBlaCer";
                break;
        }

        int i = 0;
        while (i < sprites.Length && sprites[i].name != nombre)
            i++;
        spritePuerta.sprite = sprites[i];
    }

    
}
