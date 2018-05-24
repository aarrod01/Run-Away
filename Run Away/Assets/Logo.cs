using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Logo : MonoBehaviour {
    public float tiempoDeTransicion;
    public string nombreEscena;
    float tiempo;

    void Start()
    {
        int width = 640; // or something else
        int height = 480; // or something else
        bool isFullScreen = false; // should be windowed to run in arbitrary resolution
        int desiredFPS = 60; // or something else

        Screen.SetResolution(width, height, isFullScreen, desiredFPS);
        Screen.fullScreen = true;
        tiempo = Time.time;
        StartCoroutine(Transicion());
    }
    IEnumerator Transicion()
    {
        while (Time.time - tiempo < tiempoDeTransicion && !Input.anyKeyDown)
        {
            yield return 0;
        }
        SceneManager.LoadScene(nombreEscena);
        yield return null;
    }
}
