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
            CambiarVolumenGlobal(0f);
            CambiarVolumenMusica(0f);
            CambiarVolumenEfectos(0f);
            
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
        return aux<volumenPredeterminado ? (aux-volumenPredeterminado)/(80f+volumenPredeterminado): 
            (aux - volumenPredeterminado) / (20f - volumenPredeterminado);
    }

    public void CambiarVolumenGlobal(float porcentaje)
    {
        mainMixer.SetFloat("OverallVolume", porcentaje > 0f ? volumenPredeterminado + porcentaje*(20f-volumenPredeterminado) 
            : volumenPredeterminado - porcentaje * (-80f - volumenPredeterminado));
    }

    public float PorcentajeVolumenMusica()
    {
        float aux;
        mainMixer.GetFloat("MusicVolume", out aux);
        return aux < volumenMusicaPredeterminado ? (aux - volumenMusicaPredeterminado) / (80f + volumenMusicaPredeterminado) : 
            (aux - volumenMusicaPredeterminado) / (20f - volumenMusicaPredeterminado);
    }

    public void CambiarVolumenMusica(float porcentaje)
    {
        mainMixer.SetFloat("MusicVolume", porcentaje > 0f ? volumenMusicaPredeterminado + porcentaje * (20f - volumenMusicaPredeterminado)
            : volumenMusicaPredeterminado - porcentaje * (-80f - volumenMusicaPredeterminado));
    }

    public float PorcentajeVolumenEfectos()
    {
        float aux;
        mainMixer.GetFloat("FxVolume", out aux);
        return aux < volumenSonidosPredeterminado ? (aux - volumenSonidosPredeterminado) / (80f + volumenSonidosPredeterminado) :
            (aux - volumenSonidosPredeterminado) / (20f - volumenSonidosPredeterminado);
    }

    public void CambiarVolumenEfectos(float porcentaje)
    {
        mainMixer.SetFloat("FxVolume", porcentaje > 0f ? volumenSonidosPredeterminado + porcentaje * (20f - volumenSonidosPredeterminado)
            : volumenSonidosPredeterminado - porcentaje * (-80f - volumenSonidosPredeterminado));
    }

    public void CambiarTonoMusica(float porcentaje)
    {
        mainMixer.SetFloat("MusicPitch", porcentaje);
    }


}
