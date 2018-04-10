using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Colisiones
{
    public class Colisiones
    {
        LayerMask capasInteraccion = LayerMask.NameToLayer("Obstaculos")+ LayerMask.NameToLayer("Jugador");
    }
}
