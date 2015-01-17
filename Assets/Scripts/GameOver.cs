using UnityEngine;
using System.Collections;
using System.Globalization;
using System;
using Prime31;
using UnityEngine.UI;
public class GameOver : MonoBehaviour {
	public GameObject canvas1;
	// Use this for initialization
	void Start () {
		canvas1.GetComponentInChildren<Text>().text = "Game Over";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
