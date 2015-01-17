using UnityEngine;
using System.Collections;
using Prime31;
using System.Collections.Generic;
using System.Globalization;
using System;


public class ColorGridMessageHandler : MonoBehaviour {
	private GameObject button;
	public static DateTime endtime =System.DateTime.MaxValue;
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

	void multiPeerMessageReceiver( string param )
	{
		var theStr = param;
		//Debug.Log( "received raw message from peer: " + peerId );
		Debug.Log( "message: " + theStr );
			string[] message = theStr.Split (',');
			Debug.Log("SHIPPPPPPP");
			Vector2 newPosition = new Vector2 (float.Parse (message [1]), float.Parse (message [2]));
			DateTime timestamp = DateTime.ParseExact (message [3], "MM/dd/yyyy HH:mm:ss.fffff", CultureInfo.InvariantCulture);
			if (DateTime.Compare (timestamp, endtime) <= 0) {
				GameObject.Find (message [1] + "," + message [2]).GetComponent<ColorSquareMovement> ().UpdateColor (theStr);
			}

	}


	#endregion
}
