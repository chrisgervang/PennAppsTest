using UnityEngine;
using System.Collections;
using Prime31;
using System.Collections.Generic;
using System.Globalization;
using System;

//This handle the set up and behaviors of the scene as a whole. Includes message reciever.
public class ColorGridMessageHandler : MonoBehaviour {
	public static TimeSpan endtime = TimeSpan.Zero; //Init to crazy high time
	public GameObject timer;
	// Use this for initialization

	void Start () {
		endtime = CreateGameBehaviors.currentGameStartTime + new TimeSpan(0,0,63);
		Debug.Log (endtime);
	}

	// Update is called once per frame
	void Update () {

	}

	#region Message receivers

	void multiPeerMessageReceiver( string param ) {
		var theStr = param;
		string[] message = theStr.Split (',');

		Vector2 newPosition = new Vector2 (float.Parse (message [1]), float.Parse (message [2]));

		//TimeSpan mostRecentIncomingColorChangePacket = TimeSpan.FromTicks(DateTime.ParseExact(message [3], "hh:mm:ss.fffff", CultureInfo.InvariantCulture).Ticks);
		TimeSpan mostRecentIncomingColorChangePacket = TimeSpan.Parse(message [3]);
		//Debug.Log("MessageRecieverTimeStamp"+mostRecentIncomingColorChangePacket.ToString("MM/dd/yyyy hh:mm:ss.fffff"));
		if (TimeSpan.Compare (new TimeSpan(mostRecentIncomingColorChangePacket.Ticks), new TimeSpan(endtime.Ticks)) <= 0) {
			GameObject.Find (message [1] + "," + message [2]).GetComponent<ColorSquareMovement> ().UpdateColor (theStr);
		}

	}

	#endregion
}
