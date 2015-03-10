using UnityEngine;
using System.Collections;

public class ConnectionManager : MonoBehaviour {

	//Used to store a relation to the MenuManager script
	private MenuManager menuManager;

	private LevelManager levelManager;

	private PlayerDatabase playerDB;

	// Use this for initialization
	void Start () {
		menuManager = GameObject.Find("Menu").GetComponent<MenuManager>();
		levelManager = GameObject.Find ("LevelManager").GetComponent<LevelManager>();
		playerDB = GameObject.Find ("PlayerManager").GetComponent<PlayerDatabase>();
	}
	
	//1 Fired on server only
	void OnPlayerDisconnected(NetworkPlayer network){
		playerDB.PlayerDisconnected(network);	//tell everyone
		Network.RemoveRPCs(network);
		Network.DestroyPlayerObjects(network);
	}

	//2 Fired on client
	void OnConnectedToServer(){
		menuManager.OnConnected(); 	//switch menu
		playerDB.OnConnected();		//tell everone
	}

	//2 Fired on client
	void OnDisconnectedFromServer ()
	{
		levelManager.OnDisconnected();	//switch level
		menuManager.OnDisconnected();	//switch menu
		playerDB.OnDisconnected(); 		//reset playerDB
	}


	
	public void ConnectToServer(string ip, int port){
		Network.Connect(ip, port);
	}


}
