using UnityEngine;
using System.Collections;
using System.Globalization;
using System;
using Prime31;
using UnityEngine.UI;
public class FlyInstructions : MonoBehaviour {
	public GameObject canvas1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (DateTime.Compare (CreateGameBehaviors.starttimestamp.AddSeconds (3), System.DateTime.Now) <= 0) {
			DestroyImmediate (canvas1);
		}
		else if(DateTime.Compare (CreateGameBehaviors.starttimestamp.AddSeconds (2), System.DateTime.Now) <= 0) {
			canvas1.GetComponentInChildren<Text>().text = "Tap to Swat! \n 1";
		}
		else if(DateTime.Compare (CreateGameBehaviors.starttimestamp.AddSeconds (1), System.DateTime.Now) <= 0) {
			canvas1.GetComponentInChildren<Text>().text = "Tap to Swat! \n 2";
		}
	}
}
