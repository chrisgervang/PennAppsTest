using UnityEngine;
using System.Collections;
using Prime31;

public class Pair : MonoBehaviour {
	private GameObject button;
	// Use this for initialization

	void Start () {
		MultiPeerBinding.advertiseCurrentDevice( true, "prime31-MyGame" );
		MultiPeerManager.receivedRawDataEvent += multiPeerRawMessageReceiver;
	}

	// Update is called once per frame
	void Update () {
		if (Input.touchCount == 1) {
			Touch currentTouch = Input.GetTouch(0);
			Vector2 v2 = new Vector2(Camera.main.ScreenToWorldPoint(currentTouch.position).x, Camera.main.ScreenToWorldPoint(currentTouch.position).y);

			if (currentTouch.phase == TouchPhase.Began) {
				Debug.Log("BUTTON BEGIN");

				Collider2D c2d = Physics2D.OverlapPoint(v2);
				if (c2d != null && c2d.gameObject.name == "PairButton") {
					button = c2d.gameObject;
					Debug.Log("COLLIDED WITH: Button: " + button.name);
					if (c2d != null) {


						MultiPeerBinding.showPeerPicker();


					}
				}
			}

		}
	}

	#region Message receivers

	void multiPeerMessageReceiver( string param )
	{
		Debug.Log( "received a message: " + param );
	}


	void multiPeerRawMessageReceiver( string peerId, byte[] bytes )
	{
		var theStr = System.Text.UTF8Encoding.UTF8.GetString( bytes );
		Debug.Log( "received raw message from peer: " + peerId );
		Debug.Log( "message: " + theStr );
		string[] message = theStr.Split(',');
		Vector2 newPosition = new Vector2(float.Parse(message[1]), float.Parse(message[2]));
		GameObject.Find("Hero").transform.position = newPosition;

	}

	#endregion
}
