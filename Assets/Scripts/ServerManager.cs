using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ServerManager : MonoBehaviour {

	public string serverName;
	public int port;
	public int maxPlayers = 4;
	public bool isPublic;
	private string gameKey = "RTS-YoloSwagHose";

	MenuManager menuManager;

	// Use this for initialization
	void Start () {
		menuManager = GameObject.Find ("Menu").GetComponent<MenuManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void SetServerName(Text serverName){
		this.serverName = serverName.text;
	}

	public void SetPort(Text port){
		this.port = int.Parse(port.text);
	}

	public void SetMaxPlayers(Slider maxPlayers){
		this.maxPlayers = (int)maxPlayers.GetComponent<Slider>().value;
	}

	public void SetIsPublic(Toggle isPublic){
		this.isPublic = isPublic.isOn;
	}


	public void StartServer(){
		if(isPublic){
			Debug.LogWarning(isPublic.ToString());
			Network.InitializeServer(maxPlayers, port, !Network.HavePublicAddress());
			MasterServer.RegisterHost(gameKey, serverName);
		} else {
			Network.InitializeServer(maxPlayers, port, false);
		}

		menuManager.NavigateTo("Lobby");
	}

	public void StopServer(){
		if(isPublic){
			MasterServer.UnregisterHost();
		}
		Network.Disconnect();
	}

}
