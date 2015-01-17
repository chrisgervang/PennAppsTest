using UnityEngine;
using System.Collections;

public class PlaySongGizmo : MonoBehaviour {
	public GameObject bellPrefab;
	public GameObject audioManager;
	private GameObject bell;

	// Use this for initialization
	void Start () {
		CreateGameBehaviors.setResolutionToPortrait();
		bell = Instantiate(bellPrefab) as GameObject;



		bell.name = "Bell"+"0";
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

				if (c2d != null && c2d.gameObject.name == bell.name) {
					Debug.Log("Collision with:"+bell.name);
					//Debug.Log(bell.audio.clip);
					if(bell.name == "Bell0") {
						GameObject.Find("AudioManager").GetComponent<AudioManager>().playAudioClip("C");
					} else if (bell.name == "Bell1") {
						GameObject.Find("AudioManager").GetComponent<AudioManager>().playAudioClip("G");
					} else if (bell.name == "Bell2") {
						GameObject.Find("AudioManager").GetComponent<AudioManager>().playAudioClip("E");
					} else if (bell.name == "Bell3") {
						GameObject.Find("AudioManager").GetComponent<AudioManager>().playAudioClip("D");
					}
				}
				//If finger is stationary or moving
			}
		}
	}
}
