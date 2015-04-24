using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD : MonoBehaviour {
	
	public GameObject HPText;
	public GameObject CashText;
	public GameObject Wave;
	public Button[] TowerButtons;
	public Color onColor;
	public Color offColor;

	void Start(){
		SetHUDActive(false);
		UpdateTowerButtons(0);
	}

	public void SetHUDActive(bool active){
		foreach(Transform child in transform){
			child.gameObject.SetActive(active);
		}
	}

	public void SetWave(int wave){
		Wave.GetComponent<Text>().text = wave.ToString();
	}

	public void SetHP(int hp){
		HPText.GetComponent<Text>().text = hp.ToString();
	}

	public void SetCash(int cash){
		CashText.GetComponent<Text>().text = cash.ToString();
	}

	public void SelectTower(int index){
		GameObject.Find ("GameManager").GetComponent<TurretPlacement>().SelectTower(index);
	}

	public void UpdateTowerButtons(int index){
		foreach(Button tb in TowerButtons){
			ColorBlock colors = tb.colors;
			colors.normalColor = offColor;
			colors.highlightedColor = offColor;
			colors.pressedColor = offColor;
			tb.colors = colors;
		}

		ColorBlock colorB = TowerButtons[index].colors;
		colorB.normalColor = onColor;
		colorB.highlightedColor = onColor;
		colorB.pressedColor = onColor;
		TowerButtons[index].colors = colorB;

	}


}
