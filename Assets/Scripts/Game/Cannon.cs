using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cannon : MonoBehaviour {

	public GameObject projectile;
	public float reloadSpeed;
	public float turnSpeed;
	public float firePauseTime;
	public Transform axis;
	public Transform[] muzzles;
	public List<Transform> targets = new List<Transform>();

	private float nextFireTime;
	private float nextMoveTime;
	
	void Update () {
		//CheckTargetList();
		if(targets.Count > 0){
			if(!targets[0]){
				targets.Remove (targets[0]);
			} else {
				if(Time.time >= nextMoveTime){		
					axis.rotation = Quaternion.Lerp (axis.rotation, GetAimPosition(targets[0].position), Time.deltaTime * turnSpeed);
				}
				if(Time.time >= nextFireTime){
					FireCannon();
				}
			}
		}
	
	}

	void OnTriggerEnter(Collider other){
		if(other.CompareTag("GroundEnemy")){
			Transform t = other.gameObject.transform;
			if(!targets.Contains(t)){
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
		return Quaternion.LookRotation (new Vector3(target.x - axis.position.x, target.y - axis.position.y, target.z - axis.position.z));
	}


	void CheckTargetList(){
		for(int i = 0; i < targets.Count; i++){
			if(!targets[i]) targets.Remove(targets[i]);
		}

	}

	void FireCannon(){
		//audio.play();
		nextFireTime = Time.time + reloadSpeed;
		nextMoveTime = Time.time + firePauseTime;
		foreach(Transform muzzle in muzzles){
			Instantiate(projectile, muzzle.position, muzzle.rotation);
			muzzle.GetComponent<ParticleSystem>().Play();
		}
	}
}
