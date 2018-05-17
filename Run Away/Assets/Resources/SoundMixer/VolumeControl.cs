using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class VolumeControl : MonoBehaviour {

	public AudioMixer mixer;
	public PostEffectsBase peb;

	public void ChangeOverallVol (float vol){
		mixer.SetFloat ("OverallVolume", Mathf.Log10 (vol) * 20f);
	}

	public void ChangeMusiclVol (float vol){
		mixer.SetFloat ("MusicVolume", Mathf.Log10 (vol) * 20f);
	}

	public void ChangeFxlVol (float vol){
		mixer.SetFloat ("FxVolume", Mathf.Log10 (vol) * 20f);
	}

	public void ChangeBright (float bright){

	}
}
