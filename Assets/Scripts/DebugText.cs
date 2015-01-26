using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System;
using UnityEngine.UI;

public class DebugText : MonoBehaviour {
	public Dictionary<string, GameObject> squares = new Dictionary<string, GameObject>();
	public static string debugMostRecentSent = "-1";
	public static string debugMostRecentIncoming = "-1";
	public Text debugTextToDisplay;
	// Use this for initialization
	void Start () {
		debugTextToDisplay = GetComponent<Text>();
		for (var i = 1; i <= 4; i++) {
			for (var j = 1; j <= 6; j++) {
				GameObject tempSquare = GameObject.Find(i+","+j);
				if (tempSquare != null) {
					squares.Add(i+","+j, tempSquare);
				}
			}
		}
	}

	// Update is called once per frame
	void Update () {

		string display = "Start time: " + CreateGameBehaviors.starttimestamp.ToString("MM/dd/yyyy HH:mm:ss.fff") +
		"\nEnd Time: " + ColorGridMessageHandler.endtime.ToString("MM/dd/yyyy HH:mm:ss.fff") +
		"\n1,1-Local: " + squares["1,1"].GetComponent<ColorSquareMovement>().LocalSquareLastChangedTimestamp.ToString("MM/dd/yyyy HH:mm:ss.fff") +
		"\n1,1-Inc:     " + squares["1,1"].GetComponent<ColorSquareMovement>().IncomingColorChangePacket.ToString("MM/dd/yyyy HH:mm:ss.fff") +
		"\nSystem.time: " + System.DateTime.Now +
		"\nMR sent P: " + debugMostRecentSent +
		"\nMR inc P:  " + debugMostRecentIncoming;

		debugTextToDisplay.text = display;


	}

}
