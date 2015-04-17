using UnityEngine;
using System.Collections;

public class PlacementHelpBox : MonoBehaviour {

	public Material red;
	public Material green;
	public LayerMask layerMask;
	private Material mat;

	void Start(){
		mat = green;
		SetMaterial(green);
		gameObject.SetActive(false);
	}

	// Update is called once per frame
	void Update () {
		SetPlacementHelpBox();
	}

	private void SetPlacementHelpBox(){
		Ray ray = new Ray();
		ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit = new RaycastHit();
		
		if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)){
			if(hit.collider.tag == "Ground"){
				Vector3 pos = hit.point;
				transform.position = pos;
				SetMaterial(green);
			} else {
				SetMaterial(red);
			}
			
		}
	}

	private void SetMaterial(Material mat){
		if(GetComponent<MeshRenderer>().material != mat){
			GetComponent<MeshRenderer>().material = mat;
		}
	}
}
