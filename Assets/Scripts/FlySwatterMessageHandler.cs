using UnityEngine;
using System.Collections;
using Prime31;
using System.Collections.Generic;
using System.Globalization;
using System;

//This handle the set up and behaviors of the scene as a whole. Includes message reciever.
public class FlySwatterMessageHandler : MonoBehaviour {
	public static DateTime endtime = System.DateTime.MaxValue; //Init to crazy high time
	public GameObject timer;
	public static int[] playerScores = new int[CreateGameBehaviors.sortedPlayers.Count];
	// Use this for initialization

	void Start () {
		endtime = System.DateTime.Now.AddSeconds (63);
		//endtime = CreateGameBehaviors.starttimestamp.AddSeconds (63);
		Debug.Log (endtime);


	}

	// Update is called once per frame
	void Update () {

	}

	#region Message receivers

	void multiPeerMessageReceiver( string param ) {
		var theStr = param;
		if (theStr.IndexOf ("DEAD") != -1) {
						string[] message = theStr.Split (',');
						GameObject.Find (message [2]).GetComponent<FlyMovement> ().UpdateDead ();
						playerScores [int.Parse (message [1])] += 1; 
				} else {
						string[] message = theStr.Split (',');
						//playerId + "," + fly.name + "," + timestamp
						DateTime mostRecentIncomingPacket = DateTime.ParseExact (message [2], "MM/dd/yyyy HH:mm:ss.fffff", CultureInfo.InvariantCulture);
						//Debug.Log("MessageRecieverTimeStamp"+mostRecentIncomingColorChangePacket.ToString("MM/dd/yyyy HH:mm:ss.fffff"));
						if (DateTime.Compare (mostRecentIncomingPacket, endtime) <= 0) {
								GameObject.Find (message [1]).GetComponent<FlyMovement> ().UpdateLocation (theStr);
						}
				}

	}

	#endregion
}
