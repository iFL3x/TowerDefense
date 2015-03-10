using UnityEngine;
using System.Collections;

[System.Serializable]
public class Player {

	public NetworkPlayer network;
	public string name;
	public string nation;
	public int team;
	public int wood;
	public int food;
	public int stone;
	public int gold;
	public int iron;
	public int oil;


	public Player Constructor(){
		Player player = new Player();

		player.name = name;
		player.nation = nation;
		player.team = team;
		player.wood = wood;
		player.food = food;
		player.stone = stone;
		player.gold = gold;
		player.iron = iron;
		player.oil = oil;
			
		return player;
	}
}
