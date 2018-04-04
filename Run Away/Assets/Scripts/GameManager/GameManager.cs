using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    int monstruosVivos = 0;
    int drogaConsumida = 0;

    void Start()
    {
        
    }

    public void ConsumirDroga()
    {
        drogaConsumida++;
    }

}
