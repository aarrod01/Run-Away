using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Funciones
{
    public class Funciones{
        
        //devuelve la amortiguacion de una amortiguacion en el momento tiempoTranscurrido desde el inicio.
        public float AmortiguacionPerfecta(float masa, float velocidadInicial, float constanteAmortiguacion, float tiempoTranscurrido)
        {
            return -constanteAmortiguacion * velocidadInicial * Mathf.Exp(-constanteAmortiguacion * tiempoTranscurrido / masa) / masa;
        }
    }
}


