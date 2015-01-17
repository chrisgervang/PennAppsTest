using UnityEngine;
using System.Collections;
using Prime31;



public class HeroMovement : MonoBehaviour {
	public GameObject theGuy;
	private int state = -1;
	private GameObject movingGuy;
	// Use this for initialization
	void Start () {

	}
	void SendCoordinates (Vector2 coordinates){
		var theStr = "Hero,"+coordinates.x+","+coordinates.y;
		var bytes = System.Text.UTF8Encoding.UTF8.GetBytes( theStr );

		var result = MultiPeerBinding.sendRawMessageToAllPeers( bytes );
		Debug.Log( "send result: " + result );
	}
	// Update is called once per frame
	void Update () {

		if (Input.touchCount == 1) {
			Touch currentTouch = Input.GetTouch(0);
			Vector2 v2 = new Vector2(Camera.main.ScreenToWorldPoint(currentTouch.position).x, Camera.main.ScreenToWorldPoint(currentTouch.position).y);
			//Debug.Log("Hero Vector" + v2);
			// Debug.Log("Physics!!" + Physics2D.OverlapPoint(v2));

			Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(currentTouch.position.x, currentTouch.position.y, 10));





			if (currentTouch.phase == TouchPhase.Began) {
				//Debug.Log("BEGAN");

				Collider2D c2d = Physics2D.OverlapPoint(v2);
				if (c2d != null && c2d.gameObject.name == "Hero") {
					movingGuy = c2d.gameObject;
					//Debug.Log("COLLIDED WITH: " + movingGuy.name);
					if (c2d != null) {
						//Debug.Log("Hero Vector Moving" + v2);				//your code
						// Debug.Log("WIN TWO" + c2d);				//your code
						state = 0;
						// movingGuy.transform.position = v2;
						movingGuy.transform.position = Vector3.Lerp(theGuy.transform.position, touchPosition, Time.deltaTime);
						Vector2 position = new Vector2(movingGuy.transform.position.x, movingGuy.transform.position.y);
						SendCoordinates(position);
					}
				}
			} else if (currentTouch.phase == TouchPhase.Moved) {
				//Debug.Log("MOVED");

				if(state == 0) {
					if (Input.touchCount == 1) {
						//movingGuy.transform.position = v2;
						movingGuy.transform.position = Vector3.Lerp(theGuy.transform.position, touchPosition, Time.deltaTime*100);
						Vector2 position = new Vector2(movingGuy.transform.position.x, movingGuy.transform.position.y);
						SendCoordinates(position);

					}
				}
			} else if (currentTouch.phase == TouchPhase.Ended) {
				//Debug.Log("ENDED");
				state = -1;
			}

		}
	}
}
