using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TurretPlacement: MonoBehaviour {

	public Transform placementGrid;
	public LayerMask placementLayerMask;
	public Material hoverMat;
	private Material originalMat;
	private GameObject lastHitObj;
	
	public GameObject[] buildings;
	private GameObject building;
	private bool gridActive = true;

	private bool upgradePanelActive = false;
	private PlacementPlane focusedPlane;
	private Turret buildingToUpgrade;
	private GameObject upgradeBuilding;
	private int upgradeCosts;
	
	private GUIMessages guiMessages;

	// Use this for initialization
	void Start () {
		guiMessages = GameObject.Find ("GUIMessages").GetComponent<GUIMessages>();
		SetGridActive(true);
		SelectTower(0);
	}
	
	void Update(){
		
		CheckTurretSelection();
		
		if(gridActive){
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
				if(Input.GetMouseButtonDown(0) && lastHitObj && !upgradePanelActive){

					focusedPlane = lastHitObj.GetComponent<PlacementPlane>();

					if(focusedPlane.isOpen && CheckCash(building.GetComponent<Turret>().costs)){
						SpendCash(building.GetComponent<Turret>().costs);

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

						focusedPlane.building = newBuilding;
						focusedPlane.isOpen = false;

						//building = null;
						//SetGridActive(false);
					} else if(focusedPlane.building != null){
						ShowUpgradeGUI();
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
			gridActive = true;
		} else {
			placementGrid.gameObject.SetActive(false);
			gridActive = false;
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
			SelectTower(3);
		}
		if(Input.GetKeyDown(KeyCode.Alpha5)){
			
		}
	}

	public void SelectTower(int index){
		building = buildings[index];
		SetGridActive(true);
		GetComponent<GameManager>().SetSelectedTower(index);
	}	


	private void ShowUpgradeGUI(){

		buildingToUpgrade = focusedPlane.building.GetComponent<Turret>();
		upgradeBuilding = buildingToUpgrade.upgrade;

		if(upgradeBuilding != null){
			upgradePanelActive = true;

			upgradeCosts = buildingToUpgrade.upgradeCosts;
			string upgradeName = buildingToUpgrade.upgradeName;

			guiMessages.SetUpgradeText("Upgrade to " + upgradeName + " for $" + upgradeCosts + "?");
			CostCheckButton(upgradeCosts);
			guiMessages.ShowUpgradeGUI(true);
		}

	}

	private void CostCheckButton(int costs){
		if(CheckCash(costs)){
			guiMessages.SetUpgradeButtonActive(true);
		} else {
			guiMessages.SetUpgradeButtonActive(false);
		}
	}

	public void CancelUpgrade(){
		upgradePanelActive = false;
		guiMessages.ShowUpgradeGUI(false);
	}
	
	public void ConfirmUpgrade(){
		upgradePanelActive = false;
		guiMessages.ShowUpgradeGUI(false);

		Vector3 spawnPos = buildingToUpgrade.transform.position;
		Quaternion spawnRot = buildingToUpgrade.transform.rotation;
		Destroy (buildingToUpgrade.gameObject);
		GameObject newBuilding = Instantiate(upgradeBuilding, spawnPos, spawnRot) as GameObject;
		focusedPlane.building = newBuilding;

		SpendCash(upgradeCosts);
		GetComponent<GameManager>().UpdateHUD();
	}
}
