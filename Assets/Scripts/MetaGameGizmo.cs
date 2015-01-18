using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MetaGameGizmo : MonoBehaviour {
	public Color[] shipColor = {Color.blue, Color.red, Color.green, Color.yellow, Color.magenta, Color.cyan};
	public GameObject ship;
	public static string currentGameMode;
	public static DateTime startOfCurrentGameMode = System.DateTime.Now;
	//"inMetaIntro"
	//"inMeta"
	//"inMetaEnd"
	//"inMiniIntro"
	//"inMini"
	//"inMiniEnd"

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (currentGameMode == "inMetaIntro") {
			if (DateTime.Compare (startOfCurrentGameMode.AddSeconds (1), System.DateTime.Now) <= 0) {
				//Enable Pirate canvas (fade would be nice)
				//Display text
			} else if (DateTime.Compare (startOfCurrentGameMode.AddSeconds (3), System.DateTime.Now) <= 0) {
				//Change level to minigame #1
			}
		}
	}
}
