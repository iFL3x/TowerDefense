using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	private LevelSettings levelSettings;
	private PlayerDatabase playerDB;

	private int currentWaveNr = 1;
	public Wave currentWave;
	public int numOfEnemies;

	private Transform spawnpoint;

	// Use this for initialization
	void Start () {
		levelSettings = GameObject.Find ("LevelSettings").GetComponent<LevelSettings>();
		playerDB = GameObject.Find ("PlayerManager").GetComponent<PlayerDatabase>();
		spawnpoint = levelSettings.SpawnPoint.transform;
		LoadWave();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LoadWave(){
		Debug.Log ("Wave " + currentWaveNr + " starting");
		currentWave = levelSettings.Waves[currentWaveNr - 1];
		for(int i=0; i < currentWave.Enemies.Count; i++){
			StartCoroutine(SpawnEnemy(currentWave.Enemies[i], currentWave.SpawnDelays[i]));
		}
		numOfEnemies = currentWave.Enemies.Count;
	}

	IEnumerator SpawnEnemy(GameObject go, float delay){
		yield return new WaitForSeconds(delay);
		Instantiate(go, new Vector3(spawnpoint.position.x, spawnpoint.position.y, spawnpoint.position.z), Quaternion.identity);
	}

	public void EnemyReachedEndZone(int dmg){
		ApplyDamage(dmg);
		if(playerDB.myPlayer.health <= 0){
			//Player lost
			Debug.Log ("Game Over");
		}
	}

	private void ApplyDamage(int dmg){
		playerDB.myPlayer.health -= dmg;
	}

	public void CheckWaveOver(){
		if(numOfEnemies == 0){
			//start next wave if there is one, else show gongratz screen
			if(currentWaveNr < levelSettings.Waves.Count){
				currentWaveNr++;
				LoadWave();
			} else {
				//Player won
				Debug.Log ("Congratulation - All waves are destroyed");
			}
		}
	}

}
