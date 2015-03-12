using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {

	public float health = 100;
	public float speed = 5;
	public int damage = 20;

	private List<Vector3> routePoints;
	private int nextRoutePointIndex = 0;
	public Vector3 nextRoutePoint;

	private LevelSettings levelSettings;
	private GameManager gameManager;

	// Use this for initialization
	void Start () {
		levelSettings = GameObject.Find ("LevelSettings").GetComponent<LevelSettings>();
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager>();
		this.routePoints = levelSettings.RoutePoints;
		this.nextRoutePoint = this.routePoints[0];
	}
	
	// Update is called once per frame
	void Update () {
		if(nextRoutePointIndex < routePoints.Count){
			//Move
			transform.LookAt(new Vector3(nextRoutePoint.x, nextRoutePoint.y, nextRoutePoint.z));
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
			DestroyEnemy();
		}
	}

	public void applyDamage(float dmg){
		health -= dmg;
		if(health <= 0){
			DestroyEnemy();
		}
	}

	private void DestroyEnemy(){
		Destroy (gameObject);
		if(gameManager.numOfEnemies > 0){
			gameManager.numOfEnemies -= 1;
			gameManager.CheckWaveOver();
		}
	}
	
}
