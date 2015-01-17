using UnityEngine;
using System.Collections;
using Prime31;
using System.Collections.Generic;
using System;


public class ColorGridMessageHandler : MonoBehaviour {
	private GameObject button;
	// Use this for initialization

	void Start () {

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
			GameObject.Find (message[1]+","+message[2]).GetComponent<ColorSquareMovement>().UpdateColor(theStr);

	}


	#endregion
}
