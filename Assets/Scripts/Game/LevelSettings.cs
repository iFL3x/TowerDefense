using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelSettings : MonoBehaviour {

	public Transform Route;
	public List<Vector3> RoutePoints;
	public GameObject SpawnPoint;

	public List<Wave> Waves = new List<Wave>();

	// Use this for initialization
	void Start () {
		foreach(Transform child in Route){
			RoutePoints.Add(new Vector3(child.position.x, child.position.y, child.position.z));
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}	
