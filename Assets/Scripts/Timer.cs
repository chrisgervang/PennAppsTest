using UnityEngine;
using System.Collections;
using System.Globalization;
using System;
using Prime31;
using UnityEngine.UI;
public class Timer : MonoBehaviour {
	public GameObject canvas1;
	public GameObject gameOver;
	public int state=0;
	public GameObject handler;
	// Use this for initialization
	void Start () {
		state = 0;
		Debug.Log ("Game will end at this time: " + ColorGridMessageHandler.endtime);
	}

	// Update is called once per frame
	void Update () {
		//Debug.Log ("colortimeupdate"+ColorGridMessageHandler.endtime+" "+System.DateTime.Now+" "+DateTime.Compare (ColorGridMessageHandler.endtime, System.DateTime.Now));
		if (DateTime.Compare (ColorGridMessageHandler.endtime, System.DateTime.Now) <= 0) {
			canvas1.GetComponentInChildren<Text>().text = "END";
			if(state==0){
				state = -1;
				GameObject gameOverObject = Instantiate (gameOver) as GameObject;
				gameOverObject.GetComponent<Canvas> ().worldCamera = Camera.main;
				gameOverObject.GetComponent<GameOver> ().canvas1 = gameOverObject;
				Debug.Log("created");
			}
		}
		else{

			canvas1.GetComponentInChildren<Text>().text = (ColorGridMessageHandler.endtime-System.DateTime.Now).Seconds.ToString();
		}
	}
}
