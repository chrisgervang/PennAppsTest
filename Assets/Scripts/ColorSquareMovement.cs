using UnityEngine;
using System.Collections;
using System.Globalization;
using System;
using Prime31;

public class ColorSquareMovement : MonoBehaviour {
	public Color[] squareColor = {Color.blue, Color.red, Color.green, Color.yellow};
	public GameObject square;
	private int state = -1;
	private DateTime timestamp = System.DateTime.MinValue;
	private GameObject movingGuy;
	// Use this for initialization
	void Start () {

	}
	void SendCoordinates (string coordinates){
		var theStr = CreateGameBehaviors.playerId+","+square.name+","+timestamp.ToString("MM/dd/yyyy hh:mm:ss.fffff", CultureInfo.InvariantCulture);
		var bytes = System.Text.UTF8Encoding.UTF8.GetBytes( theStr );

		var result = MultiPeerBinding.sendMessageToAllPeers( "ColorGridGizmo", "multiPeerMessageReceiver", theStr, true );
		Debug.Log( "send result: " + result );
	}
	public void UpdateColor (string theStr){
		string[] message = theStr.Split (',');
		DateTime newStamp = DateTime.ParseExact (message [3], "MM/dd/yyyy hh:mm:ss.fffff", CultureInfo.InvariantCulture);
		if (DateTime.Compare (timestamp, newStamp) <= 0) {
			state = int.Parse (message[0]);
			timestamp = newStamp;
			if(state ==-1){
				square.transform.renderer.material.color = Color.white;
			}
			else{
				square.transform.renderer.material.color = squareColor[state];
			}
		}
	}
	// Update is called once per frame
	void Update () {

		foreach (Touch currentTouch in Input.touches) {
			Vector2 v2 = new Vector2(Camera.main.ScreenToWorldPoint(currentTouch.position).x, Camera.main.ScreenToWorldPoint(currentTouch.position).y);
			//Debug.Log("Hero Vector" + v2);
			// Debug.Log("Physics!!" + Physics2D.OverlapPoint(v2));

			if (currentTouch.phase == TouchPhase.Began) {
				//Debug.Log("BEGAN");

				Collider2D c2d = Physics2D.OverlapPoint(v2);
				if (c2d != null && c2d.gameObject.name==square.name) {
					state = CreateGameBehaviors.playerId;
					timestamp = System.DateTime.Now;
					if(state ==-1){
						square.transform.renderer.material.color = Color.white;
					}
					else{
						square.transform.renderer.material.color = squareColor[state];
					}
					SendCoordinates(c2d.gameObject.name);
				}
			} else if (currentTouch.phase == TouchPhase.Moved) {
				//Debug.Log("MOVED");

			} else if (currentTouch.phase == TouchPhase.Ended) {
				//Debug.Log("ENDED");
				state = -1;
			}

		}
	}
}
