using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MenuManager : MonoBehaviour {

	public List<GameObject> menus = new List<GameObject>();
	private GameObject curMenu;
	private Text title;
	
	// Use this for initialization
	void Start () {

		title = GameObject.FindGameObjectWithTag ("MenuTitle").GetComponent<Text>();
		curMenu = menus[0];
		title.text = curMenu.name.ToUpper();
		foreach(GameObject go in menus){
			go.SetActive(false);
		}
		curMenu.SetActive(true);
	}

	private void SwitchMenu(string menu){
		curMenu.SetActive(false);
		foreach(GameObject go in menus){
			if(go.name == menu){
				curMenu = go;
				title.text = go.name.ToUpper();
				curMenu.SetActive(true);
			}
		}
	}

	public void OnDisconnected(){
		//Switch the menu back
		this.NavigateTo("Main");
	}

	public void OnConnected(){
		NavigateTo("Server Lobby");
	}

	public void NavigatoTo(Button btn){
		SwitchMenu(btn.name);
	}

	public void NavigateTo(string menu){
		SwitchMenu(menu);
	}

	public void Quit(){
		Application.Quit();
	}

	public void SetMenuActive(bool active){
		gameObject.SetActive(active);
	}
}
