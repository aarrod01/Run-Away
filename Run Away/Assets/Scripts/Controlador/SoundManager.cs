using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour {
    
    public AudioSource cancionPrincipal;
    
    [Range(-3, 3)]
    public float tonoPredeterminado, tonoDrogado;
    private AudioSource cancion;
    public static SoundManager instance = null;
    [SerializeField]
    [Range(-100f, 100)]
    float volumenPredeterminado;
    [SerializeField]
    [Range(-100f, 100f)]
    float volumenMusicaPredeterminado;
    [SerializeField]
    [Range(-100f,100f)]
    float volumenSonidosPredeterminado;
    public AudioMixer mainMixer;
  

    void Awake()
    {
       if (instance == null)
        {
            instance = this;
            CambiarVolumenGlobal(1f);
            CambiarVolumenMusica(1f);
            CambiarVolumenEfectos(1f);
            
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);

        instance.Iniciar();
    }
    private void Start()
    {
        CambiarTonoMusica(tonoPredeterminado);
    }

    void Iniciar()
    {
    }

    public float PorcentajeVolumenGlobal()
    {
        float aux;
        mainMixer.GetFloat("OverallVolume", out aux);
        return aux - volumenPredeterminado;
    }

    public void CambiarVolumenGlobal(float porcentaje)
    {
        mainMixer.SetFloat("OverallVolume", porcentaje == 0 ? float.MinValue : volumenPredeterminado + porcentaje);
    }

    public float PorcentajeVolumenMusica()
    {
        float aux;
        mainMixer.GetFloat("MusicVolume", out aux);
        return aux - volumenMusicaPredeterminado;
    }

    public void CambiarVolumenMusica(float porcentaje)
    {
        mainMixer.SetFloat("MusicVolume", porcentaje == 0 ? float.MinValue : volumenMusicaPredeterminado + porcentaje );
    }

    public float PorcentajeVolumenEfectos()
    {
        float aux;
        mainMixer.GetFloat("FxVolume", out aux);
        return aux - volumenSonidosPredeterminado;
    }

    public void CambiarVolumenEfectos(float porcentaje)
    {
        mainMixer.SetFloat("FxVolume", porcentaje == 0 ? float.MinValue : volumenSonidosPredeterminado+porcentaje);
    }

    public void CambiarTonoMusica(float porcentaje)
    {
        mainMixer.SetFloat("MusicPitch", porcentaje);
    }


}
