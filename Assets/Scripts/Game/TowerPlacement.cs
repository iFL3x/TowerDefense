using UnityEngine;
using System.Collections;

public class TowerPlacement : MonoBehaviour {

	public Transform placementGrid;
	public LayerMask placementLayerMask;
	public Material hoverMat;
	private Material originalMat;
	private GameObject lastHitObj;
	
	public GameObject[] buildings;
	private GameObject building;

	// Use this for initialization
	void Start () {
		SetGridActive(true);
	}
	
	void Update(){
		
		CheckTurretSelection();
		
		if(building != null){
			if(!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(-1)){
				Ray ray = new Ray();
				ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit = new RaycastHit();
				
				if (Physics.Raycast(ray, out hit, Mathf.Infinity, placementLayerMask)){
					if(lastHitObj){
						lastHitObj.GetComponent<Renderer>().material = originalMat;
					} 
					lastHitObj = hit.collider.gameObject;
					originalMat = lastHitObj.GetComponent<Renderer>().material;
					lastHitObj.GetComponent<Renderer>().material = hoverMat;
					
				} else {
					if(lastHitObj){
						lastHitObj.GetComponent<Renderer>().material = originalMat;
						lastHitObj = null;
					}
				}
				
				//Tower Placement on mouseclick
				if(Input.GetMouseButtonDown(0) && lastHitObj){
					if(lastHitObj.tag == "PlacementPlane_Free"){
						if(CheckCash(building.GetComponent<Building>().costs)){
							SpendCash(building.GetComponent<Building>().costs);

							GameObject newBuilding = Instantiate(
								building, 
								new Vector3(
									lastHitObj.transform.position.x, 
									lastHitObj.transform.position.y + 0.5f, 
									lastHitObj.transform.position.z), 
								Quaternion.identity) as GameObject;

							newBuilding.transform.localEulerAngles = new Vector3(
								newBuilding.transform.localEulerAngles.x, 
								Random.Range (0,360), 
								newBuilding.transform.localEulerAngles.z);

							lastHitObj.tag = "PlacementPlane_Taken";
							building = null;
							SetGridActive(false);
						}
					}
				}
			}

			
		}
	}

	private bool CheckCash(int costs){
		if(GetComponent<GameManager>().playerDB.myPlayer.cash >= costs){
			return true;
		}
		return false;
	}

	private void SpendCash(int costs){
		if(GetComponent<GameManager>().playerDB.myPlayer.cash >= costs){
			GetComponent<GameManager>().playerDB.myPlayer.cash -= costs;
		}
		GetComponent<GameManager>().UpdateHUD();
	}

	private void SetGridActive(bool state){
		if(state){
			placementGrid.gameObject.SetActive(true);
		} else {
			placementGrid.gameObject.SetActive(false);
		}
	}

	private void CheckTurretSelection(){
		if(Input.GetKeyDown(KeyCode.Alpha1)){
			SelectTower(0);
		}
		if(Input.GetKeyDown(KeyCode.Alpha2)){
			SelectTower(1);
		}
		if(Input.GetKeyDown(KeyCode.Alpha3)){
			SelectTower(2);
		}
		if(Input.GetKeyDown(KeyCode.Alpha4)){
			
		}
		if(Input.GetKeyDown(KeyCode.Alpha5)){
			
		}
	}

	public void SelectTower(int index){
		building = buildings[index];
		SetGridActive(true);
		GetComponent<GameManager>().SetSelectedTower(index);
	}	
}
