using UnityEngine;
using System.Collections;
using Prime31;
using System.Collections.Generic;
using System.Globalization;
using System;

//This handle the set up and behaviors of the scene as a whole. Includes message reciever.
public class ColorGridMessageHandler : MonoBehaviour {
	public static DateTime endtime = System.DateTime.MaxValue; //Init to crazy high time
	public GameObject timer;
	// Use this for initialization

	void Start () {
		endtime = CreateGameBehaviors.starttimestamp.AddSeconds (63);
		Debug.Log (endtime);

		GameObject newTimer = Instantiate (timer) as GameObject;
		newTimer.GetComponent<Canvas> ().worldCamera = Camera.main;
		newTimer.GetComponent<Timer> ().canvas1 = newTimer;
	}

	// Update is called once per frame
	void Update () {

	}

	#region Message receivers

	void multiPeerMessageReceiver( string param ) {
		var theStr = param;
		string[] message = theStr.Split (',');

		Vector2 newPosition = new Vector2 (float.Parse (message [1]), float.Parse (message [2]));

		DateTime mostRecentIncomingColorChangePacket = DateTime.ParseExact (message [3], "MM/dd/yyyy HH:mm:ss.fffff", CultureInfo.InvariantCulture);
		//Debug.Log("MessageRecieverTimeStamp"+mostRecentIncomingColorChangePacket.ToString("MM/dd/yyyy HH:mm:ss.fffff"));
		if (DateTime.Compare (mostRecentIncomingColorChangePacket, endtime) <= 0) {
			GameObject.Find (message [1] + "," + message [2]).GetComponent<ColorSquareMovement> ().UpdateColor (theStr);
		}

	}

	#endregion
}
