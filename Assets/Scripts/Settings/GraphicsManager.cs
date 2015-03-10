using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GraphicsManager : MonoBehaviour {

	//Graphics Settings
	private const int defQualityLevel = 3;
	private int defResolutionIndex;
	private bool defFullscreen = true;
	private bool fullscreen;

	public GameObject ResolutionSlider;
	public GameObject QualitySlider; 
	public GameObject FullScreenToggle;


	void Awake () {
		if(ResolutionSlider == null) Debug.LogError ("Add ResolutionSlider GraphicsManager Variable");

		if(ResolutionSlider == null) Debug.LogError ("Add ResolutionSlider GraphicsManager Variable");
		if(QualitySlider == null) Debug.LogError ("Add QualitySlider GraphicsManager Variable");

		fullscreen = PlayerPrefs.HasKey("fullscreen") ? bool.Parse(PlayerPrefs.GetString("fullscreen")) : defFullscreen;
		FullScreenToggle.GetComponent<Toggle>().isOn = fullscreen;
		Screen.fullScreen = fullscreen;

		defResolutionIndex = Screen.resolutions.Length - 1;
		ResolutionSlider.GetComponent<Slider>().maxValue = Screen.resolutions.Length - 1;

		int resIndex = PlayerPrefs.HasKey("resolutionIndex") ? PlayerPrefs.GetInt("resolutionIndex") : defResolutionIndex;

		if(resIndex >= Screen.resolutions.Length){
			resIndex = 0;
		}

		ResolutionSlider.GetComponent<Slider>().value = resIndex;
		Screen.SetResolution (Screen.resolutions[resIndex].width, Screen.resolutions[resIndex].height, fullscreen);

		int level = PlayerPrefs.HasKey("qualityLevel") ? PlayerPrefs.GetInt("qualityLevel") : defQualityLevel;
		QualitySlider.GetComponent<Slider>().value = level;
		QualitySettings.SetQualityLevel(level, true);
		


	}
	
	private void SetQualityLevel(){
		if(GameObject.Find (QualitySlider.name) != null){
			int level = (int)QualitySlider.GetComponent<Slider>().value;
			PlayerPrefs.SetInt("qualityLevel", level);
			QualitySettings.SetQualityLevel(level, true);
		}
	}

	private void SetFullscreen(){
		if(GameObject.Find (FullScreenToggle.name) != null){
			bool state = FullScreenToggle.GetComponent<Toggle>().isOn;
			PlayerPrefs.SetString("fullscreen", state.ToString());
			fullscreen = state;
			Screen.fullScreen = fullscreen;
		}
	}

	private void SetResolution(){
		if(GameObject.Find (ResolutionSlider.name) != null){
			int resIndex = (int)ResolutionSlider.GetComponent<Slider>().value;
			PlayerPrefs.SetInt("resolutionIndex", resIndex);
			Screen.SetResolution (Screen.resolutions[resIndex].width, Screen.resolutions[resIndex].height, fullscreen);

		}
	}



	public void SaveAll(){
		SetQualityLevel();
		SetFullscreen();
		SetResolution();
	}
}
