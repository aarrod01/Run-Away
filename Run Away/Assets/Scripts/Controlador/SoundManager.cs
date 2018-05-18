using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Sonidosss
{
    AudioClip sonido;

    public AudioClip Sonido
    {
        get
        {
            return sonido;
        }
        set
        {
            sonido = value;
        }
    }
    bool bucle;
    public bool Bucle
    {
        set
        {
            bucle = value;
        }
        get
        {
            return bucle;
        }
    }
    bool solapamiento;
    public bool Solapamiento
    {
        get
        {
            return solapamiento;
        }
        set
        {
            solapamiento = value;
        }
    }
    float volumenGlobal;
    public float VolumenGlobal
    {
        get
        {
            return volumenGlobal;
        }
        set
        {
            volumenGlobal = Mathf.Max(Mathf.Min(value,1f),0f);
            audioSource.volume = volumen * volumenGlobal;
        }
    }
    float volumen;
    public float Volumen
    {
        get
        {
            return volumen;
        }
        set
        {
            volumen = Mathf.Max(Mathf.Min(value, 1f), 0f);
        }
    }
    float tono;
    public float Tono
    {
        get
        {
            return tono;
        }
        set
        {
            tono = value;
        }
    }
    AudioSource audioSource;
    bool activo;
    public bool EstaActivo
    {
        get
        {
            return activo;
        }
    }
    public GameObject gO { get { return audioSource.gameObject; } }

    public Sonidosss(AudioClip _sonido, AudioMixer mixer, bool _bucle, bool _solapamiento, float _volumenBase, float _tono, float _volumenGlobal)
    {

        sonido = _sonido;
        bucle = _bucle;
        solapamiento = _solapamiento;
        Volumen = _volumenBase;
        tono = _tono;
        audioSource = new GameObject(_sonido.name, typeof(AudioSource)).GetComponent<AudioSource>();
        
        audioSource.clip = sonido;
        audioSource.loop = bucle;
        audioSource.playOnAwake = false;
        activo = false;
        VolumenGlobal = _volumenGlobal;
    }

    public void Activar()
    {
        if (!solapamiento)
            audioSource.Stop();
        audioSource.Play();
        audioSource.volume = volumen * volumenGlobal;
        audioSource.pitch = tono;
        activo = true;
    }

    public void CambiarVolumen(float porcentajeVolumen)
    {
        audioSource.volume = volumen * porcentajeVolumen * volumenGlobal;
    }

    public void Desactivar()
    {
        audioSource.Stop();
        activo = false;
    }

    public void Destruir()
    {
        audioSource.Stop();
        Object.Destroy(audioSource.gameObject);
    }
}

public class SoundManager : MonoBehaviour {

	public class FocosDeSonido
    {
        Foco primero;
        public Foco Primero
        {
            get { return primero; }
        }
        int numeroFocos;
        public int NumeroFocos
        {
            get
            {
                return numeroFocos;
            }
        }
        public class Foco
        {
            public class Sonidoss
            {
                SonidoL primero;
                public SonidoL Primero
                {
                    get
                    {
                        return primero;
                    }
                }
                int numeroSonidos;
                public int NumeroSonidos
                {
                    get
                    {
                        return numeroSonidos;
                    }
                }
                public class SonidoL
                {
                    Sonidosss sonido;
                    public Sonidosss Sonido
                    {
                        get
                        {
                            return sonido;
                        }
                        set
                        {
                            sonido = value;
                        }
                    }
                    SonidoL siguiente;
                    public SonidoL Siguiente
                    {
                        get
                        {
                            return siguiente;
                        }
                        set
                        {
                            siguiente = value;
                        }
                    }
                    
                    public SonidoL()
                    {
                        sonido = null;
                        siguiente = null;
                    }
                    
                }
                
                public Sonidoss()
                {
                    primero = null;
                    numeroSonidos = 0;
                }

                public Sonidoss(params Sonidosss[] audios)
                {
                    numeroSonidos = 0;
                    for(int i = 0; i<audios.Length; i++)
                    {
                        numeroSonidos++;
                        IntroducirSonido(audios[i]);
                    }
                }

                void IntroducirSonidoL(SonidoL son)
                {
                    son.Siguiente = primero;
                    primero = son;
                }

                public SonidoL BuscarSonido(Sonidosss audio)
                {
                    SonidoL aux = primero;
                    while(aux!=null&&aux.Sonido!=audio)
                    {
                        aux = aux.Siguiente;
                    }

                    return aux;
                }

                public void IntroducirSonido(Sonidosss audio)
                {
                    SonidoL aux = BuscarSonido(audio);
                    if(aux==null)
                    {
                        SonidoL nuevo = new SonidoL();
                        nuevo.Sonido = audio;
                        nuevo.Siguiente = primero;
                        primero = nuevo;
                    }
                    else
                    {
                        aux.Sonido.Solapamiento = audio.Solapamiento;
                        aux.Sonido.Volumen = audio.Volumen;
                        aux.Sonido.Tono = audio.Tono;
                    }

                }

                public Sonidosss SonidoNumero(int n)
                {
                    int i = 0;
                    SonidoL aux = primero;
                    while (i < numeroSonidos && i != n)
                        aux = aux.Siguiente;
                    if (aux != null)
                        return aux.Sonido;
                    return null;
                }
            }

            Transform posicionT;
            Foco siguiente;
            Sonidoss sonidos;

            public Foco Siguiente
            {
                get
                {
                    return siguiente;
                }
                set
                {
                    siguiente = value;
                }
            }
            public Transform PosicionT
            {
                get
                {
                    return posicionT;
                }
                set
                {
                    posicionT = value;
                }
            }
            public Sonidoss Sonidos
            {
                get
                {
                    return sonidos;
                }
                set
                {
                    sonidos = value;
                }
            }
            
            public Foco()
            {
                posicionT = null;
                siguiente = null;
                sonidos = new Foco.Sonidoss();
            }
            
        }

        public FocosDeSonido()
        {
            primero = null;
            numeroFocos = 0;
        }

        public Foco BuscarFoco(Transform t)
        {
            Foco aux = primero;
            while(aux!=null&&aux.PosicionT!=t)
            {
                aux = aux.Siguiente;
            }
            return aux;
        }
        
        //En el caso de que ya exista ese foco se cambian los sonidos por los nuevos.
        public void IntroducirFoco(Transform t, Foco.Sonidoss sonidos)
        {
            Foco aux = BuscarFoco(t);
            if(aux==null)
            {
                Foco nuevo = new Foco();
                nuevo.Siguiente = primero;
                nuevo.PosicionT = t;
                nuevo.Sonidos = sonidos;
                primero = nuevo;
                numeroFocos++;
            }
            else
            {
                aux.Sonidos = sonidos;
            }
        }

        public Foco FocoNumero(int n)
        {
            int i = 0;
            Foco aux = primero;
            while(i<numeroFocos&&i!=n)
            {
                aux = aux.Siguiente;
            }
            if(aux!=null)
                return aux;
            return null;
        }

    }

    FocosDeSonido focos;
    public AudioClip cancionPrincipal;
    [Range (0, 1)]
    public float volumenPredeterminado;
    [Range(-3, 3)]
    public float tonoPredeterminado, tonoDrogado;
    private AudioSource cancion;
    private Rigidbody2D jugadorRB;
    public static SoundManager instance = null;
    [SerializeField]
    [Range(0, 1)]
    float volumenSonidos;
    public AudioMixer mainMixer, musicMixer, effectMixer;

    public float VolumenSonidos
    {
        get
        {
            return volumenSonidos;
        }
        set
        {
            volumenSonidos = Mathf.Max(Mathf.Min(value, 1f), 0f);
            CambiarVolumenSonidos(volumenSonidos);
        }
    }

    float volumenMusica;
    public float VolumenMusica
    {
        get
        {
            return volumenMusica;
        }
        set
        {
            volumenMusica = Mathf.Max(Mathf.Min(value, 1f), 0f);
            cancion.volume = volumenMusica;
        }
    }

    void Awake()
    {
       if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            cancion = (new GameObject("CancionPrincipal", typeof(AudioSource))).GetComponent<AudioSource>();
            cancion.clip = cancionPrincipal;
            cancion.Play();
            VolumenMusica = volumenPredeterminado;
            cancion.volume = VolumenMusica;
            cancion.pitch = tonoPredeterminado;
            DontDestroyOnLoad(cancion.gameObject);
        }
        else
            Destroy(this.gameObject);

        instance.Iniciar();
    }

    void Iniciar()
    {
        focos = new FocosDeSonido();
        GameObject jugador = GameObject.FindWithTag("Player");
        if (jugador != null)
            jugadorRB = jugador.GetComponent<Rigidbody2D>();
    }

    public void CambiarTonoCancionPrincipal(float tono)
    {
        cancion.pitch = tono;
    }

    public void IntroducirGeneradorSonidos(Transform generador, params Sonidosss[] sonidos)
    {
        focos.IntroducirFoco(generador, new FocosDeSonido.Foco.Sonidoss(sonidos));
    }
    
    void FixedUpdate()
    {
        FocosDeSonido.Foco aux = focos.Primero;
        while(aux!=null)
        {
            FocosDeSonido.Foco.Sonidoss.SonidoL auxS = aux.Sonidos.Primero;
            while(auxS!=null)
            {
                if (auxS.Sonido.EstaActivo)
                    auxS.Sonido.CambiarVolumen(AtenuacionDistancia(aux.PosicionT.position));
                auxS = auxS.Siguiente;
            }
            aux = aux.Siguiente;
        }
    }

    void CambiarVolumenSonidos(float vol)
    {
        FocosDeSonido.Foco aux = focos.Primero;
        while (aux != null)
        {
            FocosDeSonido.Foco.Sonidoss.SonidoL auxS = aux.Sonidos.Primero;
            while (auxS != null)
            {
                auxS.Sonido.VolumenGlobal = vol;
                auxS.Sonido.CambiarVolumen(AtenuacionDistancia(aux.PosicionT.position));
                auxS = auxS.Siguiente;
            }
            aux = aux.Siguiente;
        }
    }

    float AtenuacionDistancia(Vector2 origen)
    {
        float distancia = (origen - jugadorRB.position).sqrMagnitude;
        return 1f / (distancia );
    }
    
}
