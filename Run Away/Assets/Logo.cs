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
