using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OilThrower : Turret {

	public GameObject oilPrefab;
	public float turnSpeed;
	public float intermission = 0f;
	public Transform axis;
	public Transform[] muzzles;

	public List<Transform> targets = new List<Transform>();
	
	private float nextFireTime;
	private float nextMoveTime;

	public float delayBetweenTargets = 0.4f;
	private float delay = 0f;

	void Update () {
		//CheckTargetList();
		if(targets.Count > 0){
			if(!targets[0]){
				targets.Remove (targets[0]);
				delay += delayBetweenTargets;
			} else {
				if(Time.time >= nextMoveTime){		
					axis.rotation = Quaternion.Lerp (axis.rotation, GetAimPosition(targets[0].position), Time.deltaTime * turnSpeed);
				}
				if(Time.time >= nextFireTime  && delay <= 0f){	
					FireCannon();
				}
				
			}
		}
		if(delay > 0) {
			delay -= Time.deltaTime;
		}
		
	}
	
	void OnTriggerEnter(Collider other){
		if(other.CompareTag("GroundEnemy")){
			Transform t = other.gameObject.transform;
			if(!targets.Contains(t)){
				if(targets.Count == 0){
					delay += delayBetweenTargets;
				}
				targets.Add (t);
			}
		}
	}
	
	void OnTriggerExit(Collider other){
		if(other.CompareTag("GroundEnemy")){
			targets.Remove(other.gameObject.transform);
		}
	}
	
	Quaternion GetAimPosition(Vector3 target){
		float error = 0.5f;
		float x = target.x - axis.position.x + Random.Range (-error, error);
		float y = target.y - axis.position.y + Random.Range (-error*5, error*3);
		float z = target.z - axis.position.z + Random.Range (-error, error);

		return Quaternion.LookRotation (new Vector3(x, y, z));
	}
	

	void CheckTargetList(){
		for(int i = 0; i < targets.Count; i++){
			if(!targets[i]) targets.Remove(targets[i]);
		}
		
	}
	
	void FireCannon(){
		//audio.play();
		nextMoveTime = Time.time;
		nextFireTime = Time.time + intermission;
		foreach(Transform muzzle in muzzles){
			Instantiate(oilPrefab, muzzle.position, muzzle.rotation);
			muzzle.GetComponent<ParticleSystem>().Play();
		}
	}
}
