using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float speed;
	public float range;
	public float damage;
	private float distance;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.forward * Time.deltaTime * speed);
		distance += Time.deltaTime * speed;
		if(distance >= range){
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter(Collider other){
		if(other.CompareTag("GroundEnemy")){
			other.GetComponent<Enemy>().applyDamage(damage);
		} 

		if(!other.CompareTag("TurretPart") && !other.CompareTag("RoutePoint")){
			Destroy (gameObject);
		}
	}
}
