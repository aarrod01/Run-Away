using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Colores;

[CustomEditor(typeof(Palanca))]

public class EditorPalancas : Editor
{
    static Sprite[] sprites;

    private void OnEnable()
    {
        sprites = Resources.LoadAll<Sprite>("Sprites/Tilesheets/Spritesheet");
    }
    private void OnSceneGUI()
    {
        Palanca palanca = target as Palanca;

        SpriteRenderer spritePalanca = palanca.GetComponent<SpriteRenderer>();

        string nombre;


        switch (palanca.color)
        {
            case Colores.Colores.Amarillo:
                if (palanca.posicionInicial)
                    nombre = "PalAmAct";
                else
                    nombre = "PalAmDes";
                break;
            case Colores.Colores.Azul:
                if (palanca.posicionInicial)
                    nombre = "PalAzAct";
                else
                    nombre = "PalAzDes";
                break;
            case Colores.Colores.Rojo:
                if (palanca.posicionInicial)
                    nombre = "PalRoAct";
                else
                    nombre = "PalRoDes";
                break;
            case Colores.Colores.Verde:
                if (palanca.posicionInicial)
                    nombre = "PalVerAct";
                else
                    nombre = "PalVerDes";
                break;
            default:
                if (palanca.posicionInicial)
                    nombre = "PalBlaAct";
                else
                    nombre = "PalBlaDes";
                break;
        }

        int i = 0;
        while (i < sprites.Length && sprites[i].name != nombre)
            i++;
        spritePalanca.sprite = sprites[i];
    }


}
