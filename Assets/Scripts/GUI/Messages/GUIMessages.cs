using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIMessages : MonoBehaviour {

	public Text upgradeText;
	public Button upgradeButton;
	public Text upgradeButtonText;
	public GameObject upgradePanel;
	public Color canAffordUpgrade;
	public Color canNotAffordUpgrade;

	private TurretPlacement turretPlacement;

	// Use this for initialization
	void Start () {
		SetMessagesActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetMessagesActive(bool active){
		foreach(Transform child in transform){
			child.gameObject.SetActive(active);
		}
	}

	public void ShowUpgradeGUI(bool state){
		upgradePanel.SetActive(state);
	}

	public void SetUpgradeText(string text){
		upgradeText.text = text;
	}

	public void CancelUpgrade(){
		GameObject.Find ("GameManager").GetComponent<TurretPlacement>().CancelUpgrade();
	}

	public void ConfirmUpgrade(){
		GameObject.Find ("GameManager").GetComponent<TurretPlacement>().ConfirmUpgrade();
	}

	public void SetUpgradeButtonActive(bool state){
		if(!state){
			upgradeButtonText.color = Color.red;
			ColorBlock colorB = upgradeButton.colors;
			colorB.normalColor = canNotAffordUpgrade;
			upgradeButton.colors = colorB;
			upgradeButton.enabled = false;
		} else {
			upgradeButtonText.color = Color.black;
			ColorBlock colorB = upgradeButton.colors;
			colorB.normalColor = canAffordUpgrade;
			upgradeButton.colors = colorB;
			upgradeButton.enabled = true;




		}
	}
}
