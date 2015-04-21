using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	private LevelSettings levelSettings;
	private PlayerDatabase playerDB;
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
		if(waveLevel < 3){
			enemyChoice = Random.Range(0, enemies.Length -1);
		}
		if(enemies[enemyChoice].tag == "GroundEnemy"){
			Instantiate(enemies[enemyChoice], GroundSpawnpoint.position, GroundSpawnpoint.rotation);
		} else if(enemies[enemyChoice].tag == "AirEnemy"){
			Instantiate(enemies[enemyChoice], AirSpawnpoint.position, AirSpawnpoint.rotation);
		}
		enemyCount++;
		lastSpawnTime = Time.time;
		respawnInterval = Random.Range (respawnMin, respawnMax);
	}

	private void UpdateHUD(){
		hud.SetHP(playerDB.myPlayer.health);
		hud.SetCash(playerDB.myPlayer.cash);
		hud.SetWave(waveLevel);
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
		UpdateHUD();
	}
}
