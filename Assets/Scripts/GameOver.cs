using UnityEngine;
using System.Collections;
using System.Globalization;
using System;
using Prime31;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
public class GameOver : MonoBehaviour {
	public GameObject canvas1;
	// Use this for initialization
	void Start () {
		canvas1.GetComponentInChildren<Text>().text = "Game Over";
		updateWinner ();
	}
	void updateWinner(){
		int[] playerScores = new int[CreateGameBehaviors.sortedPlayers.Count];

		for (int i = 1; i <= 4; i++){
			for (int j = 1; j<=6; j++){
				int playId = GameObject.Find (i+","+j).GetComponent<ColorSquareMovement> ().playerId;
				if(playId !=-1){
					playerScores[playId]+=1;
				}
			}
		}
		Dictionary<string, int> dict = (CreateGameBehaviors.sortedPlayers).ToDictionary (x => x, x => playerScores [(CreateGameBehaviors.sortedPlayers.IndexOf (x))]);
		SortedDictionary<string, int> sorted = new SortedDictionary<string, int> (dict);
		string scoresList = "";
		foreach (var pair in sorted) {
			scoresList = scoresList + pair.Key + ": " + pair.Value + "\n";
		}
		canvas1.GetComponentInChildren<Text> ().text = scoresList;
		canvas1.GetComponentInChildren<Text> ().fontSize = 60;

	}
	// Update is called once per frame
	void Update () {
		
	}
}
