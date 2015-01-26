using UnityEngine;
using System.Collections;
using System.Globalization;
using System;
using Prime31;
using UnityEngine.UI;
public class Instructions : MonoBehaviour {
	public GameObject canvas1;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (TimeSpan.Compare (CreateGameBehaviors.currentSessionStartTime + new TimeSpan(0,0,3), TimeSpan.FromTicks(System.DateTime.Now.Ticks)) <= 0) {
			DestroyImmediate (canvas1);
		}
		else if(TimeSpan.Compare (CreateGameBehaviors.currentSessionStartTime + new TimeSpan(0,0,2), TimeSpan.FromTicks(System.DateTime.Now.Ticks)) <= 0) {
			canvas1.GetComponentInChildren<Text>().text = "Color The Grid \n 1";
		}
		else if(TimeSpan.Compare (CreateGameBehaviors.currentSessionStartTime + new TimeSpan(0,0,1), TimeSpan.FromTicks(System.DateTime.Now.Ticks)) <= 0) {
			canvas1.GetComponentInChildren<Text>().text = "Color The Grid \n 2";
		}
	}
}
