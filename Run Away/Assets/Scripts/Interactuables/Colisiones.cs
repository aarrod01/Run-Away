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
        static LayerMask capasRecorrido = LayerMask.GetMask("Obstaculos", "Recorrido");
        static public LayerMask CapasRecorrido()
        {
            return capasRecorrido;
        }
        static LayerMask capaNula = LayerMask.GetMask("Nula");
        static public LayerMask CapaNula()
        {
            return capaNula;
        }
        static LayerMask capaVisionMonstruo = LayerMask.GetMask("Jugador","Obstaculos");
        static public LayerMask CapaVisionMonstruo()
        {
            return capaVisionMonstruo;
        }
        static LayerMask capaGolpeMonstruo = LayerMask.GetMask("PuntoVulnerable", "Obstaculos", "PuntoInvulnerable");
        static public LayerMask CapaGolpeMonstruo()
        {
            return capaGolpeMonstruo;
        }
    }
}
