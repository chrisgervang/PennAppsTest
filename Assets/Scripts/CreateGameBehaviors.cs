using UnityEngine;
using System.Collections;
using Prime31;
using System.Collections.Generic;
using System;

public class CreateGameBehaviors : MonoBehaviour {

	private GameObject button;
	private List<string> sortedPlayers = new List<string>();
	public static int playerId=-1;
	// Use this for initialization

	void Start () {
		Debug.Log (SystemInfo.deviceName);
		MultiPeerManager.browserFinishedEvent += browserFinishedEvent;
		MultiPeerManager.receivedRawDataEvent += multiPeerRawMessageReceiver;

	}

	void browserFinishedEvent( string param )
	{
		Debug.Log( "browserFinishedEvent: " + param );
		if (param == "done") {
			Debug.Log ("browser done");
			updatePlayers();
			sendStart ();
			Application.LoadLevel("Grid");

		}
	}
	void updatePlayers(){
		var peers = MultiPeerBinding.getConnectedPeers();
		peers.Add (SystemInfo.deviceName);
		peers.Sort();
		sortedPlayers =peers;
		playerId = sortedPlayers.IndexOf (SystemInfo.deviceName);
		Debug.Log ("the players are:");
		sortedPlayers.ForEach(Debug.Log);
		Debug.Log ("the player is id:" + playerId);

	}
	void sendStart (){
		var theStr = "start game";
		var bytes = System.Text.UTF8Encoding.UTF8.GetBytes( theStr );

		var result = MultiPeerBinding.sendRawMessageToAllPeers( bytes );
		Debug.Log( "send result: " + result );
	}
	// Update is called once per frame
	void Update () {
		if (Input.touchCount == 1) {
			Touch currentTouch = Input.GetTouch(0);
			Vector2 v2 = new Vector2(Camera.main.ScreenToWorldPoint(currentTouch.position).x, Camera.main.ScreenToWorldPoint(currentTouch.position).y);

			if (currentTouch.phase == TouchPhase.Began) {
				Debug.Log("BUTTON BEGIN");

				Collider2D c2d = Physics2D.OverlapPoint(v2);
				if (c2d != null && c2d.gameObject.name == "PairButton") {
					button = c2d.gameObject;
					Debug.Log("COLLIDED WITH: Button: " + button.name);
					if (c2d != null) {


						MultiPeerBinding.showPeerPicker();

					}
				}
			}

		}
	}

	#region Message receivers

	void multiPeerMessageReceiver( string param )
	{
		Debug.Log( "received a message: " + param );
		var theStr = param;
		//Debug.Log( "received raw message from peer: " + peerId );
		Debug.Log( "message: " + theStr );
		if (theStr == "start game") {
			updatePlayers();
		}
		else {
			string[] message = theStr.Split (',');

			Vector2 newPosition = new Vector2 (float.Parse (message [1]), float.Parse (message [2]));
			GameObject.Find (message[1]+","+message[2]).GetComponent<ColorSquareMovement>().UpdateColor(theStr);

		}
	}


	void multiPeerRawMessageReceiver( string peerId, byte[] bytes )
	{
		var theStr = System.Text.UTF8Encoding.UTF8.GetString( bytes );
		Debug.Log( "received raw message from peer: " + peerId );
		Debug.Log( "message: " + theStr );
		if (theStr == "start game") {
			updatePlayers();
			Application.LoadLevel("Grid");
		} else {
			string[] message = theStr.Split (',');

			Vector2 newPosition = new Vector2 (float.Parse (message [1]), float.Parse (message [2]));
			GameObject.Find (message[1]+","+message[2]).GetComponent<ColorSquareMovement>().UpdateColor(theStr);

		}
	}

	#endregion

	public void HostClicked () {
		MultiPeerBinding.advertiseCurrentDevice( true, "Hijinks" );
		MultiPeerBinding.showPeerPicker();
	}

	public void JoinClicked () {
		MultiPeerBinding.advertiseCurrentDevice( true, "Hijinks" );
	}

}
