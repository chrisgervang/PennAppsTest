using UnityEngine;
using System.Collections;
using System.Globalization;
using Prime31;
using System.Collections.Generic;
using System;

public class CreateGameBehaviors : MonoBehaviour {
	public static List<string> sortedPlayers = new List<string>();
	public static int playerId=-1;

	public static TimeSpan currentGameStartTime = TimeSpan.Zero;

	public static TimeSpan currentSessionStartTime = TimeSpan.Zero;

	private GameObject button;

	public void HostClicked () {
		Debug.Log("Clicked Host");
		MultiPeerBinding.advertiseCurrentDevice( true, "Hijinks" );
		MultiPeerBinding.showPeerPicker();
	}

	public void JoinClicked () {
		Debug.Log("Clicked Join");
		MultiPeerBinding.advertiseCurrentDevice( true, "Hijinks" );
	}


	public static void updatePlayers(){
		var peers = MultiPeerBinding.getConnectedPeers();
		peers.Add (SystemInfo.deviceName);
		peers.Sort();
		sortedPlayers = peers;
		playerId = sortedPlayers.IndexOf (SystemInfo.deviceName);
		Debug.Log ("the players are:");
		sortedPlayers.ForEach(Debug.Log);
		Debug.Log ("the player is id:" + playerId);

	}

	// Use this for initialization
	void Start () {
		Debug.Log (SystemInfo.deviceName);
		MultiPeerManager.browserFinishedEvent += browserFinishedEvent;
		//MultiPeerManager.receivedRawDataEvent += multiPeerRawMessageReceiver;
		setResolutionToLandscape();
	}

	// Update is called once per frame
	void Update () {

	}


	//////Bluetooth init functions
	void browserFinishedEvent( string param )
	{
		Debug.Log( "browserFinishedEvent: " + param );
		if (param == "done") {
			Debug.Log ("browser done");
			updatePlayers();
			currentGameStartTime = TimeSpan.FromTicks(System.DateTime.Now.Ticks);
			currentSessionStartTime = currentGameStartTime;
			sendStart ();
			Application.LoadLevel("Grid"); /////Load level

		}
	}

	void sendStart (){
		var theStr = "start game"+","+currentGameStartTime.ToString();
		//var bytes = System.Text.UTF8Encoding.UTF8.GetBytes( theStr ); Removed because a raw message be recieved.

		var result = MultiPeerBinding.sendMessageToAllPeers( "Gizmo", "multiPeerMessageReceiver", theStr, true );
		Debug.Log( "send result: " + result );
	}
	/////////

	#region Message receivers

	void multiPeerMessageReceiver( string param )
	{
		Debug.Log( "received a message: " + param );
		var theStr = param;
		//Debug.Log( "received raw message from peer: " + peerId );
		Debug.Log( "message: " + theStr );
		if (theStr.IndexOf("start game") != -1) {
			string[] message = theStr.Split (',');
			//currentGameStartTime = TimeSpan.FromTicks(DateTime.ParseExact(message [1], "d.HH:mm:ss.fffff", CultureInfo.InvariantCulture).Ticks);
			currentGameStartTime = TimeSpan.Parse(message [1]);
			currentSessionStartTime = currentGameStartTime;
			updatePlayers();
			Application.LoadLevel("Grid");
		}
		else {
			string[] message = theStr.Split (',');

			Vector2 newPosition = new Vector2 (float.Parse (message [1]), float.Parse (message [2]));
			GameObject.Find (message[1]+","+message[2]).GetComponent<ColorSquareMovement>().UpdateColor(theStr);

		}
	}

	// Not being used for now...
	// void multiPeerRawMessageReceiver( string peerId, byte[] bytes )
	// {
	// 	var theStr = System.Text.UTF8Encoding.UTF8.GetString( bytes );
	// 	Debug.Log( "received raw message from peer: " + peerId );
	// 	Debug.Log( "message: " + theStr );
	// 	if (theStr.IndexOf("start game") != -1) {
	// 		string[] message = theStr.Split (',');
	// 		currentGameStartTime = TimeSpan.ParseExact(message[1], "MM/dd/yyyy HH:mm:ss.fffff", CultureInfo.InvariantCulture);
	// 		updatePlayers();
	// 		Application.LoadLevel("Grid");
	// 	} else {
	// 		string[] message = theStr.Split (',');
	//
	// 		Vector2 newPosition = new Vector2 (float.Parse (message [1]), float.Parse (message [2]));
	// 		GameObject.Find (message[1]+","+message[2]).GetComponent<ColorSquareMovement>().UpdateColor(theStr);
	//
	// 	}
	// }

	#endregion

	public static void setResolutionToLandscape() {
		if( iPhoneSettings.generation == iPhoneGeneration.iPhone || iPhoneSettings.generation == iPhoneGeneration.iPhone3G ||iPhoneSettings.generation == iPhoneGeneration.iPhone3GS ||iPhoneSettings.generation == iPhoneGeneration.iPodTouch2Gen || iPhoneSettings.generation == iPhoneGeneration.iPodTouch3Gen || iPhoneSettings.generation == iPhoneGeneration.iPodTouch4Gen) {
			//480*480
			Screen.SetResolution(480, 480, true);
			Screen.orientation = ScreenOrientation.LandscapeLeft;
			Debug.Log("Setting Resolution to 480,480");
		} else if( iPhoneSettings.generation == iPhoneGeneration.iPhone4 ) {
			//960*640
			Screen.SetResolution(960, 640, true);
			Screen.orientation = ScreenOrientation.LandscapeLeft;
			Debug.Log("Setting Resolution to 960,640");
		} else if( iPhoneSettings.generation == iPhoneGeneration.iPad1Gen || iPhoneSettings.generation == iPhoneGeneration.iPad2Gen ) {
			//1024*768
			Screen.SetResolution(1024, 768, true);
			Screen.orientation = ScreenOrientation.LandscapeLeft;
			Debug.Log("Setting Resolution to 1024,768");
		} else if( iPhoneSettings.generation == iPhoneGeneration.iPhone5 || iPhoneSettings.generation == iPhoneGeneration.iPhone5S || iPhoneSettings.generation == iPhoneGeneration.iPhone5C ) {

			Screen.SetResolution(1136, 640, true);
			Screen.orientation = ScreenOrientation.LandscapeLeft;
			Debug.Log("Setting Resolution to 1136,640");
		} else if( iPhoneSettings.generation == iPhoneGeneration.iPhone6 ) {

			Screen.SetResolution(1334, 750, true);
			Screen.orientation = ScreenOrientation.LandscapeLeft;
			Debug.Log("Setting Resolution to 1334,750");
		}  else {
			//Unknown device or are you in the editor?
			Debug.Log("AutoResolution: Unknown device or are you in the editor?");
			CreateGameBehaviors.playerId = 0;
		}

	}

	public static void setResolutionToPortrait() {
		if( iPhoneSettings.generation == iPhoneGeneration.iPhone || iPhoneSettings.generation == iPhoneGeneration.iPhone3G ||iPhoneSettings.generation == iPhoneGeneration.iPhone3GS ||iPhoneSettings.generation == iPhoneGeneration.iPodTouch2Gen || iPhoneSettings.generation == iPhoneGeneration.iPodTouch3Gen || iPhoneSettings.generation == iPhoneGeneration.iPodTouch4Gen) {
			//480*480
			Screen.SetResolution(480, 480, true);
			Screen.orientation = ScreenOrientation.Portrait;
			Debug.Log("Setting Resolution to 480,480");
		} else if( iPhoneSettings.generation == iPhoneGeneration.iPhone4 ) {
			//960*640
			Screen.SetResolution(640,960, true);
			Screen.orientation = ScreenOrientation.Portrait;
			Debug.Log("Setting Resolution to 960,640");
		} else if( iPhoneSettings.generation == iPhoneGeneration.iPad1Gen || iPhoneSettings.generation == iPhoneGeneration.iPad2Gen ) {
			//1024*768
			Screen.SetResolution(768,1024, true);
			Screen.orientation = ScreenOrientation.Portrait;
			Debug.Log("Setting Resolution to 1024,768");
		} else if( iPhoneSettings.generation == iPhoneGeneration.iPhone5 || iPhoneSettings.generation == iPhoneGeneration.iPhone5S || iPhoneSettings.generation == iPhoneGeneration.iPhone5C ) {

			Screen.SetResolution(640,1136, true);
			Screen.orientation = ScreenOrientation.Portrait;
			Debug.Log("Setting Resolution to 1136,640");
		} else if( iPhoneSettings.generation == iPhoneGeneration.iPhone6 ) {

			Screen.SetResolution(750,1334, true);
			Screen.orientation = ScreenOrientation.Portrait;
			Debug.Log("Setting Resolution to 1334,750");
		}  else {
			//Unknown device or are you in the editor?
			Debug.Log("AutoResolution: Unknown device or are you in the editor?");
			CreateGameBehaviors.playerId = 2;
		}

	}

}
