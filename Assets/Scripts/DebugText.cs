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
	public static string debugTimespan = "-1";
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

		string display = "Start time: " + CreateGameBehaviors.currentGameStartTime.ToString() +
		"\nEnd Time: " + ColorGridMessageHandler.endtime.ToString() +
		"\n1,1-Local: " + squares["1,1"].GetComponent<ColorSquareMovement>().LocalSquareLastChangedTimestamp.ToString() +
		"\n1,1-Inc:     " + squares["1,1"].GetComponent<ColorSquareMovement>().IncomingColorChangePacket.ToString() +
		"\nSystem.time: " + TimeSpan.FromTicks(System.DateTime.Now.Ticks) +
		"\nMR sent P: " + debugMostRecentSent +
		"\nMR inc P:  " + debugMostRecentIncoming +
		"\nTimespan: " + debugTimespan;

		debugTextToDisplay.text = display;


	}

}
