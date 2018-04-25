using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

            if ((primero.este - posOrigen).sqrMagnitude < MARGEN)
            {
                QuitarNodo();
            }
            if (primero != null)
                return primero.este;
            return Vector2.negativeInfinity;
        }
    }
}

