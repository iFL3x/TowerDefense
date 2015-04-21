using UnityEngine;
using System.Collections;

public class TowerPlacement : MonoBehaviour {

	public Transform placementGrid;
	public LayerMask placementLayerMask;
	public Material hoverMat;
	private Material originalMat;
	private GameObject lastHitObj;

	public Color onColor;
	public Color offColor;
	public GameObject[] buildings;
	//	private int buildingIndex;
	private GameObject building;

	// Use this for initialization
	void Start () {
		buildingIndex = 0;
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
						GameObject newBuilding = Instantiate(building, lastHitObj.transform.position, Quaternion.identity) as GameObject;
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
	}
}
