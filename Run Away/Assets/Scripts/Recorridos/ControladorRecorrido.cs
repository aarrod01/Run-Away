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
        Nodo primero;
        private class Nodo
        {
            public Nodo siguiente;
            public Vector2 este;
            public Nodo(Nodo s, Vector2 e)
            {
                siguiente = s;
                este = e;
            }
        }
        public Vector2 Posicion()
        {
            if (primero != null)
                return primero.este;
            return Vector2.negativeInfinity;
        }
        public bool Fin()
        {
            return primero == null;
        }
        public listaNodos()
        {
            primero = null;
        }

        public void ponerNodo(Vector2 pos)
        {
            primero = new Nodo(primero, pos);
        }

        public void QuitarNodo()
        {
            if (primero != null)
                primero = primero.siguiente;
        }
        //metodo que devulve la primera posicion de la lista, si el vector2 esta  a menos de margen unidades la lista pasa a apuntar al elemento siguiente.
        public Vector2 PosicionObjetivo(Vector2 posOrigen)
        {
            
            if((primero.este - posOrigen).sqrMagnitude < MARGEN)
            {
                QuitarNodo();
            }
            if(primero!=null)
                return primero.este;
            return Vector2.negativeInfinity;
        }
    }
}

delegate void funcionVacia();

public class ControladorRecorrido : MonoBehaviour
{
    //Singleton
    public static ControladorRecorrido instance = null;
    //guarda todos los puntos del grafo
    static PuntoRecorrido[] puntosTotales;
    LayerMask conQueColisiona;

    void Start()
    {
        
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
        conQueColisiona = LayerMask.GetMask("Obstaculos", "Recorrido") ;
        //Busca todos los nodos del grafo.
        GameObject[] auxiliar = GameObject.FindGameObjectsWithTag("Path");
        puntosTotales = new PuntoRecorrido[auxiliar.Length];
        for (int i = 0; i < puntosTotales.Length; i++)
        {
            puntosTotales[i] = auxiliar[i].GetComponent<PuntoRecorrido>();
        }
        CrearRedInicial();

        Nodo a = new Nodo(null, puntosTotales[0], null, 0, 0);
        ColaNodos cola = new ColaNodos(a);
        for (int i = 1; i < puntosTotales.Length; i++)
            cola.IntroducirNodo(new Nodo(null, puntosTotales[i], null, 0, 0));
    }

    public PuntoRecorrido[] PuntosTotales()
    {
        return puntosTotales;
    }

    //Reinicia las conexiones entre puntos
    public void CrearRedInicial()
    {
        for (int i = 0; i < puntosTotales.Length; i++)
        {
            puntosTotales[i].CrearPrimerosContactos();
        }
    }

    //Reinicia las conexiones entre puntos
    public void ReiniciarRed()
    {
        for (int i = 0; i < puntosTotales.Length; i++)
        {
            puntosTotales[i].ReiniciarContactos();
        }
    }

    class Nodo
    {

        public Nodo padre;
        public PuntoRecorrido este;
        public Nodo hijo;
        //Coste actual.
        public float g = 0;
        //Coste estimado
        public float f = 0;

        public Nodo(Nodo _padre, PuntoRecorrido _este, Nodo _hijo, float _g, float _f)
        {
            padre = _padre;
            este = _este;
            hijo = _hijo;
            g = _g;
            f = _f;
        }

        //Metodo que devuleve los hijos del nodo que no sean el padre, calcula su coste actual y el estimado
        public Nodo[] Hijos(PuntoRecorrido[]destino)
        {
            Nodo[] hijos = null;
            int numeroHijos;
            PuntoRecorrido[] aux = este.PuntosConectados();
            if (padre == null)
                numeroHijos = aux.Length;
            else
                numeroHijos = aux.Length - 1;
            hijos = new Nodo[numeroHijos];
            int indiceHijos = 0;

            for (int i = 0; i < aux.Length; i++)
                if (padre == null || aux[i] != padre.este)
                {
                    float g_ = aux[i].DistanciaHasta(este.EstaPosicion());
                    float f_ = ControladorRecorrido.instance.DistanciaMasCorta(aux[i].EstaPosicion(), destino);
                    hijos[indiceHijos] = new Nodo(this, aux[i], null, g + g_, g + g_ +f_);
                    indiceHijos++;
                }
            return hijos;
        }

    }

    class ColaNodos
    {
        Elemento primero;
        private class Elemento
        {
            public Elemento siguiente;
            public Nodo este;
            public Elemento(Elemento s, Nodo e)
            {
                siguiente = s;
                este = e;
            }
        }
        public Nodo Primero()
        {
            if(primero!=null)
                return primero.este;
            return null;
        }
        public ColaNodos()
        {
            primero = null;
        }
        public ColaNodos(Nodo e)
        {
            primero = new Elemento(null, e);
        }
        //Metodo que busca si se encuenta el nodorecorrido abuscar, si no lo encuentra devuelve null si lo encuentra la referencia del nodo.
        public bool EstaElNodo(Nodo aBuscar)
        {
            Elemento aux = primero;
            while (aux != null && aux.este != aBuscar)
                aux = aux.siguiente;
            return aux != null;
        }
        //Metodo que quita el nodo de la lista.
        public void QuitarNodo()
        {
            if(primero!=null)
                primero = primero.siguiente;
        }

        //Metodo que quita un elemento de la lista con referencial nodorecorrido aquitar.
        public void QuitarNodo(Nodo aQuitar)
        {
            Elemento aux = primero, aux2=null;
            while (aux != null && aux.este != aQuitar)
            {
                aux2 = aux;
                aux = aux.siguiente;
            }
            if (aux != null&&aux2!=null)
            {
                aux2.siguiente = aux.siguiente;
            }    
        }

        // Introduce el nodorecorrido aintroducir conservando el orden de primero los elementos con el menor coste estimado.
        public void IntroducirNodo(Nodo aIntroducir)
        {
            Elemento aux = primero, aux2=null;
            while (aux != null && aIntroducir.f > aux.este.f) {
                aux2 = aux;
                aux = aux.siguiente;
            }
            if(aux!=null)
            {
                if (aux2 != null)
                    aux2.siguiente = new Elemento(aux, aIntroducir);
                else
                    primero = new Elemento(primero, aIntroducir);
            }
            if (primero == null)
                primero = new Elemento (null,aIntroducir);
           
        }
        
        //Introduce los nodos en la lista que se encuentren en el punto y los que esten proyectando un raycast en las cuatro direcciones cardinales.
        public void IntroducirNodosEnCruz(Vector2 posicion, PuntoRecorrido[] puntosDistanciaManhattan)
        {
            funcionVacia LlamadaDesactivar = () => { };
            //Comprobamos que el punto no este dentro de uno de los puntos de la red.
            int i = 0, puntosDentro=0;
            while (i<puntosTotales.Length)
            {
                if(puntosTotales[i].GetComponent<Collider2D>().bounds.Contains(posicion))
                {
                    float distancia = puntosTotales[i].DistanciaHasta(posicion);
                    IntroducirNodo(new Nodo(null, puntosTotales[i], null, distancia, distancia + ControladorRecorrido.instance.DistanciaMasCorta(posicion, puntosDistanciaManhattan)));
                    puntosTotales[i].gameObject.SetActive(false);
                    LlamadaDesactivar = () => { LlamadaDesactivar(); puntosTotales[i].gameObject.SetActive(true); };
                    puntosDentro++;
                }
                i++;
            }

            //Creamos 4 rayos hacia las cuatro direcciones cardinales(debido a que el mapa tiene pasillos ortogonales).
            RaycastHit2D[] hit = new RaycastHit2D[4];
            hit[0]= Physics2D.Raycast(posicion, Vector2.up, Mathf.Infinity, ControladorRecorrido.instance.conQueColisiona);
            Debug.DrawRay(posicion, Vector2.up, Color.green, 10f);
            hit[1] = Physics2D.Raycast(posicion, Vector2.right, Mathf.Infinity, ControladorRecorrido.instance.conQueColisiona);
            Debug.DrawRay(posicion, Vector2.right, Color.green, 10f);
            hit[2] = Physics2D.Raycast(posicion, Vector2.down, Mathf.Infinity, ControladorRecorrido.instance.conQueColisiona);
            Debug.DrawRay(posicion, Vector2.down, Color.green, 10f);
            hit[3] = Physics2D.Raycast(posicion, Vector2.left, Mathf.Infinity, ControladorRecorrido.instance.conQueColisiona);
            Debug.DrawRay(posicion, Vector2.left, Color.green,10f);

            PuntoRecorrido aux = null;
            for (int j = 0; j < 4; j++)
            {
                if (hit[j].collider != null && (aux = hit[j].collider.GetComponent<PuntoRecorrido>()) != null)
                {
                    float distancia = aux.DistanciaHasta(posicion);
                    IntroducirNodo(new Nodo(null, aux, null, distancia, distancia + ControladorRecorrido.instance.DistanciaMasCorta(posicion, puntosDistanciaManhattan)));
                }
            }

            LlamadaDesactivar();
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
    Nodo crearRecorrido(Vector2 posicion, PuntoRecorrido[] fin)
    {
        //Creamos las colas frontera y descubiertos(esta ultima no requeriria de cola con prioridad).
        ColaNodos frontera = new ColaNodos();
        frontera.IntroducirNodosEnCruz(posicion, fin);
        ColaNodos descubiertos = new ColaNodos();

        do
        {
            //Quita el nodo de la frontera y lo evalua
            Nodo aux = frontera.Primero();
            frontera.QuitarNodo();
            //si es el objetivo ha encontrado el objetivo
            if (aux.este.EstaEstePuntoEn(fin))
                return aux;
            //Lo anyade a los nodos descubiertos
            descubiertos.IntroducirNodo(aux);
            //Expande los hijos
            Nodo[] auxs = aux.Hijos(fin);
            //Evalua cada hijo
            for (int i = 0; i < auxs.Length; i++)
            {   //Si ya ha sido descubierto lo ignora
                if (!descubiertos.EstaElNodo(auxs[i]))
                {
                    Nodo auxc = auxs[i];
                    //si no esta en frontera y su estimacion es menor que el anterior lo sustituye 
                    if (auxc.f > auxs[i].f)
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
        } while (frontera.Primero() != null);
        return null;
    }

    //Aplica el A* desde los puntosRecorrido mas cercanos a la posicion final e inicial.
    public listaNodos EncontarCamino(Vector2 inicio, PuntoRecorrido[] fin)
    {
        //Busca el recorrido.
        Nodo aux = crearRecorrido(inicio, fin);
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