using UnityEngine;
using System.Collections;

public class Projectile_Oil : MonoBehaviour {

	public float speed;
	public float range;
	public float damage;
	private float distance;

	public GameObject groundOilPrefab;

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
		if(other.CompareTag("GroundEnemy") || other.CompareTag("AirEnemy")){
			other.GetComponent<Enemy>().applyDamage(damage);
		} 


		if(other.CompareTag("GroundBottom")){
			Instantiate(groundOilPrefab, 
			            new Vector3(
							gameObject.transform.position.x,
							gameObject.transform.position.y + 0.05f,
							gameObject.transform.position.z),
						Quaternion.identity);
			Destroy (gameObject);
		}
	}
}
