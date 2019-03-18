using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour {

	private const string typeName = "UMDemo";
	private const string gameName = "UMRoom";

	// Draw UI buttons
	void OnGUI() {
		if (!Network.isClient && !Network.isServer) {
			if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server"))
            StartServer();
		}
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

	// Initialize game server
	private void StartServer() {
		Network.InitializeServer(4, 25000, !Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, gameName);
	}

}
