using UnityEngine;
using System.Collections;
using System.Globalization;
using System;
using UnityEngine.UI;

public class PlaySongGizmo : MonoBehaviour {
	public GameObject bellPrefab;
	public GameObject audioManager;
	private GameObject bell;
	private GameObject bell1;
	private GameObject bell2;
	private GameObject bell3;
	private string[] musicScore = {"E","D","C","D","E","E","E","D","D","D","E","G","G","E","D","C","D","E","E","E","E","D","D","E","D","C"};

	// Use this for initialization
	void Start () {
		//////DEBUG CODE:
		CreateGameBehaviors.updatePlayers();

		CreateGameBehaviors.setResolutionToPortrait();
		Debug.Log(CreateGameBehaviors.sortedPlayers.Count);
		if(CreateGameBehaviors.sortedPlayers.Count == 1) {
			bell = Instantiate(bellPrefab) as GameObject;
			bell1 = Instantiate(bellPrefab) as GameObject;
			bell2 = Instantiate(bellPrefab) as GameObject;
			bell3 = Instantiate(bellPrefab) as GameObject;

			bell.name = "Bell0";
			bell1.name = "Bell1";
			bell2.name = "Bell2";
			bell3.name = "Bell3";

			bell.transform.localScale = new Vector2(0.25f,0.25f);
			bell1.transform.localScale = new Vector2(0.15f,0.15f);
			bell2.transform.localScale = new Vector2(0.19f,0.19f);
			bell3.transform.localScale = new Vector2(0.21f,0.21f);

			bell.transform.position = new Vector2(-1.3f,2.5f);
			bell1.transform.position = new Vector2(-1.3f,-2.5f);
			bell2.transform.position = new Vector2(1.3f,2.5f);
			bell3.transform.position = new Vector2(1.3f,-2.5f);

		} else if(CreateGameBehaviors.sortedPlayers.Count == 4) {
			bell = Instantiate(bellPrefab) as GameObject;
			bell.name = "Bell"+CreateGameBehaviors.playerId;
		}


	}

	// Update is called once per frame
	void Update () {
		if (Input.touchCount == 1) {
			Touch currentTouch = Input.GetTouch(0);
			Vector2 finger = new Vector2(Camera.main.ScreenToWorldPoint(currentTouch.position).x, Camera.main.ScreenToWorldPoint(currentTouch.position).y);

			//If player started tapping screen
			if (currentTouch.phase == TouchPhase.Began) {
				//Debug.Log("BEGAN");

				Collider2D c2d = Physics2D.OverlapPoint(finger); // THIS IS A GLOBAL THING.
				// So we need to dig into it to see if we collided with teh object we want.

				//if (c2d != null && (c2d.gameObject.name == bell.name || c2d.gameObject.name == bell1.name || c2d.gameObject.name == bell2.name || c2d.gameObject.name == bell3.name)) {
					Debug.Log("Collision with:"+bell.name);
					//Debug.Log(bell.audio.clip);

					if (c2d != null && c2d.gameObject.name == bell.name) {
						GameObject.Find("AudioManager").GetComponent<AudioManager>().playAudioClip("C");
						notePlayed("C");
					} else if (c2d != null && c2d.gameObject.name == bell1.name) {
						GameObject.Find("AudioManager").GetComponent<AudioManager>().playAudioClip("G");
						notePlayed("G");
					} else if (c2d != null && c2d.gameObject.name == bell2.name) {
						GameObject.Find("AudioManager").GetComponent<AudioManager>().playAudioClip("E");
						notePlayed("E");
					} else if (c2d != null && c2d.gameObject.name == bell3.name) {
						GameObject.Find("AudioManager").GetComponent<AudioManager>().playAudioClip("D");
						notePlayed("D");
					}
				//}
				//If finger is stationary or moving
			}
		}
	}

	#region Message receivers

	void multiPeerMessageReceiver( string param ) {
		var theStr = param;
		string[] message = theStr.Split (',');
		string remotePlayerId = message[1];
		string remoteNote = message[2];
		DateTime mostRecentIncomingNoteChangePacket = DateTime.ParseExact (message [3], "MM/dd/yyyy HH:mm:ss.fffff", CultureInfo.InvariantCulture);


		notePlayed(remoteNote);
		//Debug.Log("MessageRecieverTimeStamp"+mostRecentIncomingColorChangePacket.ToString("MM/dd/yyyy HH:mm:ss.fffff"));
		// if (DateTime.Compare (mostRecentIncomingNoteChangePacket, endtime) <= 0) {
		// 	GameObject.Find (message [1] + "," + message [2]).GetComponent<ColorSquareMovement> ().UpdateColor (theStr);
		// }

	}

	#endregion
	public static int playSongScore = 0;
	private float progressPosition = -9.32f;
	void notePlayed(string note) {
		float progressStep = (9.32f-1f)/26f;

		if(playSongScore == musicScore.Length-1) {
			Debug.Log("YOU WIN!");

			//Show winning!!
		} else {
			if (note == musicScore[playSongScore]) {
				var tempPosition = progressPosition;
				playSongScore += 1;
				Debug.Log("You got a point!"+ musicScore.Length);
				progressPosition += progressStep;
				
				//Was trying to smoothen out ProgressBar movement...
				GameObject.Find("ProgressBarSong").transform.position = Vector3.Lerp(new Vector3(0,tempPosition,0), new Vector3(0,progressPosition,0), 0.5f);
				//Debug.Log(GameObject.Find("ProgressBarSong").transform.position);
			} else {
				Debug.Log("Ouch, start over");
				playSongScore = 0;

				progressPosition = -9.32f;
				GameObject.Find("ProgressBarSong").transform.position = new Vector2(0, progressPosition);


			}
		}
	}
}
