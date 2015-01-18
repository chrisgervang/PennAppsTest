using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour {
	public List<AudioClip> pitches = new List<AudioClip>();
	// Use this for initialization
	public GameObject playPause;
	public GameObject restartSong;


	void Start () {
		// Debug.Log("CHECK IT" + audio.clip);
		// Debug.Log(pitches[0]);

	}

	public void playAudioClip(String name) {
		Debug.Log(name);
		if(name == "C") {
			audio.clip = pitches[0];
			audio.Play();
		} else if (name == "D") {
			audio.clip = pitches[1];
			audio.Play();
		} else if (name == "E") {
			audio.clip = pitches[2];
			audio.Play();
		} else if (name == "G") {
			audio.clip = pitches[3];
			audio.Play();
		}
	}

	private int playing = 0;
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

				// Debug.Log("Collision with:"+bell.name);
				//Debug.Log(bell.audio.clip);

				if (c2d != null && c2d.gameObject.name == playPause.name) {
					audio.clip = pitches[4];
					if(playing == 0) {

						audio.Play();
						playing = 1;
					} else if (playing == 1) {
						audio.Pause();
						playing = 0;
					}



				} else if (c2d != null && c2d.gameObject.name == restartSong.name) {

					audio.Stop();
					playing = 0;
				}
			}
		}
	}
}
