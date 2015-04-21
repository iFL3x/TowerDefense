﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {

	public float health = 100;
	public float speed = 5;
	public int damage = 20;
	public int cash = 0;
	public int spawnsAfterWave = 0;

	public GameObject smoke;
	public GameObject fire;
	public GameObject explosion;

	private List<Vector3> routePoints;
	private int numOfRoutePoints;
	private int nextRoutePointIndex = 0;
	public Vector3 nextRoutePoint;

	private float startPositionY;

	private LevelSettings levelSettings;
	private GameManager gameManager;

	private bool isEnemyDestroyed = false;

	// Use this for initialization
	void Start () {
		levelSettings = GameObject.Find ("LevelSettings").GetComponent<LevelSettings>();
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager>();
		this.routePoints = levelSettings.RoutePoints;
		this.numOfRoutePoints = this.routePoints.Count;
		this.nextRoutePoint = this.routePoints[nextRoutePointIndex];
		this.startPositionY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		if(nextRoutePointIndex < numOfRoutePoints){
			//Move
			transform.LookAt(new Vector3(nextRoutePoint.x, this.startPositionY, nextRoutePoint.z));
			transform.Translate(Vector3.forward * Time.deltaTime * speed);
		}
	}

	public void ReachedRoutePoint(){
		if(nextRoutePointIndex < numOfRoutePoints -1){
			nextRoutePointIndex++;
			nextRoutePoint = routePoints[nextRoutePointIndex];
		}
	}

	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == "EndZone"){
			gameManager.EnemyReachedEndZone(damage);
			isEnemyDestroyed = true;
			StartCoroutine(DestroyEnemy(0.0f));
		}
	}

	public void applyDamage(float dmg){
		health -= dmg;
		if(health <= 70){
			smoke.GetComponent<ParticleSystem>().emissionRate = 30f;
			smoke.GetComponent<ParticleSystem>().Play();
		}
		if(health <= 45){
			smoke.GetComponent<ParticleSystem>().emissionRate = 60f;
		}

		if(health <= 20){
			smoke.GetComponent<ParticleSystem>().emissionRate = 90f;
		}


		if(health <= 0 && !isEnemyDestroyed){
			explosion.GetComponent<ParticleSystem>().Play();
			speed = 0;
			isEnemyDestroyed = true;
			StartCoroutine(DestroyEnemy(0.23f));
		}
	}

	IEnumerator DestroyEnemy(float delay){
		yield return new WaitForSeconds(delay);
		gameManager.EnemyDestroyed(this.cash);
		Destroy (gameObject);
		if(gameManager.enemyCount > 0){
			gameManager.enemyCount -= 1;
			//gameManager.CheckWaveOver();
		}
	}
	
}