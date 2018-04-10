using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Colisiones
{
    public class Colisiones
    {
        LayerMask capasInteraccion = LayerMask.GetMask("Obstaculos","Jugador");
        LayerMask capaNula = LayerMask.GetMask("Nula");
        LayerMask capaGolpeMonstruo = LayerMask.GetMask("Obstaculos", "PuntoVulnerable", "PuntoInvulnerable");
        LayerMask capaCampoVisionMonstruo = LayerMask.GetMask("Jugador", "Obstaculos");

    }
}
