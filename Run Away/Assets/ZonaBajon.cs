using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaBajon : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.tag == "Player")
            GameManager.instance.Bajon();
    }
}
