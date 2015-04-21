using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerDatabase : MonoBehaviour {

	public List<Player> playerList = new List<Player>();
	public Player myPlayer;

	private const string defPlayerName = "Player";
	
	// Use this for initialization
	void Start () {
		playerList.Add(myPlayer);
		myPlayer.name = PlayerPrefs.HasKey("PlayerName") ? PlayerPrefs.GetString("PlayerName") : defPlayerName;
		myPlayer.health = 100;
		myPlayer.cash = 200;
	}

	public void PlayerDisconnected(NetworkPlayer network){
		GetComponent<NetworkView>().RPC("All_RemovePlayerFromList", RPCMode.AllBuffered, network);
	}

	public void OnConnected(){
		myPlayer.network = Network.player;
		GetComponent<NetworkView>().RPC ("All_AddPlayerToList", RPCMode.OthersBuffered, myPlayer.network, myPlayer.name);
	}

	public void OnDisconnected(){
		playerList.Clear();
		playerList.Add (myPlayer);
	}

	
	[RPC] 
	void All_AddPlayerToList(NetworkPlayer network, string playerName){
		Player player = new Player();
		player.network = network;
		player.name = playerName;
		player.health = 100;
		player.cash = 0;
		playerList.Add(player);
	}

	
	[RPC]
	void All_RemovePlayerFromList(NetworkPlayer network){
		for(int i = 0; i < playerList.Count; i++){
			if(playerList[i].network == network){
				playerList.Remove(playerList[i]);
			}
		}
	}
}
