using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour {

	private const string typeName = "UMDemo";
	private const string gameName = "UMRoom";
	private HostData[] hostList;

	// Called when successfully joined a hosted game
	void OnConnectedToServer() {
		Debug.Log("Server Joined");
	}

	// Draw UI buttons
	void OnGUI() {
		if (!Network.isClient && !Network.isServer) {
			if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server"))
            StartServer();

      if (GUI.Button(new Rect(100, 250, 250, 100), "Refresh Hosts"))
          RefreshHostList();

      if (hostList != null) {
      	for (int i = 0; i < hostList.Length; i++) {
          if (GUI.Button(new Rect(400, 100 + (110 * i), 300, 100), hostList[i].gameName))
                  JoinServer(hostList[i]);
        }
      }
		}
	}

	// Called when events are triggered by the master server
	void OnMasterServerEvent(MasterServerEvent msEvent) {
		// Master server sent a list of hosts
		if (msEvent == MasterServerEvent.HostListReceived)
			hostList = MasterServer.PollHostList();
	}

	// Called if the server is successfully Initialised
	void OnServerInitialized() {
		Debug.Log("Server initialised");
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	// Join a hosted game
	private void JoinServer(HostData hostData) {
		Network.Connect(hostData);
	}

	// Update the hosts list
	private void RefreshHostList() {
		MasterServer.RequestHostList(typeName);
	}

	// Initialize game server
	private void StartServer() {
		Network.InitializeServer(4, 25000, !Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, gameName);
	}

}
