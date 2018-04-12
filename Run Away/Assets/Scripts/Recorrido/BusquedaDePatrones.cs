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

        public void PatronX(Direccion[] direccionesAbiertas, Contenido tipoDeRelleno)
        {

            RellenarCentro(tipoDeRelleno);
            for (int i = 0; i < direccionesAbiertas.Length; i++)
                RellenarDireccion(direccionesAbiertas[i], tipoDeRelleno);
        }
        public Contenido ContenidoS(int x, int y)
        {
            return mascara[x, y];
        }
        void RellenarDireccion(Direccion direccion, Contenido tipoDeRelleno)
        {
            switch (direccion)
            {
                case Direccion.Arriba:
                    mascara[1, 0] = tipoDeRelleno;
                    mascara[2, 0] = tipoDeRelleno;
                    break;
                case Direccion.Derecha:
                    mascara[3, 1] = tipoDeRelleno;
                    mascara[3, 2] = tipoDeRelleno;
                    break;
                case Direccion.Abajo:
                    mascara[1, 3] = tipoDeRelleno;
                    mascara[2, 3] = tipoDeRelleno;
                    break;
                case Direccion.Izquierda:
                    mascara[0, 1] = tipoDeRelleno;
                    mascara[0, 2] = tipoDeRelleno;
                    break;
            }
        }

        void RellenarCentro(Contenido tipoDeRelleno)
        {
            for (int i = 1; i < tamanyo - 1; i++)
            {
                for (int j = 1; j < tamanyo - 1; j++)
                {
                    mascara[i, j] = tipoDeRelleno;
                }
            }
        }

    }
}

[RequireComponent(typeof(Tilemap))]
public class BusquedaDePatrones : MonoBehaviour
{

    public GameObject prefab;

    Tile[] casillas;
    BoundsInt limite;
    Patron4[] patrones;
    Vector2 posicionSuperiorIzquierda;
    Vector2 vectorDesdeEsquinaSuperiorIzquierdaACentro;
    void Awake()
    {

        patrones = TodosLosPatrones();
        Tilemap aux = GetComponent<Tilemap>();
        limite = aux.cellBounds;
        posicionSuperiorIzquierda = new Vector2(limite.xMin, limite.yMax);
        vectorDesdeEsquinaSuperiorIzquierdaACentro = (Vector2.right + Vector2.down) * aux.layoutGrid.cellSize.x * (1.5f);
        casillas = (Tile[])aux.GetTilesBlock(limite);
        CrearEnCentroPatron(prefab);
    }

    Patron4[] TodosLosPatrones()
    {
        Patron4[] ret = new Patron4[13];
        int i = 0;
        Direccion[] direcciones = new Direccion[1];
        while (i < 4)
        {
            direcciones[0] = (Direccion)(i % 4);
            ret[i] = new Patron4();
            ret[i].PatronX(direcciones, Contenido.Vacio);
            i++;
        }
        direcciones = new Direccion[2];
        while (i < 8)
        {
            direcciones[0] = (Direccion)(i % 4);
            direcciones[1] = (Direccion)((i + 1) % 4);
            ret[i] = new Patron4();
            ret[i].PatronX(direcciones, Contenido.Vacio);
            i++;
        }
        direcciones = new Direccion[3];
        while (i < 12)
        {
            direcciones[0] = (Direccion)(i % 4);
            direcciones[1] = (Direccion)((i + 1) % 4);
            direcciones[1] = (Direccion)((i + 2) % 4);
            ret[i] = new Patron4();
            ret[i].PatronX(direcciones, Contenido.Vacio);
            i++;
        }
        direcciones = new Direccion[3];
        direcciones[0] = Direccion.Abajo;
        direcciones[1] = Direccion.Arriba;
        direcciones[2] = Direccion.Derecha;
        direcciones[3] = Direccion.Izquierda;
        ret[i] = new Patron4();
        ret[i].PatronX(direcciones, Contenido.Vacio);

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
                if (patron.ContenidoS(i, j) == Contenido.Vacio && HayCasilla(i, j)
                    || patron.ContenidoS(i, j) == Contenido.Lleno && !HayCasilla(i, j))
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

        for (int i = 0; i < limite.size.x; i++)
        {
            for (int j = 0; j < limite.size.y; j++)
            {
                if (EstaAlgunPatronEn(i, j))
                {
                    Instantiate(prefab, posicionSuperiorIzquierda + Vector2.right * i + Vector2.down * j + vectorDesdeEsquinaSuperiorIzquierdaACentro, Quaternion.identity);
                }
            }
        }
    }
}

