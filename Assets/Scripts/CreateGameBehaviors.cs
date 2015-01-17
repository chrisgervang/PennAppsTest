using UnityEngine;
using System.Collections;
using System.Globalization;
using Prime31;
using System.Collections.Generic;
using System;

public class CreateGameBehaviors : MonoBehaviour {
	public static List<string> sortedPlayers = new List<string>();
	public static int playerId=-1;
	public static DateTime starttimestamp = System.DateTime.MinValue;
	private GameObject button;
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
			starttimestamp = System.DateTime.Now;
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
		var theStr = "start game"+","+starttimestamp.ToString("MM/dd/yyyy HH:mm:ss.fffff", CultureInfo.InvariantCulture);
		var bytes = System.Text.UTF8Encoding.UTF8.GetBytes( theStr );

		var result = MultiPeerBinding.sendRawMessageToAllPeers( bytes );
		Debug.Log( "send result: " + result );
	}
	// Update is called once per frame
	void Update () {
		
	}

	#region Message receivers

	void multiPeerMessageReceiver( string param )
	{
		Debug.Log( "received a message: " + param );
		var theStr = param;
		//Debug.Log( "received raw message from peer: " + peerId );
		Debug.Log( "message: " + theStr );
		if (theStr.IndexOf("start game")!=-1) {
			string[] message = theStr.Split (',');
			updatePlayers();
			starttimestamp = DateTime.ParseExact(message[1], "MM/dd/yyyy HH:mm:ss.fffff", CultureInfo.InvariantCulture);
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
		if (theStr.IndexOf("start game")!=-1) {
			string[] message = theStr.Split (',');
			starttimestamp = DateTime.ParseExact(message[1], "MM/dd/yyyy HH:mm:ss.fffff", CultureInfo.InvariantCulture);
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
		Debug.Log("Clicked Host");
		MultiPeerBinding.advertiseCurrentDevice( true, "Hijinks" );
		MultiPeerBinding.showPeerPicker();
	}

	public void JoinClicked () {
		Debug.Log("Clicked Join");
		MultiPeerBinding.advertiseCurrentDevice( true, "Hijinks" );
	}

}
