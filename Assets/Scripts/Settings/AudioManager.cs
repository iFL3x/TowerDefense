using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AudioManager : MonoBehaviour {
	//Audio Settings
	private const float defaultVolume = 0.5f;
	
	public GameObject SoundVolumeSlider;
	public GameObject MusicVolumeSlider;

	public AudioClip intro;

	void Awake(){
		GetComponent<AudioSource>().clip = intro;
		GetComponent<AudioSource>().Play();
	}

	void Start(){
		//Set Values
		SoundVolumeSlider.GetComponent<Slider>().value = PlayerPrefs.HasKey("SoundVolume") ? PlayerPrefs.GetFloat("SoundVolume") : defaultVolume;
		MusicVolumeSlider.GetComponent<Slider>().value = PlayerPrefs.HasKey("MusicVolume") ? PlayerPrefs.GetFloat("MusicVolume") : defaultVolume;
		GetComponent<AudioSource>().volume = MusicVolumeSlider.GetComponent<Slider>().value;
	}
	
	public void SetSoundVolume(){
		if(GameObject.Find (SoundVolumeSlider.name) != null){
			PlayerPrefs.SetFloat("SoundVolume", SoundVolumeSlider.GetComponent<Slider>().value);
		}
	}
	
	public void SetMusicVolume(){

		if(GameObject.Find (MusicVolumeSlider.name) != null){
			PlayerPrefs.SetFloat("MusicVolume", MusicVolumeSlider.GetComponent<Slider>().value);
			GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MusicVolume");
		}

	}
	
	public void SaveAll(){
		SetSoundVolume();
		SetMusicVolume();
	}

}
