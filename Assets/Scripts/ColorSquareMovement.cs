using UnityEngine;
using System.Collections;
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
		var theStr = Pair.playerId+","+square.name+","+timestamp.ToString();
		var bytes = System.Text.UTF8Encoding.UTF8.GetBytes( theStr );
		
		var result = MultiPeerBinding.sendMessageToAllPeers( "PairButton", "multiPeerMessageReceiver", theStr, true );
		Debug.Log( "send result: " + result );
	}
	public void UpdateColor (string theStr){
		string[] message = theStr.Split (',');
		DateTime newStamp = DateTime.Parse (message [3]);
		if (DateTime.Compare (timestamp, newStamp) < 0) {
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
					state = Pair.playerId;
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