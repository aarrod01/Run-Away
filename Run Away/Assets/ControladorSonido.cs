using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorSonido : MonoBehaviour {

    public static ControladorSonido instance;
	// Use this for initialization
	void Awake () {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
	}

    public void ReproducirSonidoAsincrono(AudioClip a, float intensidad)
    {
        StartCoroutine(Sonido(a, intensidad));
    }
    IEnumerator Sonido(AudioClip a, float intensidad)
    {
        AudioSource aux = new AudioSource();
        aux.clip = a;
        aux.volume = intensidad;
        aux.Play();
        yield return null;
    }

    public void ReproducirMusica(AudioClip a)
    {
    }

	
	// Update is called once per frame
	void Update () {
		
	}
}
