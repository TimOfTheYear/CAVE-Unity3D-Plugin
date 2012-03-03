using UnityEngine;
using System.Collections;

public class NetworkDisplay : MonoBehaviour {
	
	public int Cam = 0;
	
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetAxis("Start Button") != 0) {
			Network.SetSendingEnabled(0, true);
			Time.timeScale = 1.0f;
			Destroy(this.gameObject);
		}
	}
	
	void OnPlayerConnected(NetworkPlayer player) {
		
	}
	
	void OnLevelWasLoaded (int level)
	{
		if(Network.isServer) {
			Debug.Log("I am a server.");
			Network.SetSendingEnabled(0, false);
			Time.timeScale = 0.0f;
			MainCameraInit();
		}
		else if(Network.isClient)
		{
			Debug.Log("I am a client.");
			ClientCamInit();
		}
		
	}
	
	void MainCameraInit()
	{
		GameObject disableCamera = GameObject.Find("Left Camera");
		foreach (Camera c in disableCamera.GetComponentsInChildren<Camera>())
		{
			c.enabled = false;
		}	
		
		disableCamera = GameObject.Find("Right Camera");
		foreach (Camera c in disableCamera.GetComponentsInChildren<Camera>())
		{
			c.enabled = false;
		}
		
		disableCamera = GameObject.Find("Bottom Camera");
		foreach (Camera c in disableCamera.GetComponentsInChildren<Camera>())
		{
			c.enabled = false;
		}
	}
	
	protected virtual void ClientCamInit()
	{
		switch(Cam){
			case 1: LeftCameraInit();
					break;
			case 2: BottomCameraInit();
					break;
			case 3: RightCameraInit();
					break;
		}
		
		Destroy(this.gameObject);
	}
	
	void LeftCameraInit()
	{
		GameObject disableCamera = GameObject.Find("Main Camera");
		foreach (Camera c in disableCamera.GetComponentsInChildren<Camera>())
		{
			c.enabled = false;
		}
		
		
		disableCamera = GameObject.Find("Right Camera");
		foreach (Camera c in disableCamera.GetComponentsInChildren<Camera>())
		{
			c.enabled = false;
		}
		
		disableCamera = GameObject.Find("Bottom Camera");
		foreach (Camera c in disableCamera.GetComponentsInChildren<Camera>())
		{
			c.enabled = false;
		}
	}
	
	void RightCameraInit()
	{
		GameObject disableCamera = GameObject.Find("Main Camera");
		foreach (Camera c in disableCamera.GetComponentsInChildren<Camera>())
		{
			c.enabled = false;
		}
		
		
		disableCamera = GameObject.Find("Left Camera");
		foreach (Camera c in disableCamera.GetComponentsInChildren<Camera>())
		{
			c.enabled = false;
		}
		
		disableCamera = GameObject.Find("Bottom Camera");
		foreach (Camera c in disableCamera.GetComponentsInChildren<Camera>())
		{
			c.enabled = false;
		}
	}
	
	void BottomCameraInit()
	{
		GameObject disableCamera = GameObject.Find("Main Camera");
		foreach (Camera c in disableCamera.GetComponentsInChildren<Camera>())
		{
			c.enabled = false;
		}
		
		
		disableCamera = GameObject.Find("Right Camera");
		foreach (Camera c in disableCamera.GetComponentsInChildren<Camera>())
		{
			c.enabled = false;
		}
		
		disableCamera = GameObject.Find("Left Camera");
		foreach (Camera c in disableCamera.GetComponentsInChildren<Camera>())
		{
			c.enabled = false;
		}
	}
}