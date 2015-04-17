using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerPlacement : MonoBehaviour {

	public List<Transform> towers = new List<Transform>();
	private Transform tower;
	public Transform placementHelpBox;
	public LayerMask layerMask;

	void Start(){
	}

	void Update(){

		CheckTurretSelection();

		if(tower != null){

			//Tower Placement on mouseclick
			if(Input.GetMouseButtonUp(0)){
				SetTurret();
			}
		
		}
	}

	private void SetPlacementHelpBoxActive(bool state){
		if(state){
			placementHelpBox.gameObject.SetActive(true);
		} else {
			placementHelpBox.gameObject.SetActive(false);
		}
	}

	private void SetTurret(){
		Ray ray = new Ray();
		ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit = new RaycastHit();
		
		if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)){
			if(hit.collider.tag == "Ground"){
				Vector3 pos = hit.point;
				pos.y += 1f;
				pos.x = Mathf.Round(pos.x);
				pos.z = Mathf.Round(pos.z);
				Instantiate (tower, pos, Quaternion.identity);
				tower = null;
				SetPlacementHelpBoxActive(false);
			}
		}
	}

	void CheckTurretSelection(){
		if(Input.GetKeyDown(KeyCode.Alpha1)){
			tower = towers[0];
			SetPlacementHelpBoxActive(true);
		}
		if(Input.GetKeyDown(KeyCode.Alpha2)){
			tower = towers[1];
			SetPlacementHelpBoxActive(true);
		}
		if(Input.GetKeyDown(KeyCode.Alpha3)){
			tower = towers[2];
			SetPlacementHelpBoxActive(true);
		}
		if(Input.GetKeyDown(KeyCode.Alpha4)){
			
		}
		if(Input.GetKeyDown(KeyCode.Alpha5)){
			
		}
	}
}
