﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {

	public float health = 100;
	public float speed = 5;
	public int damage = 20;

	public GameObject smoke;
	public GameObject fire;
	public GameObject explosion;

	private List<Vector3> routePoints;
	private int nextRoutePointIndex = 0;
	public Vector3 nextRoutePoint;

	private float startPositionY;

	private LevelSettings levelSettings;
	private GameManager gameManager;

	// Use this for initialization
	void Start () {
		levelSettings = GameObject.Find ("LevelSettings").GetComponent<LevelSettings>();
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager>();
		this.routePoints = levelSettings.RoutePoints;
		this.nextRoutePoint = this.routePoints[0];
		this.startPositionY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		if(nextRoutePointIndex < routePoints.Count){
			//Move
			transform.LookAt(new Vector3(nextRoutePoint.x, this.startPositionY, nextRoutePoint.z));
			transform.Translate(Vector3.forward * Time.deltaTime * speed);
		}
	}

	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == "RoutePoint"){
			if(nextRoutePointIndex < routePoints.Count -1){
				nextRoutePointIndex++;
				nextRoutePoint = routePoints[nextRoutePointIndex];
			}
		} else if(col.gameObject.tag == "EndZone"){
			gameManager.EnemyReachedEndZone(damage);
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


		if(health <= 0){
			explosion.GetComponent<ParticleSystem>().Play();
			speed = 0;
			StartCoroutine(DestroyEnemy(0.23f));
		}
	}



	IEnumerator DestroyEnemy(float delay){
		yield return new WaitForSeconds(delay);
		Destroy (gameObject);
		if(gameManager.numOfEnemies > 0){
			gameManager.numOfEnemies -= 1;
			gameManager.CheckWaveOver();
		}
	}
	
}
