using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class VolumeControl : MonoBehaviour {

	public void ChangeMusiclVol (float vol){
        SoundManager.instance.VolumenMusica = vol;
	}

	public void ChangeFxlVol (float vol){
        SoundManager.instance.VolumenSonidos = vol;
    }

	public void ChangeBright (float bright){

	}
}
