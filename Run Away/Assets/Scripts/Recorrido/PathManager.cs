using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Recorrido;


//Espacio de nombres para los nodos de los recorridos
namespace Recorrido
{
    public class listaNodos
    {
        const float MARGEN = 0.01f;
        public listaNodos siguiente;
        public Vector2 este;

        public listaNodos()
        {
            siguiente = null;
            este = Vector2.negativeInfinity;
        }

        public void ponerNodo(Vector2 pos)
        {
            if (este.Equals(Vector2.negativeInfinity))
                este = pos;
            else
            {
                listaNodos aux = new listaNodos();
                aux.este = este;
                aux.siguiente = siguiente;
                este = pos;
                siguiente = aux;
            }


        }

        public Vector2 QuitarNodo()
        {
            if (siguiente == null)
                este = Vector2.negativeInfinity;
            este = siguiente.este;
            siguiente = siguiente.siguiente;
            return este;
        }
        //metodo que devulve la primera posicion de la lista, si el vector2 esta  a menos de margen unidades la lista pasa a apuntar al elemento siguiente.
        public Vector2 PosicionObjetivo(Vector2 posOrigen)
        {
            if ((este - posOrigen).sqrMagnitude < MARGEN)
            {
                if (siguiente == null)
                    return Vector2.positiveInfinity;
                else
                {
                    siguiente = siguiente.siguiente;
                    return este = siguiente.este;
                }
            }
            return este;
        }
    }
}
public class PathManager : MonoBehaviour
{
    //Singleton
    public static PathManager instance = null;
    //guarda todos los puntos del grafo
    static PuntoRecorrido[] puntosTotales;
    public LayerMask conQueColisiona;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);

        //Busca todos los nodos del grafo.
        GameObject[] auxiliar = GameObject.FindGameObjectsWithTag("Path");
        puntosTotales = new PuntoRecorrido[auxiliar.Length];
        for (int i = 0; i < puntosTotales.Length; i++)
        {
            puntosTotales[i] = auxiliar[i].GetComponent<PuntoRecorrido>();
        }
        ReiniciarRed();

        NodoRecorrido a = new NodoRecorrido(null, puntosTotales[0], null, 0, 0);
        ColaNodos cola = new ColaNodos(a, null);
        for (int i = 1; i < puntosTotales.Length; i++)
            cola.IntroducirNodo(new NodoRecorrido(null, puntosTotales[i], null, 0, 0));
    }
    //Reinicia las conexiones entre puntos
    public void ReiniciarRed()
    {
        for (int i = 0; i < puntosTotales.Length; i++)
        {
            puntosTotales[i].ReiniciarContactos();
        }
    }

    class NodoRecorrido
    {
        public NodoRecorrido padre;
        public PuntoRecorrido este;
        public NodoRecorrido hijo;
        //Coste actual.
        public float g = 0;
        //Coste estimado
        public float f = 0;

        public NodoRecorrido(NodoRecorrido _padre, PuntoRecorrido _este, NodoRecorrido _hijo, float _g, float _f)
        {
            padre = _padre;
            este = _este;
            hijo = _hijo;
            g = _g;
            f = _f;
        }

        //Metodo que devuleve los hijos del nodo que no sean el padre, calcula su coste actual y el estimado
        public NodoRecorrido[] Hijos(PuntoRecorrido[]destino)
        {
            NodoRecorrido[] hijos = null;
            int numeroHijos;
            PuntoRecorrido[] aux = este.PuntosConectados();
            if (padre == null)
                numeroHijos = aux.Length;
            else
                numeroHijos = aux.Length - 1;
            hijos = new NodoRecorrido[numeroHijos];
            int indiceHijos = 0;

            for (int i = 0; i < aux.Length; i++)
                if (padre == null || aux[i] != padre.este)
                {
                    float g_ = aux[i].DistanciaHasta(este.EstaPosicion());
                    float f_ = PathManager.instance.DistanciaMasCorta(aux[i].EstaPosicion(), destino);
                    hijos[indiceHijos] = new NodoRecorrido(this, aux[i], null, g + g_, g + g_ +f_);
                    indiceHijos++;
                }
            return hijos;
        }

    }

    class ColaNodos
    {

        public ColaNodos siguiente;
        public NodoRecorrido este;
        public ColaNodos(NodoRecorrido a, ColaNodos proximo)
        {
            este = a;
            siguiente = proximo;
        }
        public ColaNodos()
        {
            este = null;
            siguiente = null;
        }
        //Metodo que busca si se encuenta el nodorecorrido abuscar, si no lo encuentra devuelve null si lo encuentra la referencia del nodo.
        public ColaNodos BuscarNodo(NodoRecorrido aBuscar)
        {
            if (este == aBuscar)
                return this;
            else if (siguiente != null)
                return siguiente.BuscarNodo(aBuscar);
            else
                return null;
        }
        //Metodo que quita el nodo de la lista.
        public ColaNodos QuitarNodo(ColaNodos aQuitar)
        {
            if (this == aQuitar)
            {
                if (siguiente != null)
                {
                    siguiente = siguiente.siguiente;
                    este = siguiente.este;
                    return this;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                if (siguiente != null)
                {
                    siguiente = siguiente.QuitarNodo(aQuitar);
                }
                else
                {
                    este = null;
                    siguiente = null;
                }
                return this;
            }
        }

        //Metodo que quita un elemento de la lista con referencial nodorecorrido aquitar.
        public ColaNodos QuitarNodo(NodoRecorrido aQuitar)
        {
            if (este == aQuitar)
            {
                if (siguiente != null)
                {
                    este = siguiente.este;
                    siguiente = siguiente.siguiente;
                    return this;
                }
                else
                {
                    este = null;
                    return null;
                }
            }
            else
            {
                if (siguiente != null)
                {
                    siguiente = siguiente.QuitarNodo(aQuitar);
                    return this;
                }
                else
                {
                    return null;
                }

            }
        }

        // Introduce el nodorecorrido aintroducir conservando el orden de primero los elementos con el menor coste estimado.
        public void IntroducirNodo(NodoRecorrido aIntroducir)
        {
            if (este == null)
                este = aIntroducir;
            else
            {
                if (aIntroducir.f > este.f)
                {
                    if (siguiente != null)
                        siguiente.IntroducirNodo(aIntroducir);
                    else
                        siguiente = new ColaNodos(aIntroducir, null);
                }
                else
                {
                    if (este != null)
                        siguiente = new ColaNodos(este, siguiente);
                    else
                        siguiente = null;
                    este = aIntroducir;
                }
            }
        }
        
        //Introduce los nodos en la lista que se encuentren en el punto y los que esten proyectando un raycast en las cuatro direcciones cardinales.
        public void IntroducirNodosEnCruz(Vector2 posicion, PuntoRecorrido[] puntosDistanciaManhattan)
        {
            //Comprobamos que el punto no este dentro de uno de los puntos de la red.
            int i = 0;
            while (i<puntosTotales.Length&&!puntosTotales[i].GetComponent<Collider2D>().bounds.Contains(posicion))
            {
                i++;
            }
            //Si esta dentro introducimos ese nodo en la cola.
            if(i < puntosTotales.Length)
            {
                float distancia = puntosTotales[i].DistanciaHasta(posicion);
                IntroducirNodo(new NodoRecorrido(null, puntosTotales[i], null, distancia, distancia + PathManager.instance.DistanciaMasCorta(posicion,puntosDistanciaManhattan)));
            }

            //Creamos 4 rayos hacia las cuatro direcciones cardinales(debido a que el mapa tiene pasillos ortogonales).
            RaycastHit2D[] hit = new RaycastHit2D[4];
            hit[0]= Physics2D.Raycast(posicion, Vector2.up, Mathf.Infinity, PathManager.instance.conQueColisiona);
            Debug.DrawRay(posicion, Vector2.up, Color.green, 10f);
            hit[1] = Physics2D.Raycast(posicion, Vector2.right, Mathf.Infinity, PathManager.instance.conQueColisiona);
            Debug.DrawRay(posicion, Vector2.right, Color.green, 10f);
            hit[2] = Physics2D.Raycast(posicion, Vector2.down, Mathf.Infinity, PathManager.instance.conQueColisiona);
            Debug.DrawRay(posicion, Vector2.down, Color.green, 10f);
            hit[3] = Physics2D.Raycast(posicion, Vector2.left, Mathf.Infinity, PathManager.instance.conQueColisiona);
            Debug.DrawRay(posicion, Vector2.left, Color.green,10f);

            PuntoRecorrido aux = null;
            for (int j = 0; j < 4; j++)
            {
                if (hit[j].collider != null && (aux = hit[j].collider.GetComponent<PuntoRecorrido>()) != null)
                {
                    float distancia = aux.DistanciaHasta(posicion);
                    IntroducirNodo(new NodoRecorrido(null, aux, null, distancia, distancia + PathManager.instance.DistanciaMasCorta(posicion, puntosDistanciaManhattan)));
                }
            }
        }
    }

    //Devuelve la diastancia mas corta de entre las distancia desde desde a puntos.
    float DistanciaMasCorta(Vector2 desde, PuntoRecorrido[] puntos)
    {
        float min = Mathf.Infinity;

        for (int i = 0; i < puntos.Length; i++)
            min = Mathf.Min(min, puntos[i].DistanciaHasta(desde));

        return min;
    }

    //Algoritmo A*
    NodoRecorrido crearRecorrido(Vector2 posicion, PuntoRecorrido[] fin)
    {
        //Creamos las colas frontera y descubiertos(esta ultima no requeriria de cola con prioridad).
        ColaNodos frontera = new ColaNodos();
        frontera.IntroducirNodosEnCruz(posicion, fin);
        ColaNodos descubiertos = new ColaNodos(null, null);

        do
        {
            //Quita el nodo de la frontera y lo evalua
            NodoRecorrido aux = frontera.este;
            frontera.QuitarNodo(aux);
            //si es el objetivo ha encontrado el objetivo
            if (aux.este.EstaEstePuntoEn(fin))
                return aux;
            //Lo anyade a los nodos descubiertos
            descubiertos.IntroducirNodo(aux);
            //Expande los hijos
            NodoRecorrido[] auxs = aux.Hijos(fin);
            //Evalua cada hijo
            for (int i = 0; i < auxs.Length; i++)
            {   //Si ya ha sido descubierto lo ignora
                if (descubiertos.BuscarNodo(auxs[i]) == null)
                {
                    ColaNodos auxc = frontera.BuscarNodo(auxs[i]);
                    //si no esta en frontera y su estimacion es menor que el anterior lo sustituye 
                    if (auxc != null && auxc.este.f > auxs[i].f)
                    {
                        frontera.QuitarNodo(auxc);
                        frontera.IntroducirNodo(auxs[i]);
                    }
                    else
                        //En caso contrario lo introduce.
                        frontera.IntroducirNodo(auxs[i]);
                }
            }
            //Si la frontera se queda vacia no hay solucion.
        } while (frontera.este != null);
        return null;
    }

    //Aplica el A* desde los puntosRecorrido mas cercanos a la posicion final e inicial.
    public listaNodos EncontarCamino(Vector2 inicio, PuntoRecorrido[] fin)
    {
        //Busca el recorrido.
        NodoRecorrido aux = crearRecorrido(inicio, fin);
        //Crea la lista en orden.
        listaNodos lista = new listaNodos();
        while (aux != null)
        {
            lista.ponerNodo(aux.este.EstaPosicion());
            aux = aux.padre;
        }
        return lista;
    }
}