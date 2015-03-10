using UnityEngine;
using System.Collections;

[System.Serializable]
public class Map
{
	/// <summary>
	/// This Class is used to create MapSettings Objects that holds different
	/// important information about the maps.
	/// </summary>
	
	//The Map Name that is showed as map title
	public string name;
	
	//This name is used to hold the name of the scene fitting to the map
	public string loadName;
	
	//This texture is used to show a little texture in the map selection menu or game lobby
	public Texture previewTexture;
	
	//Holds the texture drawed on the loading screen
	public Texture loadTexture;
	
	public Map Constructor(){
		Map map = new Map();
		
		map.name = name;
		map.loadName = loadName;
		map.previewTexture = previewTexture;
		map.loadTexture = loadTexture;
		
		return map;
	}
	
}
