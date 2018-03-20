using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Recorrido;


//Espacio de nombres para los nodos de los recorridos
namespace Recorrido
{
    public class listaNodos
    {
        const float MARGEN = 0.001f;
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
            siguiente = siguiente.siguiente;
            return este = siguiente.este;
        }
        //metodo que devulve la primera posicion de la lista, si el vector2 esta  a menos de margen unidades la lista pasa a apuntar al elemento siguiente.
        public Vector2 PosicionObjetivo(Vector2 posOrigen)
        {
            if((este-posOrigen).sqrMagnitude<MARGEN)
            {
                if (siguiente == null)
                    return Vector2.positiveInfinity;
                else { 
                    siguiente = siguiente.siguiente;
                    return este = siguiente.este;
                }
            }
            return este;
        }
    }
}
public class PathManager : MonoBehaviour {
    //Singleton
    public static PathManager instance = null;
	public PuntoRecorrido puntoBase;
    //guarda todos los puntos del grafo
    static PuntoRecorrido[] puntosTotales;
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
			cola.IntroducirNodoAlPrincipio(new NodoRecorrido(null,puntosTotales[i], null, 0,0));
    }
    //Reinicia las conexiones entre puntos
    void ReiniciarRed()
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
        public NodoRecorrido[] Hijos(Vector2 destino)
        {
            NodoRecorrido[] hijos= null;
            int numeroHijos;
            PuntoRecorrido[] aux = este.PuntosConectados();
            if (padre == null)
                numeroHijos = aux.Length;
            else
                numeroHijos = aux.Length - 1;
            hijos = new NodoRecorrido[numeroHijos];
            int indiceHijos = 0;

            for(int i =0; i<aux.Length;i++)
                if(padre==null||aux[i]!=padre.este)
                {
                    hijos[indiceHijos] = new NodoRecorrido(this, aux[i], null, g + aux[i].DistanciaHasta(este.EstaPosicion()), g + aux[i].DistanciaHasta(este.EstaPosicion()) + aux[i].DistanciaHasta(destino));
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
        public void IntroducirNodoOrdenado(NodoRecorrido aIntroducir)
        {
            if (este == null)
                este = aIntroducir;
            else
            {
                if (aIntroducir.f > este.f)
                {
                    if (siguiente != null)
                        siguiente.IntroducirNodoOrdenado(aIntroducir);
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

		public void IntroducirNodoAlPrincipio(NodoRecorrido aIntroducir)
		{
			if (este == null)
				este = aIntroducir;
			else
			{
				siguiente=new ColaNodos(este,siguiente);
				este = aIntroducir;
			}
		}
		public void IntroducirNodoAlFinal(NodoRecorrido aIntroducir)
		{
			if (este == null)
				este = aIntroducir;
			else if(siguiente==null)
				siguiente=new ColaNodos(aIntroducir,null);
			else
			{
				siguiente.IntroducirNodoAlFinal (aIntroducir);
			}
		}
    }

    //Algoritmo A*
    NodoRecorrido crearRecorrido(PuntoRecorrido inicio, PuntoRecorrido[] fin)
    {
		//Creamos las colas frontera y descubiertos(esta ultima no requeriria de cola con prioridad).
		ColaNodos frontera = new ColaNodos(new NodoRecorrido(null, fin[0], null, 0, 0), null);
		ColaNodos descubiertos = new ColaNodos(null, null);
		for (int i = 1; i < fin.Length; i++)
			frontera.IntroducirNodoOrdenado (new NodoRecorrido(null, fin[i], null, 0, 0));

        do
        {
            //Quita el nodo de la frontera y lo evalua
            NodoRecorrido aux = frontera.este;
            frontera.QuitarNodo(aux);
            //si es el objetivo ha encontrado el objetivo
            if (aux.este == inicio)
                return aux;
            //Lo anyade a los nodos descubiertos
            descubiertos.IntroducirNodoOrdenado(aux);
            //Expande los hijos
            NodoRecorrido[] auxs = aux.Hijos(inicio.EstaPosicion());
            //Evalua cada hijo
            for (int i = 0; i < auxs.Length; i++)
            {   //Si ya ha sido descubierto lo ignora
                if (descubiertos.BuscarNodo(auxs[i]) == null)
                {
                    ColaNodos auxc = frontera.BuscarNodo(auxs[i]);
                    //si no esta enfrontera y su estimacion es menor que el anterior lo sustituye 
                    if (auxc != null&& auxc.este.f > auxs[i].f)
                    {
                        frontera.QuitarNodo(auxc);
                        frontera.IntroducirNodoOrdenado(auxs[i]);
                    }
                    else
                        //En caso contrario lo introduce.
                        frontera.IntroducirNodoOrdenado(auxs[i]);
                }
            }
            //Si la frontera se queda vacia no hay solucion.
        } while (frontera.este != null);
        return null;
    }

    //Aplica el A* desde los puntosRecorrido mas cercanos a la posicion final e inicial.
	public listaNodos EncontarCamino(Transform inicio, PuntoRecorrido[] fin)
    {
		PuntoRecorrido ini=Instantiate (puntoBase,inicio);
		puntoBase.ReiniciarContactos ();

        //Busca el recorrido.
		     NodoRecorrido aux = crearRecorrido(ini, fin);
        //Crea la lista en orden.
        listaNodos lista = new listaNodos();
        while (aux != null)
        {
            lista.ponerNodo(aux.este.EstaPosicion());
            aux = aux.padre;
        }
		Destroy (ini);
        return lista;
    }
}
