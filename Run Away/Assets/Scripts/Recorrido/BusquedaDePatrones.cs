using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Patron;

namespace Patron
{
    enum Direccion { Arriba, Derecha, Abajo, Izquierda };
    enum Contenido { Desconocido, Lleno, Vacio }
    class Patron4
    {
        Contenido[,] mascara;
        public const int tamanyo = 4;
        public Patron4()
        {
            mascara = new Contenido[tamanyo, tamanyo];
            for (int i = 0; i < tamanyo; i++)
            {
                for (int j = 0; j < tamanyo; j++)
                {
                    mascara[i, j] = Contenido.Desconocido;
                }
            }
        }

        public void PatronX(Direccion[] direccionesAbiertas)
        {

            RellenarCentro();
            for (int i = 0; i < direccionesAbiertas.Length; i++)
                RellenarDireccion(direccionesAbiertas[i]);
        }
        public Contenido ContenidoS(int x, int y)
        {
            return mascara[x, y];
        }
        void RellenarDireccion(Direccion direccion)
        {
            switch (direccion)
            {
                case Direccion.Arriba:
                    mascara[1, 0] = Contenido.Vacio;
                    mascara[2, 0] = Contenido.Vacio;
                    if (mascara[1, 3] == Contenido.Desconocido)
                    {
                        mascara[1, 3] = Contenido.Lleno;
                        mascara[2, 3] = Contenido.Lleno;
                    }
                    break;
                case Direccion.Derecha:
                    mascara[3, 1] = Contenido.Vacio;
                    mascara[3, 2] = Contenido.Vacio;
                    if (mascara[0, 1] == Contenido.Desconocido)
                    {
                        mascara[0, 1] = Contenido.Lleno;
                        mascara[0, 2] = Contenido.Lleno;
                    }
                    break;
                case Direccion.Abajo:
                    mascara[1, 3] = Contenido.Vacio;
                    mascara[2, 3] = Contenido.Vacio;
                    if (mascara[1,0] == Contenido.Desconocido)
                    {
                        mascara[1, 0] = Contenido.Lleno;
                        mascara[2, 0] = Contenido.Lleno;
                    }
                    break;
                case Direccion.Izquierda:
                    mascara[0, 1] = Contenido.Vacio;
                    mascara[0, 2] = Contenido.Vacio;
                    if (mascara[3, 1] == Contenido.Desconocido)
                    {
                        mascara[3, 1] = Contenido.Lleno;
                        mascara[3, 2] = Contenido.Lleno;
                    }
                    break;
            }
        }

        public void RellenarCentro()
        {
            for (int i = 1; i < tamanyo - 1; i++)
            {
                for (int j = 1; j < tamanyo - 1; j++)
                {
                    mascara[i, j] = Contenido.Vacio;
                }
            }
        }

    }
}

[RequireComponent(typeof(Tilemap))]
public class BusquedaDePatrones : MonoBehaviour
{

    public GameObject prefab;

    TileBase[] casillas;
    BoundsInt limite;
    Patron4[] patrones;
    Vector2 posicionSuperiorIzquierda;
    Vector2 vectorDesdeEsquinaSuperiorIzquierdaACentro;
    float ancho;
    void Awake()
    {

        patrones = TodosLosPatrones();
        Tilemap aux = GetComponent<Tilemap>();
        limite = aux.cellBounds;
        posicionSuperiorIzquierda = aux.LocalToWorld(new Vector2(aux.localBounds.min.x, aux.localBounds.min.y));
        ancho = aux.LocalToWorld(aux.cellSize).x;
        vectorDesdeEsquinaSuperiorIzquierdaACentro = aux.LocalToWorld((Vector2.right + Vector2.up) * ancho);
        casillas = aux.GetTilesBlock(limite);
        CrearEnCentroPatron(prefab);
    }

    Patron4[] TodosLosPatrones()
    {/*
        Patron4[] ret = new Patron4[1];
        ret[0] = new Patron4();
            ret[0].RellenarCentro();
        */
        Patron4[] ret = new Patron4[13];
        int i = 0;
        Direccion[] direcciones = new Direccion[1];
        while (i < 4)
        {
            direcciones[0] = (Direccion)(i % 4);
            ret[i] = new Patron4();
            ret[i].PatronX(direcciones);
            i++;
        }
        direcciones = new Direccion[2];
        while (i < 8)
        {
            direcciones[0] = (Direccion)(i % 4);
            direcciones[1] = (Direccion)((i + 1) % 4);
            ret[i] = new Patron4();
            ret[i].PatronX(direcciones);
            i++;
        }
        direcciones = new Direccion[3];
        while (i < 12)
        {
            direcciones[0] = (Direccion)(i % 4);
            direcciones[1] = (Direccion)((i + 1) % 4);
            direcciones[1] = (Direccion)((i + 2) % 4);
            ret[i] = new Patron4();
            ret[i].PatronX(direcciones);
            i++;
        }
        direcciones = new Direccion[4];
        direcciones[0] = Direccion.Abajo;
        direcciones[1] = Direccion.Arriba;
        direcciones[2] = Direccion.Derecha;
        direcciones[3] = Direccion.Izquierda;
        ret[i] = new Patron4();
        ret[i].PatronX(direcciones);
        
        return ret;
    }

    bool HayCasilla(int x, int y)
    {
        return null != casillas[x + y * limite.size.x];
    }
    bool EstaElPatronEn(int x, int y, Patron4 patron)
    {
        bool fin = true;
        for (int i = 0; i < Patron4.tamanyo && fin; i++)
        {
            for (int j = 0; j < Patron4.tamanyo && fin; j++)
            {
                if (patron.ContenidoS(i, j) == Contenido.Vacio && HayCasilla(x+i, y+j)
                    || patron.ContenidoS(i, j) == Contenido.Lleno && !HayCasilla(x+i, y+j))
                    fin = false;
            }
        }

        return fin;
    }

    bool EstaAlgunPatronEn(int x, int y)
    {
        int i = 0;
        while (i < patrones.Length && !EstaElPatronEn(x, y, patrones[i]))
            i++;
        return i != patrones.Length;
    }

    public void CrearEnCentroPatron(GameObject prefab)
    {
        GameObject padre = new GameObject();
        padre.name = "Puntos Ruta";
        for (int i = 0; i < limite.size.x-3; i++)
        {
            for (int j = 0; j < limite.size.y-3; j++)
            {
                if (EstaAlgunPatronEn(i, j))
                {
                    GameObject a=
                    Instantiate(prefab, posicionSuperiorIzquierda + Vector2.right * i*ancho + Vector2.up * j*ancho + vectorDesdeEsquinaSuperiorIzquierdaACentro, Quaternion.identity);
                    a.transform.parent = padre.transform;
                    a.name = "("+i + ", " + j+")";
                }
            }
        }
    }
}

