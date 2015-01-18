using UnityEngine;
using System.Collections;
using System.Globalization;
using System;
using Prime31;

public class ColorSquareMovement : MonoBehaviour {
	//PUBLICS
	public Color[] squareColor = {Color.blue, Color.red, Color.green, Color.yellow, Color.magenta, Color.cyan};
	public GameObject square;
	//Privates
	public int playerId = -1; //-1 when the player is touching the screen, or when the square hasn't be touched by a player yet.
	private DateTime LocalSquareLastChangedTimestamp = System.DateTime.MinValue; //crazy low time so nothing is below it.

	// Use this for initialization
	void Start () {

	}

	void SendCoordinates (string coordinates){
		var theStr = CreateGameBehaviors.playerId+","+square.name+","+LocalSquareLastChangedTimestamp.ToString("MM/dd/yyyy HH:mm:ss.fffff", CultureInfo.InvariantCulture);
		//var bytes = System.Text.UTF8Encoding.UTF8.GetBytes( theStr );
		Debug.Log("Timestamp String Send Cords: " + LocalSquareLastChangedTimestamp.ToString("MM/dd/yyyy HH:mm:ss.fffff"));
		var result = MultiPeerBinding.sendMessageToAllPeers( "ColorGridGizmo", "multiPeerMessageReceiver", theStr, true );
		//Debug.Log( "send result: " + result );
	}

	public void UpdateColor (string theStr){
		string[] message = theStr.Split (',');
		DateTime IncomingColorChangePacket = DateTime.ParseExact (message [3], "MM/dd/yyyy HH:mm:ss.fffff", CultureInfo.InvariantCulture);
		Debug.Log ("TimeDates - LocalSquareLastChangedTimestamp: "+LocalSquareLastChangedTimestamp.ToString("MM/dd/yyyy HH:mm:ss.fffff", CultureInfo.InvariantCulture)+" - IncomingColorChangePacket: "+IncomingColorChangePacket.ToString("MM/dd/yyyy HH:mm:ss.fffff", CultureInfo.InvariantCulture)+" - Compare: "+DateTime.Compare (LocalSquareLastChangedTimestamp,IncomingColorChangePacket));



		if (DateTime.Compare (LocalSquareLastChangedTimestamp, IncomingColorChangePacket) <= 0) {
			Debug.Log ("Incoming square is newer! Changeing square: " + square.name);
			playerId = int.Parse (message[0]);
			LocalSquareLastChangedTimestamp = IncomingColorChangePacket;
			if(playerId == -1){
				square.transform.renderer.material.color = Color.white;
			} else{
				square.transform.renderer.material.color = squareColor[playerId];
			}

		}
	}
	// Update is called once per frame
	void Update () {

		foreach (Touch currentTouch in Input.touches) {
			Vector2 v2 = new Vector2(Camera.main.ScreenToWorldPoint(currentTouch.position).x, Camera.main.ScreenToWorldPoint(currentTouch.position).y);

			if (currentTouch.phase == TouchPhase.Began) {
				//Debug.Log("BEGAN");

				Collider2D c2d = Physics2D.OverlapPoint(v2);

				if (c2d != null && c2d.gameObject.name == square.name) {

					DateTime now1 = System.DateTime.Now; //Save time at collision. Looks like:

					//If now1 is earlier than the set end time of the current game.
					if (DateTime.Compare (now1, ColorGridMessageHandler.endtime) <= 0 ) {
						playerId = CreateGameBehaviors.playerId;
						LocalSquareLastChangedTimestamp = now1;

						//default to white... but this will never happen?
						if(playerId ==-1){
							square.transform.renderer.material.color = Color.white;
						} else {
							square.transform.renderer.material.color = squareColor[CreateGameBehaviors.playerId];
						}

						Debug.Log ("ColorSquare: "+square.name+ " Time: "+LocalSquareLastChangedTimestamp.ToString("MM/dd/yyyy HH:mm:ss.fffff", CultureInfo.InvariantCulture));

						SendCoordinates(c2d.gameObject.name);
					}

				}

			} else if (currentTouch.phase == TouchPhase.Moved) {
				//Debug.Log("MOVED");

			} else if (currentTouch.phase == TouchPhase.Ended) {
				//Debug.Log("ENDED");
			}

		}
	}
}
