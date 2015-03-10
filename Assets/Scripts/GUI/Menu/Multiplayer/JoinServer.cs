using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class JoinServer : MonoBehaviour {


	ServerList serverList;
	public GameObject serverPrefab;
	public GameObject targetPanel;

	// Use this for initialization
	void Start () {
		serverList = GameObject.Find ("ServerManager").GetComponent<ServerList>();
		serverList.RefreshServerList();
	}

	void Update(){
		if(serverList.listUpdated) UpdateServerList();
	}

	private void UpdateServerList(){
		serverList.listUpdated = false;

		foreach(GameObject child in GameObject.FindGameObjectsWithTag("JoinServerElement")){
			GameObject.Destroy(child);
		}


		int counter = 0;
		foreach(HostData hd in serverList.serverList){
			GameObject server = (GameObject)Instantiate(serverPrefab);
			server.name = hd.gameName;
			foreach(Transform child in server.transform){
				if(child.name == "ServerName"){
					child.GetComponent<Text>().text = hd.gameName;
				}
				if(child.name == "Connect"){
					child.GetComponent<Button>().onClick.AddListener(() => { GameObject.Find ("ConnectionManager").GetComponent<ConnectionManager>().ConnectToServer(hd.ip[0], hd.port);  });
				}
			}
			server.transform.SetParent(targetPanel.transform, false);
			server.transform.position = new Vector3(server.transform.position.x, server.transform.position.y - counter * 60 - counter * 0, server.transform.position.z);

			counter++;
		}
	}

}
