using UnityEngine;
using System.Collections;
using System.Globalization;
using System;
using Prime31;

public class FlyMovement : MonoBehaviour {
	//PUBLICS
	private static Vector2 N = new Vector2 (0, 1);
	private static Vector2 W = new Vector2 (-1, 0);
	private static Vector2 E = new Vector2 (1, 0);
	private static Vector2 S = new Vector2 (0, -1);
	private static Vector2 NE = new Vector2 (1, 1);
	private static Vector2 NW = new Vector2 (-1, 1);
	private static Vector2 SE = new Vector2 (1, -1);
	private static Vector2 SW = new Vector2 (-1, -1);

	public static Vector2[] directions = {NE, NW, SE, SW, N,W,E,S};
	public GameObject fly;
	//Privates
	public int playerId = -1; //-1 when the player is touching the screen, or when the square hasn't be touched by a player yet.
	private DateTime timestamp = System.DateTime.MinValue; //crazy low time so nothing is below it.

	// Use this for initialization
	void Start () {

	}
	void SendScreen (int playerId){
				var theStr = playerId + "," + fly.name + "," + timestamp.ToString ("MM/dd/yyyy HH:mm:ss.fffff", CultureInfo.InvariantCulture);
				var result = MultiPeerBinding.sendMessageToAllPeers ("FlyGameGizmo", "multiPeerMessageReceiver", theStr, true);

	}
	public void UpdateDead(){
		timestamp = System.DateTime.MaxValue;
		fly.transform.renderer.enabled = false;
	}

	public void UpdateLocation (string theStr){
		string[] message = theStr.Split (',');
		DateTime incoming = DateTime.ParseExact (message [2], "MM/dd/yyyy HH:mm:ss.fffff", CultureInfo.InvariantCulture);


		if (DateTime.Compare (timestamp, incoming) <= 0) {
			//Debug.Log ("Incoming square is newer! Changeing square: ");
			playerId = int.Parse (message[0]);
			timestamp = incoming;
			if(playerId != CreateGameBehaviors.playerId){
				//fly.transform.renderer.enabled = false;
			} else{
				fly.transform.renderer.enabled = true;
			}

		}
	}
	// Update is called once per frame
	void Update () {
		System.Random rnd = new System.Random();
		int runtest = rnd.Next (0, 2);
		if (runtest==1) {
						int movement = rnd.Next (0, 8);
						int timeMultiplier = rnd.Next (1, 14);
						int distanceMultiplier = rnd.Next (1, 3);
						Debug.Log (movement);
						Vector2 vec = new Vector2(distanceMultiplier * directions [movement].x,distanceMultiplier * directions [movement].y) +new Vector2(fly.transform.position.x,fly.transform.position.y);
						fly.transform.position = Vector3.Slerp (fly.transform.position, vec, Time.deltaTime * timeMultiplier);
				}
	}
}
