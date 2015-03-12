using UnityEngine;
using System.Collections;

[System.Serializable]
public class Player {

	public NetworkPlayer network;
	public string name;
	public int health;
	public int cash;

	public Player Constructor(){
		Player player = new Player();
		player.name = name;
		player.health = health;
		player.cash = cash;
		return player;
	}
}
