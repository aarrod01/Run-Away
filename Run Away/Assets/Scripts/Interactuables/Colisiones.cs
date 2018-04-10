using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Colisiones
{
    public class Colision
    {
        static LayerMask capasInteraccion = LayerMask.GetMask("Obstaculos", "Jugador");
        static public LayerMask CapasInteraccion()
        {
            return capasInteraccion;
        }
        static LayerMask capaNula = LayerMask.GetMask("Nula");
        static public LayerMask CapaNula()
        {
            return capaNula;
        }
    }
}
