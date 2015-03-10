using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ShowSliderValue : MonoBehaviour
{

	public void UpdateLabel (float value)
	{
		Text lbl = GetComponent<Text>();
		if (lbl != null)
			lbl.text = Mathf.RoundToInt (value * 100) + "%";
	}

	public void UpdateResolutionLabel(float value){
		Text lbl = GetComponent<Text>();
		if (lbl != null){
			int resIndex = (int)value;
			lbl.text = Screen.resolutions[resIndex].width + "x" + Screen.resolutions[resIndex].height;
		}
	}

	public void UpdateQualityLabel(float value){
		Text lbl = GetComponent<Text>();
		if (lbl != null){
			int level = (int)value;
			lbl.text = QualitySettings.names[level];
		}
	}


	public void UpdateMaxPlayerLabel(float value){
		Text lbl = GetComponent<Text>();
		if (lbl != null){
			int players = (int)value;
			lbl.text = players + " Players";
		}
	}
}