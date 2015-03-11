using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	private LevelSettings levelSettings;

	private int currentWaveNr = 2;
	public Wave currentWave;

	private Transform spawnpoint;

	// Use this for initialization
	void Start () {
		levelSettings = GameObject.Find ("LevelSettings").GetComponent<LevelSettings>();
		spawnpoint = levelSettings.SpawnPoint.transform;
		LoadWave();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LoadWave(){
		currentWave = levelSettings.Waves[currentWaveNr - 1];
		for(int i=0; i < currentWave.Enemies.Count; i++){
			StartCoroutine(SpawnEnemy(currentWave.Enemies[i], currentWave.SpawnDelays[i]));
		}
	}

	IEnumerator SpawnEnemy(GameObject go, float delay){
		yield return new WaitForSeconds(delay);
		Instantiate(go, new Vector3(spawnpoint.position.x, spawnpoint.position.y, spawnpoint.position.z), Quaternion.identity);
	}
}
