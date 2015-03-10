using UnityEngine;
using System.Collections;

public class ControlsManager : MonoBehaviour {

	//Controls
	public float mouseSensivity;
	private const float defMouseSensivity = 50;

	// Use this for initialization
	void Start () {
		if(PlayerPrefs.HasKey("mouseSensivity")){
			SetMouseSensivity(PlayerPrefs.GetFloat("mouseSensivity"));
		} else {
			SetMouseSensivity(defMouseSensivity);
		}
	}
	
	private void SetMouseSensivity(float sensivity){
		mouseSensivity = sensivity;
		PlayerPrefs.SetFloat("mouseSensivity", sensivity);
	}

	public void SaveAll(){
		SetMouseSensivity(mouseSensivity);
	}
}
