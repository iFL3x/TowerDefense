using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ServerList : MonoBehaviour {


	private string gameKey = "RTS-YoloSwagHose";
	private Ping masterServerPing;
	private List<Ping> serverPingList = new List<Ping>();
	public HostData[] serverList;

	public bool listUpdated = false;

	// Use this for initialization
	void Start () {
		MasterServer.RequestHostList(gameKey);
		masterServerPing = new Ping(MasterServer.ipAddress);

	}


	public void RefreshServerList(){
		StartCoroutine(TalkToMasterServer());
	}

	IEnumerator TalkToMasterServer(){

		//Clear Hostdata list
		serverList = new HostData[0];
		
		//Clear Server Host List
		MasterServer.ClearHostList();
		//Get all active Servers
		MasterServer.RequestHostList(gameKey);

		//Wait a bit until the host list is complete
		yield return new WaitForSeconds(masterServerPing.time/100 + 0.1f);
		
		
		//Put them in an array
		serverList = MasterServer.PollHostList();


		if(serverList.Length == 0){
			//serverSearch = ServerSearch.NoServerFound;
		} 
		//Clear the serverPing list used to show the different servers ping later
		serverPingList.Clear();
		serverPingList.TrimExcess();
		//Fill the list with all server pings
		if(serverList.Length != 0){
			foreach(HostData hd in serverList){
				serverPingList.Add(new Ping(hd.ip[0]));
			}
			//serverSearch = ServerSearch.ServerFound;
		}

		listUpdated = true;

	}
}
