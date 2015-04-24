using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	private LevelSettings levelSettings;
	public PlayerDatabase playerDB;
	private HUD hud;
	
	private Transform GroundSpawnpoint;
	private Transform AirSpawnpoint;

	private bool waveActive = false;
	private bool spawnEnemies = false;

	public int waveLevel = 0;
	public float difficultyMultiplier = 1.0f;
	public float difficultyMultiplierSteps = 0.005f;
	public float waveLength = 30.0f;
	public float intermissionTime = 5.0f;
	private float waveEndTime = 0f;

	public GameObject[] enemies;
	public List<GameObject> validEnemies;
	public float respawnMinBase = 3.0f;
	public float respawnMaxBase = 10.0f;
	private float respawnMin = 3.0f;
	private float respawnMax = 10.0f;
	public float respawnInterval = 2.5f;
	public int enemyCount = 0;
	private float lastSpawnTime = 0f;



	// Use this for initialization
	void Start () {
		levelSettings = GameObject.Find ("LevelSettings").GetComponent<LevelSettings>();
		playerDB = GameObject.Find ("PlayerManager").GetComponent<PlayerDatabase>();
		hud = GameObject.Find ("HUD").GetComponent<HUD>();
		
		GroundSpawnpoint = levelSettings.GroundSpawnpoint.transform;
		AirSpawnpoint = levelSettings.AirSpawnpoint.transform;

		validEnemies = new List<GameObject>();

		SetNextWave();
		StartNextWave();
	}


	// Update is called once per frame
	void Update () {
		if(waveActive){
			if(Time.time >= waveEndTime){
				spawnEnemies = false;
				if(enemyCount == 0){
					StartCoroutine(FinishWave());
				}
			}

			if(spawnEnemies){
				if(Time.time > (lastSpawnTime + respawnInterval)){
					SpawnNewEnemy();
				}
			}
		}
	}

	private void SetNextWave(){
		waveLevel++;
		difficultyMultiplier = ((Mathf.Pow(waveLevel, 2)) * difficultyMultiplierSteps) + 1f;
		respawnMin = respawnMinBase * (1 / difficultyMultiplier);
		respawnMax = respawnMaxBase * (1 / difficultyMultiplier);

		validEnemies.Clear();
		foreach(GameObject enemy in enemies){
			if(enemy.GetComponent<Enemy>().spawnsAfterWave <= waveLevel){
				validEnemies.Add (enemy);
			}
		}
	}

	private void StartNextWave(){
		UpdateHUD();
		SpawnNewEnemy();
		waveEndTime = Time.time + waveLength;
		waveActive = true;
		spawnEnemies = true;
	}

	IEnumerator FinishWave(){
		waveActive = false;
		
		yield return new WaitForSeconds(intermissionTime);
		
		SetNextWave();
		StartNextWave();
	}

	private void SpawnNewEnemy(){
		int enemyChoice = 0;
		enemyChoice = Random.Range(0, validEnemies.Count);

		if(validEnemies[enemyChoice].tag == "GroundEnemy"){
			Instantiate(validEnemies[enemyChoice], GroundSpawnpoint.position, GroundSpawnpoint.rotation);
		} else if(validEnemies[enemyChoice].tag == "AirEnemy"){
			Instantiate(validEnemies[enemyChoice], AirSpawnpoint.position, AirSpawnpoint.rotation);
		}
		enemyCount++;
		lastSpawnTime = Time.time;
		respawnInterval = Random.Range (respawnMin, respawnMax);
	}

	public void UpdateHUD(){
		hud.SetHP(playerDB.myPlayer.health);
		hud.SetCash(playerDB.myPlayer.cash);
		hud.SetWave(waveLevel);
	}

	public void SetSelectedTower(int index){
		//hud.UpdateTowerButtons(index);
		GameObject.Find ("HUD").GetComponent<HUD>().UpdateTowerButtons(index);
	}

	public void EnemyReachedEndZone(int dmg){
		ApplyDamage(dmg);
		if(playerDB.myPlayer.health <= 0){
			//Player lost
			Debug.Log ("Game Over");
		}
	}

	public void EnemyDestroyed(int cash){
		playerDB.myPlayer.cash += cash;
		UpdateHUD();
	}

	private void ApplyDamage(int dmg){
		playerDB.myPlayer.health -= dmg;
		UpdateHUD();
	}

}
