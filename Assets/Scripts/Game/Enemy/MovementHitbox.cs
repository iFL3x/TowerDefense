using UnityEngine;
using System.Collections;

public class MovementHitbox : MonoBehaviour {

	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == "RoutePoint"){
			transform.parent.GetComponent<Enemy>().ReachedRoutePoint();
		} 
	}
}
