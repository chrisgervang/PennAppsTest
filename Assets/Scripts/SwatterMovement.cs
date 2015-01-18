using UnityEngine;
using System.Collections;
using System.Globalization;
using System;
using Prime31;

public class SwatterMovement : MonoBehaviour {
	//PUBLICS
	public GameObject swatter;
	public Vector3 target=new Vector3(0,0,0);
	//Privates
	public static int playerId = CreateGameBehaviors.playerId; //-1 when the player is touching the screen, or when the square hasn't be touched by a player yet.
	private DateTime timestamp = System.DateTime.MinValue; //crazy low time so nothing is below it.

	// Use this for initialization
	void Start () {

	}
	void SendScreen (GameObject fly){
				var theStr = "DEAD,"+playerId + "," + fly.name + "," + timestamp.ToString ("MM/dd/yyyy HH:mm:ss.fffff", CultureInfo.InvariantCulture);
				var result = MultiPeerBinding.sendMessageToAllPeers ("FlyGameGizmo", "multiPeerMessageReceiver", theStr, true);

	}

	// Update is called once per frame
	void Update () {
		if (Input.touchCount == 1) {
						Touch currentTouch = Input.GetTouch (0);
						Vector2 v2 = new Vector2 (Camera.main.ScreenToWorldPoint (currentTouch.position).x, Camera.main.ScreenToWorldPoint (currentTouch.position).y);
						//Debug.Log("Hero Vector" + v2);
						// Debug.Log("Physics!!" + Physics2D.OverlapPoint(v2));
			
						Vector3 touchPosition = Camera.main.ScreenToWorldPoint (new Vector3 (currentTouch.position.x, currentTouch.position.y, 10));

						if (currentTouch.phase == TouchPhase.Began) {
								//Debug.Log("BEGAN");
				Debug.Log (touchPosition.x-.0000075f);
				Vector3 adjustedPosition = Camera.main.ScreenToWorldPoint (new Vector3(touchPosition.x, touchPosition.y,touchPosition.z )-new Vector3(0.17f,0.17f,-10));
								target=adjustedPosition;
								moveTo (target);
			}else if (currentTouch.phase == TouchPhase.Stationary) {
				Vector3 adjustedPosition = Camera.main.ScreenToWorldPoint (new Vector3(touchPosition.x, touchPosition.y, touchPosition.z)-new Vector3(0.17f,0.17f,-10));
				target=adjustedPosition;
				moveTo (target);}
				//Debug.Log("MOVED"); 
			else if (currentTouch.phase == TouchPhase.Moved) {
								//Debug.Log("MOVED");
						} else if (currentTouch.phase == TouchPhase.Ended) {
								//Debug.Log("ENDED");
						}
				} else if (swatter.transform.position != target) {
			moveTo(target);
				}
		
	}
	void moveTo(Vector3 touchPosition){
		swatter.transform.position = Vector3.Lerp (swatter.transform.position, touchPosition, .2f);
	}
}
