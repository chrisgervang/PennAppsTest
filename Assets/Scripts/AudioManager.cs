using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour {
	public List<AudioClip> pitches = new List<AudioClip>();
	// Use this for initialization



	void Start () {
		Debug.Log("CHECK IT" + audio.clip);
		Debug.Log(pitches[0]);

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


	// Update is called once per frame
	void Update () {

	}
}
