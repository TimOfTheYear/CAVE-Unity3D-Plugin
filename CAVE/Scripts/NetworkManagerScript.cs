using UnityEngine;
using System.Collections;

public class NetworkManagerScript : MonoBehaviour {
	
	private string gamename;
	private bool refreshing;
	private HostData[] hostData;
	public NetworkDisplay netDisplay;
	private string message = "";
	public string SceneToLoad;
	public string ip;
	public int port;
	public int connections;
	public bool UseNAT;
	public bool UseMasterServer;
	public string ServerName;
	public string ServerDescription;

	// Use this for initialization
	void Start () 
	{
		gamename = "CAVE_MultiCam";
		//MasterServer.ipAddress = "192.168.0.1";
		//MasterServer.port = 23466;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (refreshing) {
		
			if (MasterServer.PollHostList().Length > 0) {
			
				refreshing = false;
				Debug.Log(MasterServer.PollHostList().Length);
				
				hostData = MasterServer.PollHostList();
			}
			
		}
	
	}
	
	void StartServer() {
	
		Network.InitializeServer(connections, port, UseNAT);
		if(UseMasterServer) {
			MasterServer.RegisterHost( gamename, ServerName, ServerDescription);
		}
		Application.LoadLevel(SceneToLoad);
		
	}
	
	void OnServerInitialized() {
	
		Debug.Log("Server Started");
		
	}
	
	void OnMasterServerEvent(MasterServerEvent mse) {
	
		if (mse == MasterServerEvent.RegistrationSucceeded) {
		
			Debug.Log("Registered Server");
			
		}
		
	}
	
	void refreshHostList() {
		
		MasterServer.RequestHostList(gamename);
		refreshing = true;
		
	}
	
	void OnGUI() 
	{
	
		if (!Network.isClient && !Network.isServer) 
		{
			
			GUI.Label(new Rect(100, 100, 500, 500), "WELCOME TO THE CAVE! \n\n" +
				"Press 'S' to start the Server.\nPress 'L' to connect as left screen.\n" +
				"Press 'R' to start as right screen.\nPress 'B' to start as bottom screen.");
		
			if (Input.GetKeyDown(KeyCode.S)) {  // MAIN
				StartServer();
			}
			if (Input.GetKeyDown(KeyCode.L)) {	// LEFT
				netDisplay.Cam = 1;
				message = "Waiting on server...";
				Network.Connect(ip, port);
			} 
			if (Input.GetKeyDown(KeyCode.B)) { // BOTTOM
				netDisplay.Cam = 2;
				message = "Waiting on server...";
				Network.Connect(ip, port);
			}
			if (Input.GetKeyDown(KeyCode.R)) {  // RIGHT
				netDisplay.Cam = 3;
				message = "Waiting on server...";
				Network.Connect(ip, port);
			}
			
			GUI.Label(new Rect(550, 400, 150, 150), message);
			
			/*if (GUI.Button(new Rect(150, 50, 100, 100), "3DTest 1")) {
				Application.LoadLevel("3DTest3");
			}
			
			if (GUI.Button(new Rect(300, 50, 100, 100), "3DTest 2")) {
				Application.LoadLevel("3DTest2");
			}
			
			if (GUI.Button(new Rect(550, 50, 150, 150), "Refresh Hosts")) {
				refreshHostList();
			}
			
			if (GUI.Button(new Rect(550, 350, 50, 50), "Left")) {
						netDisplay.Cam = 1;
						Network.Connect(ip, port);
						
					} 
					if (GUI.Button(new Rect(600, 350, 50, 50), "Bottom")) {
						netDisplay.Cam = 2;
						Network.Connect(ip, port);
					}
					if (GUI.Button(new Rect(650, 350, 50, 50), "Right")) {
						netDisplay.Cam = 3;
						Network.Connect(ip, port);
					}
			
			if(hostData !=  null)
				for(int i = 0; i < hostData.Length; i++) 
				{
					GUI.Label(new Rect(550, 300, 150, 50), hostData[i].gameName);
				
					if (GUI.Button(new Rect(550, 350, 50, 50), "Left")) {
						netDisplay.Cam = 1;
						Network.Connect(hostData[i]);
						
					} 
					if (GUI.Button(new Rect(600, 350, 50, 50), "Bottom")) {
						netDisplay.Cam = 2;
						Network.Connect(hostData[i]);
					}
					if (GUI.Button(new Rect(650, 350, 50, 50), "Right")) {
						netDisplay.Cam = 3;
						Network.Connect(hostData[i]);
					}
				
					
					
				}			*/
		}
	}
	
	void OnConnectedToServer () {
		Application.LoadLevel(SceneToLoad);
	}
	
	void OnFailedToConnect(NetworkConnectionError error) {
    	message = "Could not connect to server: " + error;
	}
}
