using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour {

	public float cooldown = 0f;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, cooldown);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
