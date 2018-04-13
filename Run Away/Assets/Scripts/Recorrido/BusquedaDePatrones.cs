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

        /*public void PatronX(Direccion[] direccionesAbiertas)
        {

            RellenarCentro();
            for (int i = 0; i < direccionesAbiertas.Length; i++)
                RellenarDireccion(direccionesAbiertas[i]);
        }*/
        public Contenido ContenidoS(int x, int y)
        {
            return mascara[x, y];
        }

        public void RellenarFines(int numero, Contenido contenido)
        {
            numero = numero % 4;
            RellenarSemiLaterales(numero, contenido);
            Vector2Int[] x = new Vector2Int[2] { new Vector2Int(1, numero%2*3),
                new Vector2Int(2, numero%2*3) };
            if(numero>1)
            {
                for (int i = 0; i < x.Length; i++)
                    x[i] = InvertirElementos(x[i]);
            }
            RellenarCasillas(x, contenido);

        }
        public void RellenarEntreIntersecciones(int numero, Contenido contenido)
        {
            numero = numero % 4;
            Vector2Int[] x = new Vector2Int[4] { new Vector2Int(numero % 2 * 3, 0),
                new Vector2Int(numero % 2 * 3, 1),
                new Vector2Int((numero + 1) % 2 * 3, 2),
                new Vector2Int((numero + 1) % 2 * 3, 3) };
            
            if(numero>1)
            {
                for (int i = 0; i < x.Length; i++)
                    x[i] = InvertirElementos(x[i]);
            }

            RellenarCasillas(x, contenido);
        }
        Vector2Int InvertirElementos(Vector2Int x)
        {
            Vector2Int aux = new Vector2Int();
            aux.y = x.x;
            aux.x = x.y;
            return aux;
        }
        public void RellenarSemiLaterales(int numero, Contenido contenido)
        {
            numero = numero % 4;
            switch (numero)
            {
                case 0:
                    RellenarCasilla(new Vector2Int(0, 1), contenido);
                    RellenarCasilla(new Vector2Int(3, 1), contenido);
                    break;
                case 1:
                    RellenarCasilla(new Vector2Int(0, 2), contenido);
                    RellenarCasilla(new Vector2Int(3, 2), contenido);
                    break;
                case 2:
                    RellenarCasilla(new Vector2Int(1, 0), contenido);
                    RellenarCasilla(new Vector2Int(1, 3), contenido);
                    break;
                case 3:
                    RellenarCasilla(new Vector2Int(2, 0), contenido);
                    RellenarCasilla(new Vector2Int(2, 3), contenido);
                    break;
            }
        }

        public void RellenarEsquina(int esquina, Contenido contenido)
        {
            Vector2Int[] total = new Vector2Int[5];
            total[0] = new Vector2Int((esquina % 2) * 3, (esquina / 2 * 3));
            if(total[0].x==0)
            {
                for (int i = 1; i < 3; i++)
                    total[i] = total[0] + Vector2Int.right * i;
            }
            else
            {
                for (int i = 1; i < 3; i++)
                    total[i] = total[0] - Vector2Int.right * i;
            }
            if (total[0].y == 0)
            {
                for (int i = 3; i < 5; i++)
                    total[i] = total[0] + Vector2Int.up * (i-2);
            }
            else
            {
                for (int i = 3; i < 5; i++)
                    total[i] = total[0] - Vector2Int.up * (i-2);
            }
            RellenarCasillas(total, contenido);
        }

        public void RellenarCasillas(Vector2Int[]pos, Contenido contenido)
        {
            for(int i=0; i<pos.Length;i++)
            {
                RellenarCasilla(pos[i], contenido);
            }
        }
        public void RellenarCasilla(Vector2Int pos, Contenido contenido)
        {
            mascara[pos.x, pos.y] = contenido;
        }
        public void RellenarCentro(Contenido contenido)
        {
            for (int i = 1; i < tamanyo - 1; i++)
            {
                for (int j = 1; j < tamanyo - 1; j++)
                {
                    mascara[i, j] = contenido;
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
    Patron4[] patronesGenerales, patronesImposibles, patronesExcepciones;
    Vector2 posicionSuperiorIzquierda;
    Vector2 vectorDesdeEsquinaSuperiorIzquierdaACentro;
    float ancho;
    GameObject padre;

    void Awake()
    {

        patronesGenerales = PatronGeneral();
        patronesImposibles = PatronExcluyente();
        patronesExcepciones = PatronExcepciones();
        Tilemap aux = GetComponent<Tilemap>();
        limite = aux.cellBounds;
        posicionSuperiorIzquierda = aux.LocalToWorld(new Vector2(aux.localBounds.min.x, aux.localBounds.min.y));
        ancho = aux.LocalToWorld(aux.cellSize).x;
        vectorDesdeEsquinaSuperiorIzquierdaACentro = aux.LocalToWorld((Vector2.right + Vector2.up) * ancho);
        casillas = aux.GetTilesBlock(limite);
        CrearEnCentroPatron(prefab);
    }

    Patron4[] PatronGeneral()
    {
        Patron4[] ret = new Patron4[5];
        ret[0] = new Patron4();
            ret[0].RellenarCentro(Contenido.Vacio);
        return ret;
    }
    Patron4[] PatronExcluyente()
    {
        Patron4[] ret = new Patron4[12];
        for (int i = 0; i < 4; i++)
        {
            ret[i] = new Patron4();
            ret[i].RellenarEsquina(i, Contenido.Vacio);

        }
        for(int i = 4;i<8;i++)
        {
            ret[i] = new Patron4();
            ret[i].RellenarSemiLaterales(i, Contenido.Lleno);
        }
        for (int i = 8; i < 12; i++)
        {
            ret[i] = new Patron4();
            ret[i].RellenarEntreIntersecciones(i, Contenido.Vacio);
        }


        return ret;
    }

    Patron4[] PatronExcepciones()
    {
        Patron4[] ret = new Patron4[4];
        for (int i = 0; i < 4; i++)
        {
            ret[i] = new Patron4();
            ret[i].RellenarFines(i, Contenido.Lleno);
        }
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

    bool EvaluarPatrones(int x, int y)
    {
        int i = 0;
        while (i < patronesImposibles.Length && !EstaElPatronEn(x, y, patronesImposibles[i]))
            i++;
        if (i == patronesImposibles.Length)
        {
            i = 0;
            while (i < patronesGenerales.Length && !EstaElPatronEn(x, y, patronesGenerales[i]))
                i++;
            return i != patronesGenerales.Length;
        }
        else
        {
            i = 0;
            while (i < patronesExcepciones.Length && !EstaElPatronEn(x, y, patronesExcepciones[i]))
                i++;
            return i < patronesExcepciones.Length; ;
        }
    }

    public void CrearEnCentroPatron(GameObject prefab)
    {
        padre = new GameObject();
        padre.name = "Puntos Ruta";
        for (int i = 0; i < limite.size.x-3; i++)
        {
            for (int j = 0; j < limite.size.y-3; j++)
            {
                if (EvaluarPatrones(i, j))
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

