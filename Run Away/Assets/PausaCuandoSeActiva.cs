﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausaCuandoSeActiva : MonoBehaviour {

    void OnEnable()
    {
        Time.timeScale = 0.0f;
    }
    private void OnDisable()
    {
        Time.timeScale = 1.0f;
    }
}
