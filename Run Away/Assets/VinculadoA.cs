using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VinculadoA : MonoBehaviour {

    public GameObject vinculo;

    void OnDestroy()
    {
        Destroy(vinculo);
    }
}
