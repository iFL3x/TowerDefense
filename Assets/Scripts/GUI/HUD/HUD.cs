using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

	public GameObject HPText;
	public GameObject CashText;

	void Start(){
		SetHUDActive(false);
	}

	public void SetHUDActive(bool active){
		foreach(Transform child in transform){
			if(child.name == "Panel"){
				child.gameObject.SetActive(active);
			}
		}
	}

	public void SetHP(int hp){
		HPText.GetComponent<Text>().text = hp.ToString();
	}

	public void SetCash(int cash){
		CashText.GetComponent<Text>().text = cash.ToString();
	}
}
