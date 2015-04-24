using UnityEngine;
using System.Collections;

public class Oil : MonoBehaviour {

	public GameObject fire;
	public bool burning = false;
	private bool animationPlaying = false;
	public GameObject FireCollider;

	// Use this for initialization
	void Start () {
		if(burning){
			SetFire(true);
		}
	}
	
	// Update is called once per frame
	void Update () {

		if(!animationPlaying && burning){
			SetFire(true);
		}

		if (Input.GetKey(KeyCode.F)){
			burning = !burning;
			SetFire(burning);
		}
	}



	void SetFire(bool state){
		burning = state;
		if(state){
			GameObject obj = Instantiate(FireCollider, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
			obj.transform.parent = transform;
			PlayAnimation();
		} else {
			StopAnimation();
		}
	}

	void OnTriggerEnter(Collider col){
		if(col.CompareTag("Fire")){
			burning = true;
		}
	}


	void PlayAnimation(){
		fire.GetComponent<ParticleSystem>().Play();
		animationPlaying = true;
	}

	void StopAnimation(){
		fire.GetComponent<ParticleSystem>().Stop();
		animationPlaying = false;
	}
}
